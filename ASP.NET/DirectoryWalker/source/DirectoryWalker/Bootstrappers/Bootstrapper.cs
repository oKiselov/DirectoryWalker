using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

namespace DirectoryWalker.Bootstrappers
{
    public class Bootstrapper
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterControllers(typeof(MvcApplication).Assembly);

            builder
                .RegisterFilterProvider();

            builder
                .RegisterSource(new ViewRegistrationSource());

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();

            builder
                .RegisterAssemblyModules(assemblies);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}