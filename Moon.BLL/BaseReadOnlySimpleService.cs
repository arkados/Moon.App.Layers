using Moon.DAL;

namespace Moon.BLL
{
   public class BaseReadOnlySimpleService<T> :IReadOnlyService<T>
   {
       protected readonly IRepositoryReadOnly<T> Repository;
       private ISimpleQuery<T> _query;

       public BaseReadOnlySimpleService(IRepositoryReadOnly<T> repository )
       {
           Repository = repository;
       }

       public ISimpleQuery<T> Query
       {
           get { return _query ?? (_query = new SimpleQuery<T>(Repository)); }
       }
   }
}
