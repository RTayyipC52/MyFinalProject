using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    //Bütün methodlar önce bu kurallardan geçecek
    public abstract class MethodInterception : MethodInterceptionBaseAttribute//Sen bir attribute'sun
    {
        //invocation çalıştırmak istediğimiz method -- business method
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {// try catch hata yakalama bloğu
            var isSuccess = true;
            OnBefore(invocation); //invocation methodunun başında çalışır, methoddan önce
            try //Çalıştırmayı dene
            {
                invocation.Proceed();
            }
            catch (Exception e) //Hata alırsan bunu dene
            {
                isSuccess = false;
                OnException(invocation, e); //invocation methodu hata aldığında çalışır
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation); //invocation methodu başarılı olduğunda çalışır
                }
            }
            OnAfter(invocation); //invocation methodundan sonra çalışsın
        }
    }
}
