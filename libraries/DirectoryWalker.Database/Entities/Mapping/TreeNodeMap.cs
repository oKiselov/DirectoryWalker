using DirectoryWalker.Database.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryWalker.Database.Entities.Mapping
{
    class TreeNodeMap : IMappingEntity<TreeNode>
    {
        public void Map(EntityTypeBuilder<TreeNode> builder)
        {
            builder.ToTable("tree", "tree");
            builder.HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.Id)
                .HasColumnName("item_id")
                .IsRequired();

            builder
                .Property(entity => entity.ParentId)
                .HasColumnName("parent_id")
                .IsRequired();

            builder
                .Property(entity => entity.Name)
                .HasColumnName("item_name")
                .IsRequired();

            builder
                .Property(entity => entity.AmountOfChildren)
                .HasColumnName("nodes_amount")
                .IsRequired();
        }
    }
}
