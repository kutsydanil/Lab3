using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Interface;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Cinema.Context;

namespace Cinema.Services
{
    public class GenericCachedClassService<T> : IGenericCachedServices<T> where T : class
    {
        private CinemaContext _db;
        private int _rowsCount = 20;
        private IMemoryCache _cache;
        private DbSet<T> _table;
        private int _seconds = 262;

        public GenericCachedClassService(CinemaContext contextDb, IMemoryCache memoryCache)
        {
            this._db = contextDb;
            this._cache = memoryCache;
            this._table = _db.Set<T>();
        }

        public void Add(T obj)
        {
            _table.Add(obj);
            _db.SaveChanges();
            Type type = typeof(T);
            var obj_id = type.GetField("id")?.GetValue(obj);
            if(obj_id != null)
            {
                _cache.Set(obj_id, obj, new MemoryCacheEntryOptions {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_seconds)
                });
            }
            else
            {
                throw new Exception($"Exception in add element into table - {type.Name}");
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _table.Take(_rowsCount);
        }

        public IEnumerable<T> GetAll(string cacheKey)
        {
            IEnumerable<T> elements = null;
            Type type = typeof(T);
            if (!_cache.TryGetValue(cacheKey, out elements))
            {
                elements = _table.Take(_rowsCount).ToList();
                if (elements != null)
                {
                    _cache.Set(cacheKey, elements,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
                else
                {
                    throw new Exception($"Any problems with cache _rowCount element in table {type.Name}");
                }
            }
            return elements;

        }

        public T GetById(int id)
        {
            T obj = null;
            Type type = typeof(T);
            if (!_cache.TryGetValue(id, out obj))
            {
                obj = _table.Find(id);
                if (obj != null)
                {
                    //var any = type.GetField("Id");
                    //var obj_id = any?.GetValue(obj);
                    _cache.Set(type.Name + id, obj, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_seconds)
                    });
                }
                else
                {
                    throw new Exception($"Not Found Element By Id in table {type.Name}");
                }
            }
            return obj;
        }
    }
}
