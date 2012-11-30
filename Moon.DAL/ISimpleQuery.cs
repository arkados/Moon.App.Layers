using System;
using System.Linq;

namespace Moon.DAL
{
    public interface ISimpleQuery<T> : IQuery<T>
    {
        Func<IQueryable<T>, IQueryable<T>> FilterAndSort { get; set; }
    }
}
