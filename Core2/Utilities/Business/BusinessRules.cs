using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {//params verdiğimiz zaman Run içerisine istediğimiz kadar parametre olarak IResult verebiliyoruz
        //Gönderdiğimiz bütün parametreleri array olarak IResult[]'a atıyor
        public static IResult Run(params IResult[] logics)
        {//Parametre olarak gönderdiğimiz iş kuralı başarısızsa Business'a haber veriyoruz
            //Mevcut bir hata varsa direk hatayı döndürür
            foreach (var logic in logics)//Tüm kuralları gez
            {
                if (!logic.Success)//Kurala uymayan varsa döndür
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
