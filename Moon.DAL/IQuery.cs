using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Moon.DAL
{
    public interface IQuery<T>
    {
        /// <summary>
        /// Gets or sets a number of records to skip.
        /// </summary>
        int? Skip { get; set; }

        /// <summary>
        /// Gets or sets a number of records to take.
        /// </summary>
        int? Take { get; set; }

        /// <summary>
        /// For easy sorting (GridView ...)
        /// </summary>
        string SortExpression { get; set; }

        /// <summary>
        /// Returns results of the query.
        /// </summary>
        IEnumerable<T> Execute(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Gets a total number of records.
        /// </summary>
        int GetCount();
    }
}
