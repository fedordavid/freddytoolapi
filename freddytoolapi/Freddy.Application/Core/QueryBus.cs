using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Core
{
    public class QueryBus
    {
        private IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : Query<TResult>
        {
            var executor = _serviceProvider.GetService<IExecuteQuery<TQuery, TResult>>();

            return executor.Execute(query);
        }
    }
}
