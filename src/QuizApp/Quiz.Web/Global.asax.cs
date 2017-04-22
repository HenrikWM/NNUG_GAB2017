using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Quiz.DataAccess.Ef;
using Quiz.DataAccess.InMemory.Quiz.InMemory;

namespace Quiz.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //InMemoryDbConfiguration.Seed();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QuizAppEntities, Quiz.DataAccess.Ef.Migrations.Configuration>());
        }
    }
}
