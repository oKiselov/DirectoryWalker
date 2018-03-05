using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryWalker.Database.Infrastructure.Interfaces
{
    public interface IMappingEntity<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}
