using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute> //Classın attributelarını oku
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name) //Methodun attributelarını oku
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));//Otomatik olarak sistemdeki bütün methodları loga dahil et
            //Attributeları listeye koy,Çalışma sırasını öncelik değerine göre sırala
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
