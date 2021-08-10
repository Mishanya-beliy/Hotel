using System.Collections.Generic;

namespace Hotel.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        public int Create(TEntity item);
        public bool Update(int id, TEntity item);
        public bool Delete(int id);
        public TEntity Get(int id);
        public IEnumerable<TEntity> GetAll();
    }
}
