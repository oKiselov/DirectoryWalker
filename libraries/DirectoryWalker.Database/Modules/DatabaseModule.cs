using Autofac;
using DirectoryWalker.Database.Contexts;
using DirectoryWalker.Database.Repositories;
using DirectoryWalker.Database.Repositories.Interfaces;

namespace DirectoryWalker.Database.Modules
{
    public class DatabaseModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DirectoryWalkerContext>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TreeReadRepository>()
                .As<ITreeReadRepository>();
        }
    }
}
