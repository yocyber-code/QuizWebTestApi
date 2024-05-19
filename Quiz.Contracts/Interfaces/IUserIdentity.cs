using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Interfaces
{
    public interface IUserIdentity
    {
        string GetUserId();
        string? GetAuthor();
        string? GetRoleName();
        string GetCompanyCode();
        string GetTokenPIN();
        string GetPlatfrom();
        string GetLanguage();
        string? GetUserAgent();
        string? GetUserBrowser();
        string GetPIN();
        int GetRoleId();
        string? GetCtrName();
        string? GetPicture();
        List<Claim> GetIdentity();
        string? GetAuthorization();
        string? GetDomain();
    }
}
