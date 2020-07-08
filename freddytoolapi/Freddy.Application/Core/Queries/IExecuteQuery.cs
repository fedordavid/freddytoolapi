using System.Threading.Tasks;

namespace Freddy.Application.Core.Queries
{
    public interface IExecuteQuery<in TQuery, TResult> where TQuery : Query<TResult>
    {
        Task<TResult> Execute(TQuery query);
    }
}
