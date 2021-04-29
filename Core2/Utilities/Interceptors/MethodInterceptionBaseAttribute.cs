using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    //Attribute'u classlara veya methodlara ekleyebilirsin,birden fazla ekleyebilirsin,inherit edilen noktada da attribute çalışsın
    //Attribute kodu çalışacağı zaman onun üstündeki vb. [] içindeki kodlar çalışacak
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } //Hangi attribute önce çalışsın

        public virtual void Intercept(IInvocation invocation) // Ne yapılacağı yazılacak
        {

        }
    }
}
