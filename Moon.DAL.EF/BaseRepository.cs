using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Moon.DAL.EF
{
    /// <summary>
    /// Bazova trida Repository pro EF 5
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
   public abstract class BaseRepository<T> : BaseRepositoryReadOnly<T>, IRepository<T> where T:class 
   {
       protected BaseRepository(DbContext context ):base(context)
       {}
   
       /// <summary>
       /// Přidá entitu do sledování, včetně Related Entit a nastaví ji
       /// EntityState.Added
       /// </summary>
       /// <param name="entity">Entita pro vložení.</param>
       public virtual void Insert(T entity)
       {
           Context.Entry(entity).State = EntityState.Added;
       
       }

       
       /// <summary>
       /// Pokud má entitu se stejným klíčem ve sledování, aplikuje na její primitivní vlastnosti
       /// Vlastnosti, které má předaná entita .
       /// Jinak připojí předanou entitu do sledování a nastaví ji jako změněnou
       /// </summary>
       /// <param name="entity">Entita pro Update</param>
       public virtual void Update(T entity)
       {
         ObjectStateEntry entry;
         var key = ObjContext.CreateEntityKey(EntitySetName, entity);
        
         ObjContext.ObjectStateManager.TryGetObjectStateEntry(key, out entry);

         if (entry != null)
         {
             ObjContext.ApplyCurrentValues(key.EntitySetName, entity);
         }
         else
         {
             Context.Entry(entity).State = EntityState.Modified;
         }
       }
       

       /// <summary>
       /// Pokud nemá předanou entitu ve sledování, Přidá jí do sledování a nastaví ji jako Deleted
       /// </summary>
       /// <param name="entity">Entita pro smazání</param>
       public virtual void Delete(T entity)
       {
           var key = ObjContext.CreateEntityKey(EntitySetName, entity);

           ObjectStateEntry stateEntry;

           if (!ObjContext.ObjectStateManager.TryGetObjectStateEntry(key, out stateEntry))
           {
              Context.Entry(entity).State = EntityState.Deleted;
           }
           else
           {
               stateEntry.Delete();
           }
       }
    
       /// <summary>
       /// Připojí entitu na sledování
       /// </summary>
       /// <param name="entity">Entita která se má začít sledovat</param>
       public virtual void Attach(T entity)
       {
           ObjContext.AttachTo(EntitySetName, entity);
       }
   }
}
