using LibraryAutomation.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Repositories
{
    public class UserRepositories
    {
        public void SaveUser(User user)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                if (user.Id == default(int))
                {
                    dbContext.Users.Add(user);

                }

                dbContext.SaveChanges();
            }


        }

        public User GetUser(string userName)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
            }

        }

        public User GetUserById(int id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            }

        }

        public void SaveUserLoginToken(UserLoginToken userLoginToken)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                if (userLoginToken.Id == default(int))
                {
                    dbContext.UserLoginTokens.Add(userLoginToken);
                }

                dbContext.SaveChanges();
            }
        }
        public UserLoginToken GetUserLoginToken(string token)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {

                return dbContext.UserLoginTokens.Where(u => u.Token == token && u.ExpiryDate > DateTime.Now).FirstOrDefault();

            }
        }

        public UserLoginToken GetUserLoginTokenByUserId(int userId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {

                return dbContext.UserLoginTokens.Where(u => u.UserId == userId && u.ExpiryDate > DateTime.Now).FirstOrDefault();

            }
        }
    }
}