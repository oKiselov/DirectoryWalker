using DirectoryWalker.Database.Contexts;
using DirectoryWalker.Database.Entities;
using DirectoryWalker.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories
{
    public class TreeReadRepository : ITreeReadRepository
    {
        private readonly DbSet<TreeNode> dbSet;

        public TreeReadRepository(DbContext dbContext)
        {
            this.dbSet = dbContext.Set<TreeNode>();
        }

        public async Task<TreeNode> GetNodeByCombinedPath(IEnumerable<string> combinedPath)
        {
            var collectionOfPathes = new NpgsqlParameter("array_pathes", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = (object)combinedPath };
            return dbSet.FromSql("SELECT * FROM tree.get_node_by_combined_path(@array_pathes)", collectionOfPathes).First();
        }

        public async Task<IEnumerable<TreeNode>> GetNodesChildren(TreeNode treeNode)
        {
            return dbSet
                .Where(node => node.ParentId == treeNode.Id)
                .Take(treeNode.AmountOfChildren)
                .ToList();
        }

    }
}
