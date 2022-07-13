using System.Threading.Tasks;

namespace Deliveroo.Service
{
    public interface IBaseRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();
    }
}
