using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moon.DAL;

namespace test
{
    public class LanguageService :Moon.BLL.BaseReadOnlySimpleService<Language>,ILanguageService
    {
        public LanguageService(ILanguageRepository repository)
            : base(repository)
        {
        }
    }
}