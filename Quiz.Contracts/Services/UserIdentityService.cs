using Azure.Core;
using Quiz.Contracts.Constants;
using Quiz.Contracts.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Services
{
    public class UserIdentityService : IUserIdentity
    {
        public IHttpContextAccessor _httpContextAccessor { get; set; }
        public ClaimsIdentity _principal { get; set; }
        public string _companyCode { get; set; }

        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor = httpContextAccessor;
                _principal = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            }



        }

        public List<Claim> GetIdentity()
        {
            return _principal.Claims.ToList();
        }

        public string GetCompanyCode()
        {
            string? companyCode = null;

            var path = _httpContextAccessor.HttpContext.Request.Path.Value;

            int index = path.IndexOf("/api");
            if (index > 0)
            {
                companyCode = path.Substring(1, index - 1);

            }
            else
            {
                companyCode = _httpContextAccessor.HttpContext.Request.Headers["CompanyCode"].ToString().ToUpper();
            }
            return companyCode ?? "-";
        }

        public string GetDomain()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";

            var path = request.PathBase;
            if (path != null)
                domain += path.Value;


            return domain;

        }

        public string GetLanguage()
        {
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers["Language"]))
            {
                return _httpContextAccessor.HttpContext.Request.Headers["Language"];
            }
            else
            {
                return Common.DefaultLanguage;
            }
        }

        public string GetPIN()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["PIN"];
        }

        public string GetPlatfrom()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["Platform"];
        }

        public string GetTokenPIN()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["TokenPIN"];
        }

        public string GetUserId()
        {
            var a = _principal.Claims.First(m => m.Type == "sub").Value;
            return a;
        }

        public string? GetCtrName()
        {
            string path = _httpContextAccessor.HttpContext.Request.Path.Value;

            int length = path.Split('/').Count();
            string? ctr = path.Split('/')[length - 2];

            return ctr;
        }


        public string? GetAuthor()
        {
            return _principal.Claims.FirstOrDefault(m => m.Type == JwtClaimTypes.GivenName)?.Value;
        }

        public int GetRoleId()
        {
            return Convert.ToInt32(_principal.Claims.FirstOrDefault(m => m.Type == JwtClaimTypes.Role)?.Value);
        }

        public string? GetRoleName()
        {
            return _principal.Claims.FirstOrDefault(m => m.Type == "")?.Value;
        }

        public string? GetPicture()
        {
            return _principal.Claims.FirstOrDefault(m => m.Type == JwtClaimTypes.Picture)?.Value;
        }

        public string? GetUserAgent()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString() ?? null;
        }

        public string? GetUserBrowser()
        {
            return GetBrowserNameWithVersion();
        }

        private string GetBrowserNameWithVersion()
        {
            var userAgent = GetUserAgent();
            var browserWithVersion = "";
            if (userAgent.IndexOf("Edge") > -1)
            {
                //Edge
                browserWithVersion = "Edge Browser Version : " + userAgent.Split(new string[] { "Edge/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Chrome") > -1)
            {
                //Chrome
                browserWithVersion = "Chrome Browser Version : " + userAgent.Split(new string[] { "Chrome/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Safari") > -1)
            {
                //Safari
                browserWithVersion = "Safari Browser Version : " + userAgent.Split(new string[] { "Safari/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Firefox") > -1)
            {
                //Firefox
                browserWithVersion = "Firefox Browser Version : " + userAgent.Split(new string[] { "Firefox/" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("rv") > -1)
            {
                //IE11
                browserWithVersion = "Internet Explorer Browser Version : " + userAgent.Split(new string[] { "rv:" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("MSIE") > -1)
            {
                //IE6-10
                browserWithVersion = "Internet Explorer Browser  Version : " + userAgent.Split(new string[] { "MSIE" }, StringSplitOptions.None)[1].Split('.')[0];
            }
            else if (userAgent.IndexOf("Other") > -1)
            {
                //Other
                browserWithVersion = "Other Browser Version : " + userAgent.Split(new string[] { "Other" }, StringSplitOptions.None)[1].Split('.')[0];
            }

            return browserWithVersion;
        }

        public string? GetAuthorization()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString() ?? null;
        }

    }
}

