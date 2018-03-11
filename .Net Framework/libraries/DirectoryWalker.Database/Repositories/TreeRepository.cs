using DirectoryWalker.Database.Entities;
using DirectoryWalker.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories
{
    public class TreeRepository: ITreeRepository
    {
        private readonly DbSet<TreeNode> dbSet;

        public TreeRepository(DbContext dbContext)
        {
            this.dbSet = dbContext.Set<TreeNode>();
        }

        public TreeNode GetRootNode()
        {
            return dbSet
                .OrderBy(node => node.Id)
                .FirstOrDefault();
        }

        //public async Task<TreeNode> GetNodeByCombinedPath(IEnumerable<string> combinedPath)
        //{
        //    var collectionOfPathes = new SqlParameter("array_pathes", System.Data.SqlDbType.Text) { Value = (object)combinedPath };
        //    return dbSet.FromSql("SELECT * FROM tree.get_node_by_combined_path(@array_pathes)", collectionOfPathes).First();
        //}

        public async Task<IEnumerable<TreeNode>> GetNodesChildren(TreeNode treeNode)
        {
            return dbSet
                .Where(node => node.ParentId == treeNode.Id)
                .Where(node => node.ParentId != node.Id)
                .OrderBy(node => node.Id)
                .Take(treeNode.AmountOfChildren)
                .ToList();
        }
    }
}
