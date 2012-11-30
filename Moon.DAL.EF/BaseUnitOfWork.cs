using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace Moon.DAL.EF
{
    /// <summary>
    /// Bazova trida UnitOfWork pro EF 5
    /// </summary>
    public abstract class BaseUnitOfWork:IUnitOfWork
    {
        private readonly DbContext _context;
        
        public DbContext Context
        {
            get { return _context; }
        }

        protected BaseUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
          return Context.SaveChanges();
        }

        
        public bool IsAttach(object entity)
        {
            if (Context.ChangeTracker.Entries().Any(item => item.Entity.Equals(entity)))
            {
                return true;
            }

            return false;
        }


        private ObjectContext ObjContext
        {
            get { return ((IObjectContextAdapter)Context).ObjectContext; }
        }

        public void Detach(object entity)
        {
            ObjContext.Detach(entity);
        }
    }
}
