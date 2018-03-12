using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectoryWalker.Database.Entities.Mapping
{
    public class TreeNodeMap : EntityTypeConfiguration<TreeNode>
    {
        public TreeNodeMap()
        {
            ToTable("tree")
                .HasKey(entity => entity.Id);

            Property(entity => entity.Id)
                .HasColumnName("item_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(entity => entity.ParentId)
                .HasColumnName("parent_id")
                .IsRequired();

            Property(entity => entity.Name)
                .HasColumnName("item_name")
                .IsRequired();

            Property(entity => entity.AmountOfChildren)
                .HasColumnName("nodes_amount")
                .IsRequired();
        }
    }
}
