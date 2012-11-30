using System;
using System.Collections.Generic;
using System.Linq;

namespace Moon.DAL
{
    public abstract  class CultureQuery<T> : BaseQuery<T>, ICultureQuery<T>
    {
        public System.Globalization.CultureInfo Culture
        {
            get; set;
        }

        public override IEnumerable<T> Execute(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            if(Culture== null)
            {
                Culture = System.Globalization.CultureInfo.CurrentCulture;
            }

            return base.Execute(includeProperties);
        }

        public Func<IQueryable<T>, IQueryable<T>> FilterAndSort { get; set; }
    }
}
