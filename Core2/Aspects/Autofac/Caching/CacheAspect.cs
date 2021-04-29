using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)//60 dk sonra silinecek
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)//Intecepte override et,altındaki lifecircle'ı çalıştır
        {//ReflectedType:methodun Namespace'i (namespace.I-Service.GetAll (örnek))
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();//Methodun parametrelerini listeye çevir
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//Parametre değeri varsa onu geçer
            //arguments.select parametre edğerlerini listeye çevirir,string join ise virgülle yan yana getirir.?? yoksa demek
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);//Methodu hiç çalıştırmadan cacheten return yap
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }//Çalıştırdığımız methodun namespace'i,ismi,method ismi ve parametrelerine göre key oluşturuyor
}//Nortwind.Business.IProductDal.GetAll(parametre)
