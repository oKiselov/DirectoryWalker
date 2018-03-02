using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace DirectoryWalker.Database.Contexts
{
    public sealed class DirectoryWalkerContext: DbContext
    {
        public DirectoryWalkerContext(DbContextOptions<DirectoryWalkerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetDirectoryWalkerSchema(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void SetDirectoryWalkerSchema(ModelBuilder modelBuilder)
        {
            // TODO: schemas mapping
        }
    }
}