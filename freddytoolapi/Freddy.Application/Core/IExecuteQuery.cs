using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Core
{
    public interface IExecuteQuery<in TQuery, TResult> where TQuery : Query<TResult>
    {
        TResult Execute(TQuery query);
    }
}
