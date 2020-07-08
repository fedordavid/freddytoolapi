using System.Threading.Tasks;

namespace Freddy.Application.Core.Queries
{
    public interface IQueryBus
    {
        Task<TResult> Execute<TResult>(Query<TResult> query);
    }
}