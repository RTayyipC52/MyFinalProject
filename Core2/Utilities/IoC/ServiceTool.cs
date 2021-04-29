using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {//Web API veya Autofac de oluşturduğumuz injectionları oluşturabilmemize yarıyor
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)//.NET'in servislerini al
        {
            ServiceProvider = services.BuildServiceProvider();//Servisleri build et
            return services;
        }
    }//Hrehangi bir interfacein servisteki karşılığını ServiceTool'la alabiliriz. 
}
