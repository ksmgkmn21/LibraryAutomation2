using LibraryAutomation.Data;
using LibraryAutomation.Data.Entity;
using LibraryAutomation.Data.Enums;
using LibraryAutomation.Data.Models;
using LibraryAutomation.Data.Repositories;
using LibraryAutomation.Data.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LibraryAutomation.Handler
{
    public class UserHandler
    {
        private UserRepositories userRepositories = new UserRepositories();
        public UserHandler()
        {

        }

        public string SaveUser(SaveUserRequest request)
        {

            if (!IsUserNameExist(request.UserName))
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.UserName,
                    Password = Encrypt(request.Password),
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now
                };
                userRepositories.SaveUser(user);

                return string.Empty;
            }
            else
            {

                var message = request.Language == Language.Tr ? "{0} Kullanıcı adı zaten mevcut." : "{0} Username already exists.";
                return string.Format(message, request.UserName);
            }


        }

        public LoginValidationModel LoginValidation(string token, Language language)
        {
            LoginValidationModel model = new LoginValidationModel();
            var userToken = userRepositories.GetUserLoginToken(token);
            if (userToken != null)
            {
                model.IsSuccess = true;
                model.UserId = userToken.UserId;
            }
            else
            {
                model.IsSuccess = false;
                model.Message = language == Language.Tr ? "Lütfen Geçerli Bir Token Giriniz" : "Please Enter a Valid Token";
            }
            return model;
        }
        public string GetToken(string userName, string password)
        {

            var user = userRepositories.GetUser(userName);

            var token = Guid.NewGuid().ToString("N");

            var userLoginToken = new UserLoginToken
            {
                UserId = user.Id,
                Token = token,
                CreateDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            };

            userRepositories.SaveUserLoginToken(userLoginToken);

            return token;
        }

        public UserValidationModel UserValidation(LoginUserRequest request)
        {
            UserValidationModel model = new UserValidationModel();
            var user = userRepositories.GetUser(request.UserName);

            if (IsUserNameExist(request.UserName))
            {
                if (user.Password == Encrypt(request.Password))
                {
                    model.IsSuccess = true;

                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = request.Language == Language.Tr ? "Şifre hatalı." : "Password is incorrect.";
                }
            }
            else
            {
                model.IsSuccess = false;
                model.Message = request.Language == Language.Tr ? "Kullanıcı bulunamadı." : "User not found.";
            }
            return model;
        }

        public bool IsUserNameExist(string userName)
        {
            var user = userRepositories.GetUser(userName);

            return user == null ? false : true;
        }

        public string Encrypt(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}