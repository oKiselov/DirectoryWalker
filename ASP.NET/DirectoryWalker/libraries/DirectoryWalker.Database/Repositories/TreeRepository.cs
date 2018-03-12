using DirectoryWalker.Database.Entities;
using DirectoryWalker.Database.Helpers;
using DirectoryWalker.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories
{
    public class TreeRepository : ITreeRepository
    {
        private readonly DbSet<TreeNode> dbSet;
        private readonly DbContext dbContext;

        public TreeRepository(DbContext dbContext)
        {
            this.dbSet = dbContext.Set<TreeNode>();
            this.dbContext = dbContext;
        }

        public TreeNode GetRootNode()
        {
            return dbSet
                .OrderBy(node => node.Id)
                .FirstOrDefault();
        }

        public async Task<TreeNode> GetNodeByCombinedPath(IList<string> pathes)
        {
            var dataTable = DataTableHelper.PrepareDataTableParameter(pathes);
            var node = new TreeNode();

            using (SqlConnection connection = new SqlConnection(dbContext.Database.Connection.ConnectionString))
            {
                var query = "SELECT * FROM inspect_path_and_get_last_node(@array_pathes)";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var parameter = command.Parameters.AddWithValue("@array_pathes", dataTable);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "[dbo].[PathesArray]";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        node = new TreeNode
                        {
                            Id = reader.GetInt64(0),
                            ParentId = reader.GetInt64(1),
                            Name = reader.GetString(2),
                            AmountOfChildren = reader.GetInt32(3)
                        };
                    }
                }
            }
            return node;
        }

        public async Task<IEnumerable<TreeNode>> GetNodesChildren(TreeNode treeNode)
        {
            return await dbSet
                .Where(node => node.ParentId == treeNode.Id)
                .Where(node => node.ParentId != node.Id)
                .OrderBy(node => node.Id)
                .Take(treeNode.AmountOfChildren)
                .ToListAsync();
        }
    }

}