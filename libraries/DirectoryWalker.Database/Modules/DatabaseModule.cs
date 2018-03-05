using Autofac;
using DirectoryWalker.Database.Contexts;
using DirectoryWalker.Database.Repositories;
using DirectoryWalker.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;

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
                .Register(CreateDbContextOptions);

            builder
                .RegisterType<TreeReadRepository>()
                .As<ITreeReadRepository>();
        }

        private static DbContextOptions<DirectoryWalkerContext> CreateDbContextOptions(IComponentContext context)
        {
            var connection = context.Resolve<DbConnection>();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DirectoryWalkerContext>();
            dbContextOptionsBuilder.UseNpgsql(connection);

            return dbContextOptionsBuilder.Options;
        }

        private static DbConnection CreateEntityConnection(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();
            return new NpgsqlConnection(configuration["Data:ConnectionString"]);
        }
    }
}
