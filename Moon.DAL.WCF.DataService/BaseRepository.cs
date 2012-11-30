using System.Data.Services.Client;
using System.Linq;

namespace Moon.DAL.WCF.DataService
{
    public abstract class BaseRepository<T> : BaseRepositoryReadOnly<T>, IRepository<T> where T : class 
   {
        protected BaseRepository(DataServiceContext context)
           : base(context){}
        
       public virtual void Insert(T entity)
       {
           var collection = new DataServiceCollection<T>(Context);
           collection.Insert(0, entity);
       }

       public virtual void Update(T entity)
       {
           if (!Context.Entities.Any(item => item.Entity.Equals(entity)))
           {
               Context.AttachTo(EntitySetName, entity);
           }
           
           Context.UpdateObject(entity);
           Attach(entity);
       }
       
       public virtual void Delete(T entity)
       {
           if (!Context.Entities.Any(item => item.Entity.Equals(entity)))
           {
               Context.AttachTo(EntitySetName,entity);
           }

           Context.DeleteObject(entity);
       }
       
       public virtual void Attach(T entity)
       {
           var collection = new DataServiceCollection<T>(Context);
           collection.Load(entity);
       }
   }
}
