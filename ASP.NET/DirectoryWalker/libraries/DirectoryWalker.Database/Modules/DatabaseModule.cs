using Autofac;
using DirectoryWalker.Database.Contexts;
using DirectoryWalker.Database.Repositories;
using DirectoryWalker.Database.Repositories.Interfaces;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DirectoryWalker.Database.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DirectoryWalkerContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder
                .Register(CreateEntityConnection)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TreeRepository>()
                .As<ITreeRepository>();
        }

        private static DbConnection CreateEntityConnection(IComponentContext context)
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DirectoryWalkerContext"].ConnectionString);
        }
    }
}
