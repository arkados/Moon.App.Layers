using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test
{
    public class LanguageRepository : Moon.DAL.EF.BaseRepositoryReadOnly<Language>, ILanguageRepository
    {
        public LanguageRepository(HotelsEntities context)
            : base(context)
        {
        }
    }
}