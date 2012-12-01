using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moon.BLL;
using Ninject;

namespace test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       [Inject]
        public ILanguageService Service { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ILanguageService Service1 = new LanguageService(new LanguageRepository(new HotelsEntities())); 

            var query = Service1.Query;
            var list = query.Execute().ToList();
            Response.Write(list.Count);
        }

        public IEnumerable<Language> GetLanguages()
        {

            return Service.Query.Execute();
        }
    }
}