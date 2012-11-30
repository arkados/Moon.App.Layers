using System;
using System.Data.Services.Client;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Moon.DAL.WCF.DataService
{

   public abstract class BaseRepositoryReadOnly<T> : IRepositoryReadOnly<T> where T : class 
   {
         private readonly DataServiceContext _context;
       
       private string _entitySetName ;
       
       protected string EntitySetName
       {
           get { return _entitySetName ?? (_entitySetName = GetEntitySetName(typeof(T))); }
       }

       protected DataServiceContext Context { get { return _context; } }
       
      private static string GetEntitySetName(Type entityType)
       {
           var entitySetAttributes = entityType.GetCustomAttributes(true).
                OfType<EntitySetAttribute>().ToArray();  
           
           if (!entitySetAttributes.Any() )
           {
               throw new Exception("Pro " + entityType.Name + " se nepodarilo se najit  EntitySetAttribute");
           }

           if(entitySetAttributes.Count()>1)
           {
               throw new Exception("Pro " + entityType.Name + " bylo nalezeno vice jak jeden atribut EntitySetAttribute");
           }

           return entitySetAttributes.Single().EntitySet;
       }

       protected BaseRepositoryReadOnly(DataServiceContext context)
       {
           _context = context;
       }

       public virtual IQueryable<T> GetQuery(params Expression<Func<T, object>>[] includeProperties)
       {
           Context.MergeOption = MergeOption.NoTracking;
           var query = Context.CreateQuery<T>(EntitySetName);
           return includeProperties.Aggregate(query, (current, e) => current.Expand(e));
       }

   
   }
   
   }

