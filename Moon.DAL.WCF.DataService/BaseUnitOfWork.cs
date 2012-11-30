using System.Data.Services.Client;
using System.Linq;

namespace Moon.DAL.WCF.DataService
{
   public class BaseUnitOfWork :IUnitOfWork
   {
       private readonly DataServiceContext _context;

       public DataServiceContext Context
       {
           get { return _context; }
       }

       public bool IsAttach (object entity)
       {
           return Context.Entities.Any(item => item.Entity.Equals(entity));
       }

       public BaseUnitOfWork(DataServiceContext context)
       {
           _context = context;
       }
       
       public int Commit()
       {
           return Context.SaveChanges(SaveChangesOptions.Batch).Count();
       }
       
       public void Detach(object entity)
       {
          Context.Detach(entity);
       }
   }
}
