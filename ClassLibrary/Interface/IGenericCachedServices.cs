using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Interface
{
    public interface IGenericCachedServices<T> where T : class
    {
        void Add(T obj);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(string cacheKey);

        T GetById(int id);

    }
}
