using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Moon.DAL.EF
{
   public abstract class BaseRepositoryReadOnly<T> : IRepositoryReadOnly<T> where T : class 
   {
       private readonly DbContext _context;
       private string _entitySetName;
       protected readonly DbSet<T> DbSet;
       
       protected BaseRepositoryReadOnly(DbContext context )
       {
          _context = context;
          DbSet = Context.Set<T>();
       }
       
       protected DbContext Context
       {
           get { return _context; }
       }
       
       protected ObjectContext ObjContext
       {
           get { return ((IObjectContextAdapter)Context).ObjectContext; }
       }
       
       protected string EntitySetName
       {
           get { return _entitySetName ?? (_entitySetName = ObjContext.GetEntitySetName(typeof(T))); }
       }
       
        /// <summary>
        /// Vracenej dotaz na vsechny entity je mozne i na dale omezovat ci s nim jinak pracovat.
        /// Do parametru je mozne zadat, ktere vlastnosti navazane pomoci relace chceme rovnou take vratit.
        /// Do databaze se pote posle dotaz, ktery rovnou obsahuje Join na podrizene Entity.
        /// </summary>
       /// <param name="includeProperties">Podrizene Entity. Ktere vlastnosti navazane pomoci relace chceme rovnou take vratit. </param>
        /// <returns>Dotaz IQueryable na vsechny entyty</returns>
        public virtual IQueryable<T> GetQuery( params Expression<Func<T, object>>[] includeProperties)
       {
           IQueryable<T> query = DbSet;
           query = PerformInclusions(includeProperties, query);
            return query.AsNoTracking();
       }
  

       /// <summary>
       /// Vlozi do dotazu rovnou Join na podrizene entity.
       /// </summary>
       /// <param name="includeProperties">Ktere podrizene Entity chceme rovnou zahrnout do vysledku dotazu.</param>
       /// <param name="query">Dotaz bez podrizenych Entit.</param>
       /// <returns>Upraveny dotaz zahrnujici jiz podrizene Entyty.</returns>
       private static IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query)
       {
          return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
       }
   
   }
   
   }

