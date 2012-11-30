using System;
using System.Linq;
using System.Linq.Expressions;

namespace Moon.DAL
{
    public  interface IRepositoryReadOnly<T>
    {
       IQueryable<T> GetQuery(params Expression<Func<T, object>>[] includeProperties);
    }
}
