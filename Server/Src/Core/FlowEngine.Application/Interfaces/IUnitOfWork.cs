using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
