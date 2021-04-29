using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        //Bir IValidator ver, bir de doğrulamam için varlık ver -- ProductValidator,product gibi
        //Doğrulama kurallarının olduğu class ve doğrulanacak class
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);//Validate ile doğru olup olmadığına baktık
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);//Değilse ValidationException la patlattık
            }
        }
    }
}
