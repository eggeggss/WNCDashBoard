
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace Dashboard
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Assembly assembly = Assembly.GetExecutingAssembly();

            //builder.RegisterType<HumanService>().As<IHumanService>();
            //builder.RegisterType<HumanBaseDAL>().As<IHumanBaseDAL>();

            builder.RegisterAssemblyTypes(Assembly.Load("BLL"))
            .Where(x => x.Name.EndsWith("Service", StringComparison.Ordinal))
            .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("DAL"))
            .Where(x => x.Name.EndsWith("Repository", StringComparison.Ordinal))
            .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("DAL"))
          .Where(x => x.Name.EndsWith("Adapter", StringComparison.Ordinal))
          .AsImplementedInterfaces();


            builder.RegisterControllers(assembly);
            builder.RegisterApiControllers(assembly);

            var container = builder.Build();

            var resolverApi = new AutofacWebApiDependencyResolver(container);

            //GlobalConfiguration.Configuration.DependencyResolver = resolverApi;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}