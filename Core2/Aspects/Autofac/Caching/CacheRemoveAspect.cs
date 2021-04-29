using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{//CacheRemoveAspect datamız bozulduğunda çalışır,data yeni data eklenirse,güncellenirse,silinirse bozulur
    //Managerda Cache yönetimi yapıyorsak o managerın veriyi manipüle eden methodlarına(add,delete,update)CacheRemoveAspect uygulanır
    public class CacheRemoveAspect:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }//Patterna göre silme işlemi yapar
    }
}
