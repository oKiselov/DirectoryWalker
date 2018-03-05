using DirectoryWalker.Database.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DirectoryWalker.Database.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void MapEntity<T, K> (this ModelBuilder modelBuilder) where K: IMappingEntity<T>, new() where T: class
        {
            var mapper = new K();
            mapper.Map(modelBuilder.Entity<T>());
        }
    }
}
