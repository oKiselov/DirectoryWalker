using DirectoryWalker.Database.Entities.Mapping;
using System.Data.Entity;

namespace DirectoryWalker.Database.Contexts
{
    public class DirectoryWalkerContext : DbContext
    {
        public DirectoryWalkerContext()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetDirectoryWalkerSchema(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void SetDirectoryWalkerSchema(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TreeNodeMap());
        }
    }
}
