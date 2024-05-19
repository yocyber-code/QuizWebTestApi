//using Quiz.Contracts.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
//using Quiz.Contracts.DTOs;

//using Microsoft.EntityFrameworkCore;
//using static Quiz.Contracts.Constants.Common;
//using Quiz.Contracts.Entities;

//namespace Wattana.Web.Middleware
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
//    public class IdentityAuthentication : AuthorizeAttribute, IAuthorizationFilter
//    {
//        private IConfiguration _config;
//        private IMemoryCache _memoryCache;
//        private IDatabaseManager _database;
//        private IUserIdentity _userIdentity;
//        private IUnitOfWork _repository;

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            AssignServices(context.HttpContext.RequestServices);
//            // check is authen
//            if (!context.HttpContext.User.Identity.IsAuthenticated)
//            {
//                context.HttpContext.Response.StatusCode = 403;
//                context.Result = new JsonResult(new ApiGenericResult<dynamic>() { Code = 401, Count = 1, Message = "Authorization has been denied for this request.", MessageAlt = "การอนุญาตถูกปฏิเสธสำหรับคำขอนี้" });
//                return;
//            }

//            var user = ValidAccount();
//            if (user == null)
//            {
//                context.HttpContext.Response.StatusCode = 403;
//                context.Result = new JsonResult(new ApiGenericResult<dynamic>() { Code = 401, Count = 1, Message = "Authorization has been denied for this request.", MessageAlt = "การอนุญาตถูกปฏิเสธสำหรับคำขอนี้" });
//                return;
//            }
//            if (user.RoleId != (int)UserRoleEnum.Customer)
//            {
//                context.HttpContext.Response.StatusCode = 403;
//                context.Result = new JsonResult(new ApiGenericResult<dynamic>() { Code = 401, Count = 1, Message = "Authorization has been denied for this request.", MessageAlt = "การอนุญาตถูกปฏิเสธสำหรับคำขอนี้" });
//                return;
//            }

//            return;

//        }

//        public void AssignServices(IServiceProvider services)
//        {
//            _config = services.GetRequiredService<IConfiguration>();
//            _memoryCache = services.GetRequiredService<IMemoryCache>();
//            _database = services.GetRequiredService<IDatabaseManager>();
//            _userIdentity = services.GetRequiredService<IUserIdentity>();
//            _repository = services.GetRequiredService<IUnitOfWork>();

//        }

//        public AUser ValidAccount()
//        {
//            var result = _database.ConvertToList<AUser>(_database.ExecuteQuery($"SELECT * FROM [dbo].[TD_USER] WHERE USER_ID ='" + _userIdentity.GetUserId() + "'")).FirstOrDefault();
//            return result;
//        }
//    }
//}
