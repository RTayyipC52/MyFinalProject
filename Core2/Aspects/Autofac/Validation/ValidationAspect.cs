using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception//Aspect
    {//Aspect:Methodun başında, sonunda ve hata verdiğinde çalışacak yapı
        //ValidationAspect bir attribute. Attribute methoda anlam katmaya çalıştığımız yapılardır
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //Validator type ver
        {
            //Defensive coding:Savunma odaklı kodlama -- if'li yapı
            if (!typeof(IValidator).IsAssignableFrom(validatorType))//Gönderilen validatorType bir IValidator değilse kız diyor
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
                //throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType; //Hata yoksa benim validatorType'ım gönderilen validatorType dır diyor
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //Reflection: Çalışma anında bir şeyleri çalıştırabilmemizi sağlar
            //CreateInstance bir reflection,ProductValidator'ın instance'ını oluştur diyor
            //ProductValidator'ın base type'ını bul(AbstractValidator),onun generic argümanlarından ilkini bul
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//ProductValidator'ın çalışma tipini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//Methodun parametrelerini bul
            foreach (var entity in entities)//Her birini tek tek gez
            {
                ValidationTool.Validate(validator, entity);//ValidationTool'u kullanarak Validate et
            }
        }
    }
}
