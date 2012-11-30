using System;
using System.Linq;

namespace Moon.DAL
{
    public  class SimpleQuery<T> : BaseQuery<T>, ISimpleQuery<T>
    {
        private readonly IRepositoryReadOnly<T> _repository;
        
        protected IRepositoryReadOnly<T> Repository
        {
            get { return _repository;}
        }

        public SimpleQuery(IRepositoryReadOnly<T> repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Gets or sets a filter and sort function.
        /// </summary>
        public Func<IQueryable<T>, IQueryable<T>> FilterAndSort
        {
            get;
            set;
        }
        
        /// <summary>
        /// Vynucené přepsání, v BaseQuery je definovaná jako abstract
        /// </summary>
        /// <returns>Queryable bez stránkování Skip a Take s aplikuje az v Execute</returns>
        protected override IQueryable<T> GetNonPagedRecords( )
        {
            var data = Repository.GetQuery(IncludeProperties);
            
            if (FilterAndSort != null)
            {
                data = FilterAndSort(data);
            }
         
            return data;
        }
        
    }
}
