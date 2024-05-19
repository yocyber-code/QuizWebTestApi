
namespace Quiz.Contracts.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        void Add(T entity);
        void Update(T entity);
        void InsertOrUpdate(T entity);
        void Delete(object id);
        int Count();
        IQueryable<T> GetQueryable();
        public void Cancel();
    }
}
