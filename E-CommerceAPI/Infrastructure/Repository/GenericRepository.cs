using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly StoreContext _storeDb;
        public GenericRepository(StoreContext storeContext)
        {
            _storeDb = storeContext;
        }

        public void Delete(int id)
        {
            var obj = GetByIdAsync(id) as TEntity;
            if (obj != null)
            {
                _storeDb.Set<TEntity>().Remove(obj);
            }
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var query = _storeDb.Set<TEntity>().AsNoTracking();

            var queryCount = await query.CountAsync();

            if (queryCount == 0) return new List<TEntity>();

            var totalPages = (int)Math.Ceiling((double)queryCount / pageSize);

            var entities = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return entities;
        }


        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _storeDb.Set<TEntity>().FindAsync(id);
        }

        public async void Insert(TEntity obj)
        {
            await _storeDb.Set<TEntity>().AddAsync(obj);
        }

        public int Save()
        {
           return _storeDb.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _storeDb.Set<TEntity>().Update(obj);
        }
    }
}
