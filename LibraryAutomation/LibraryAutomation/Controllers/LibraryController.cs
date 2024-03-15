using LibraryAutomation.Data.Requests;
using LibraryAutomation.Data.Responses;
using LibraryAutomation.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryAutomation.Controllers
{
    public class LibraryController : ApiController
    {

        private UserHandler userHandler = new UserHandler();
        private BookHandler bookHandler = new BookHandler();

        [HttpGet]
        public InformationResponse Information()
        {
            InformationResponse response = new InformationResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = "Successful. / İşlem Başarılı." };

            response.DescriptionTr = @"LibraryAutomation uygulamasına hoş geldiniz. Uygulamanın temel amacı kütüphanedeki kitapların takibini yapmaktır. Kütüphaneye kitap eklemek, çıkarmak ve güncellemek gibi işlemler yapılabilmektedir. Ayrıca, kütüphaneden bir kitabın detaylarına ulaşmak da mümkündür.Uygulamayı kullanabilmek için öncelikle adınızı, soyadınızı, kullanıcı adınızı ve belirleyeceğiniz bir şifreyi kullanarak kayıt olmanız gerekmektedir. Ardından, giriş yaparak bir token almanız gerekmektedir. Bu token 10 dakika boyunca geçerlidir. 10 dakika sonunda yeni bir token alabilirsiniz.Kütüphane uygulamasında yapacağınız tüm işlemler için token gerekmektedir ve dil tercihinde bulunmanız gerekmektedir. Dil seçeneklerimiz için GetLanguage servisine istek atabilirsiniz. Ayrıca, GetBookTypes ile kitap türlerinin kod numaralarına ve GetBookCategories ile kitapların kategori kod numaralarına ulaşabilirsiniz.";

            response.DescriptionEn = @"Welcome to the LibraryAutomation application. The main purpose of the application is to track books in the library. Operations such as adding, removing, and updating books can be performed. Additionally, it is possible to access the details of a book from the library.To use the application, you need to register with your name, surname, username, and a password of your choice. Then, you need to log in to get a token. This token is valid for 10 minutes. After 10 minutes, you can obtain a new token.A token is required for all operations in the library application, and you need to specify your language preference. You can request language options by calling the GetLanguage service. Additionally, you can access the code numbers of book types with GetBookTypes and the category code numbers of books with GetBookCategories.";

            return response;
        }

        [HttpPost]
        public SaveUserResponse SaveUser(SaveUserRequest request)
        {
            SaveUserResponse response = new SaveUserResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                userHandler.SaveUser(request);
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }

        [HttpPost]
        public LoginUserResponse Login(LoginUserRequest request)
        {
            LoginUserResponse response = new LoginUserResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                var result = userHandler.UserValidation(request);
                if (result.IsSuccess)
                {
                    response.Token = userHandler.GetToken(request.UserName, request.Password);
                }
                else
                {
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }

        [HttpPost]
        public SaveBookResponse SaveBook(SaveBookRequest request)
        {

            SaveBookResponse response = new SaveBookResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    bookHandler.SaveBook(request, result.UserId);
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }

        [HttpPost]
        public GetBookResponse GetBook(GetBookRequest request)
        {

            GetBookResponse response = new GetBookResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    var book = bookHandler.GetBook(request.BookId);

                    if (book == null)
                        response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Book Not Found." : "Kitap Bulunamadı.";

                    response.BookModel = book;
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }

        [HttpPost]
        public DeleteBookResponse DeleteBook(DeleteBookRequest request)
        {
            DeleteBookResponse response = new DeleteBookResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    bookHandler.DeleteBook(request.BookId, result.UserId);
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }

        [HttpPost]
        public GetBooksWithFilterResponse GetBooksWithFilter(GetBooksWithFilterRequest request)
        {

            GetBooksWithFilterResponse response = new GetBooksWithFilterResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    var bookList = bookHandler.GetBooksWithFilter(request.Filter, request.Id);

                    if (bookList == null || bookList?.Count < 0)
                        response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Book Not Found." : "Kitap Bulunamadı.";

                    response.Books = bookList;
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }
        
        [HttpPost]
        public SaveBookResponse UpdateBook(SaveBookRequest request)
        {
            SaveBookResponse response = new SaveBookResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };
            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    var updateResult = bookHandler.UpdateBook(request, result.UserId);
                    if (updateResult != string.Empty)
                    {
                        response.ResponseMessage = updateResult;
                    }
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }
            return response;
        }

        [HttpPost]
        public GetTypesResponse GetTypeList(BaseRequest request)
        {
            GetTypesResponse response = new GetTypesResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };

            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    response.Types = bookHandler.GetTypeList();
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }

            return response;
        }

        [HttpPost]
        public GetCataoriesResponse GetCatagoryList(BaseRequest request)
        {
            GetCataoriesResponse response = new GetCataoriesResponse { ResponseCode = HttpStatusCode.OK, ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Successful." : "İşlem Başarılı." };

            try
            {
                var result = userHandler.LoginValidation(request.Token, request.Language);

                if (result.IsSuccess)
                {
                    response.Catagories = bookHandler.GetCatagoryList();
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.OK;
                    response.ResponseMessage = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseMessage = request.Language == Data.Enums.Language.Eng ? "Unexpected error." : "Beklenmedik hata.";
            }

            return response;
        }


    }
}