using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using BlogBusinessLogic;
using System.Reflection;
using Autofac.Integration.Mvc;
using BlogRepositoryContext;

namespace MyBlogNew
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());//注册所有控制器
            builder.RegisterType<DBManager>().As<IDBmanager>().InstancePerLifetimeScope();
            builder.RegisterType<SqlserverCDB>().As<IRepository>().InstancePerLifetimeScope();
            var container = builder.Build();
            var resovler = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resovler);

        }
    }
}
