using System.Linq.Expressions;

namespace PresentationLayer.Infrastructure.Interfaces
{
    public interface ICommon<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>>? predicate = null, string? includeProperties = null);
        T GetT(Expression<Func<T,bool>>? predicate = null, string? includeProperties = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entities);
    }
}
