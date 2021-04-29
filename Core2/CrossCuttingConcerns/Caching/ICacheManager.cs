using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);//Key verip bellekte o keyin karşılığı datayı verir
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key);//Cachete var mı
        void Remove(string key);//Cacheten uçurma
        void RemoveByPattern(string pattern);//Verdiğimiz patterna göre silme işlemi
    }
}
