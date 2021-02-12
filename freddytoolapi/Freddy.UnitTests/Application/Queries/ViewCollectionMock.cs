using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Freddy.Application.UnitTests.Application.Queries
{
    internal class ViewCollectionMock<TView> : EnumerableQuery<TView>, IAsyncEnumerable<TView>, IAsyncQueryProvider
    {
        private readonly IList<TView> _list;

        public ViewCollectionMock() : this(new TView[0]) { }

        public ViewCollectionMock(IList<TView> list) : base(list)
        {
            _list = list;
        }

        public IAsyncEnumerator<TView> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new AsyncEnumeratorAdapter(_list.GetEnumerator());
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return (TResult) typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(typeof(TResult).GetGenericArguments()[0])
                .Invoke(null, new[] { Execute(expression) });
        }

        private object Execute(Expression expression) => ((IQueryProvider) this).Execute(expression);

        private class AsyncEnumeratorAdapter : IAsyncEnumerator<TView>
        {
            private readonly IEnumerator<TView> _enumerator;

            public TView Current => _enumerator.Current;
            
            public AsyncEnumeratorAdapter(IEnumerator<TView> enumerator) => _enumerator = enumerator;

            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_enumerator.MoveNext());

            public ValueTask DisposeAsync()
            {
                _enumerator.Dispose();
                return new ValueTask();
            }
        }
    }
}