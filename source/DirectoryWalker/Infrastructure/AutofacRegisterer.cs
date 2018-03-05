using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryWalker.Infrastructure
{
    internal class AutofacRegisterer
    {
        private readonly ContainerBuilder builder;

        public AutofacRegisterer()
        {
            builder = new ContainerBuilder();
        }

        public void RegisterModules(IEnumerable<AssemblyName> assemblyNames)
        {
            var assemblies = assemblyNames
                .Where(name => name.Name.StartsWith("DirectoryWalker"))
                .Distinct()
                .Select(Assembly.Load);

            builder.RegisterAssemblyModules(assemblies.ToArray());
        }

        public void RegisterConfiguration(IConfiguration configuration)
        {
            builder.Register(context => configuration);
        }

        public IContainer Build(IServiceCollection services)
        {
            builder.Populate(services);
            return builder.Build();
        }
    }
}
