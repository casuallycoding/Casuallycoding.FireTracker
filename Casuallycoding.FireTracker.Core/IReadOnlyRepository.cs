using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Casuallycoding.FireTracker.Core
{
    interface IReadOnlyRepository<T> where T : IDatatable
    {

        public T Create(T value);

        public T Read(T value);

        public IEnumerable<T> Read(Expression<Func<T, bool>> search);

        public IEnumerable<T> ReadAll();


        

    }
}
