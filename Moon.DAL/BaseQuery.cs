using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using Moon.Linq;

namespace Moon.DAL
{
    public abstract class BaseQuery<T> : IQuery<T>
    {
        protected Expression<Func<T, object>>[] IncludeProperties { get; set; }

        /// <summary>
        /// Gets or sets a number of records to skip.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets a number of records to take.
        /// </summary>
        public int? Take { get; set; }


        /// <summary>
        /// Razeni jako string pro snadnou implementaci,
        /// razeni a strankovani pro GridView a ostatni, 
        /// data zobrazujici prvky.
        /// </summary>
        public string SortExpression { get; set; }

        private static string GetPropertyNameForDefaultSorting(Type type)
        {

            var attributes = type.GetCustomAttributes(false).FirstOrDefault(p => p is DataServiceKeyAttribute);

            if (attributes != null)
                return ((DataServiceKeyAttribute)attributes).KeyNames.FirstOrDefault();
         
            return type.GetProperties().Any() ? type.GetProperties().FirstOrDefault().Name : "";
        }

        private IQueryable<T> AddSortExpresion(IQueryable<T> query)
        {
            if (!string.IsNullOrEmpty(SortExpression))
            {
                var sValues = SortExpression.Split(' ');
                if (sValues.Length > 1)
                {
                    if (sValues[1].ToUpper() == "DESC")
                    {
                        query = query.OrderByDescending(sValues[0]);
                    }
                }
                else
                {
                    query = query.OrderBy(sValues[0]);
                }
            }
            
            return query;
        }

        /// <summary>
        /// Returns results of the query.
        /// </summary>
        public virtual IEnumerable<T> Execute(params Expression<Func<T, object>>[] includeProperties)
        {
            IncludeProperties = includeProperties;
            
            IQueryable<T> data = AddSortExpresion(GetNonPagedRecords());
            
            if (Skip != null)
            {
                //pokud chceme delat skip musime zajistit razeni
                if(!data.ContainsQueryOrder())
                {   
                    //Pridame sortovani
                    data = data.OrderBy(GetPropertyNameForDefaultSorting(typeof(T)));
                }

                data = data.Skip(Skip.Value);
            }
            
            if (Take != null)
            {
                data = data.Take(Take.Value);
            }
            
            return data.ToList();
        }

        /// <summary>
        /// Gets all records without respect to paging functions.
        /// </summary>
        protected abstract IQueryable<T> GetNonPagedRecords();

        /// <summary>
        /// Gets a total number of records.
        /// </summary>
        public int GetCount()
        {
            return GetNonPagedRecords().Count();
        }
    }
}
