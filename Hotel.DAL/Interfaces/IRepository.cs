using System.Collections.Generic;

namespace Hotel.DAL.Interfaces
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public T Get(int id);
        public void Create(T item);
        public bool Update(int id, T item);
        public bool Delete(int id);
    }
}
