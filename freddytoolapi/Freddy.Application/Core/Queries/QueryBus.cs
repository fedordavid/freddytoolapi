using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Freddy.Application.Core.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> Execute<TResult>(Query<TResult> query)
        {
            var invokerType = typeof(ExecuteQueryInvoker<,>).MakeGenericType(query.GetType(), typeof(TResult));
            
            var invoker = Activator.CreateInstance(invokerType) as IExecuteQueryInvoker<TResult>;
            
            return invoker?.Execute(_serviceProvider, query) ?? throw new ApplicationException("Unable to invoke query");
        }
        
        private interface IExecuteQueryInvoker<TResult>
        {
            public Task<TResult> Execute(IServiceProvider serviceProvider, Query<TResult> query);
        }
        
        private class ExecuteQueryInvoker<TQuery, TResult> : IExecuteQueryInvoker<TResult> 
            where TQuery : Query<TResult>
        {
            public Task<TResult> Execute(IServiceProvider serviceProvider, Query<TResult> query)
            {
                var executor = serviceProvider.GetService<IExecuteQuery<TQuery, TResult>>();
            
                return executor.Execute(query as TQuery);
            }
        }
    }
}
