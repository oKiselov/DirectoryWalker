using Autofac;
using DirectoryWalker.Services;
using DirectoryWalker.Services.Interfaces;

namespace DirectoryWalker.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HierarchyService>()
                .As<IHierarchyService>();
        }
    }
}