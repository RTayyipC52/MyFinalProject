using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{//
    public static class ClaimsPrincipalExtensions
    {//ClaimsPrincipal JWTla gelen kişinin claimlerine ulaşmak için .Net'te olan class
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)//direk rolleri döndürür
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }//? null dönebilir diye
    }
}
