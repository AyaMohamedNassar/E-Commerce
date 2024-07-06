using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<TEntity> GetByIdAsync(int id);
        public void Insert(TEntity obj);
        public void Update(TEntity obj);
        public void Delete(int id);
        public int Save();
    }
}
