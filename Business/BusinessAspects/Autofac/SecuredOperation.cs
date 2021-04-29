using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)//Rolleri ver
        {
            _roles = roles.Split(',');//Metni virgüle göre ayırıp arraya atar
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //ServiceTool bizim injection altyapımızı aynen okumamıza yarayan araç
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//Kullanıcının rollerini bul
            foreach (var role in _roles)//Rolleri gez
            {
                if (roleClaims.Contains(role))//İlgili rol varsa
                {
                    return;//Methodu çalıştırmaya devam et
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
