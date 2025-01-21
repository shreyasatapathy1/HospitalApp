using System.Linq.Expressions;

namespace HospitalApp.Data.Repository
{
    public interface IRepository<T> where T : class
    {
       
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
