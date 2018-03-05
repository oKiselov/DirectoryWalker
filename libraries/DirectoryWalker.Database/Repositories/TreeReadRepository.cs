using DirectoryWalker.Database.Contexts;
using DirectoryWalker.Database.Entities;
using DirectoryWalker.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories
{
    public class TreeReadRepository : ITreeReadRepository
    {
        private readonly DbSet<TreeNode> dbSet;
        private readonly DirectoryWalkerContext dbContext;

        public TreeReadRepository(DirectoryWalkerContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TreeNode>();
            Val = 5;
        }

        public int Val { get ; set; }

        public async Task<bool> CheckIfPathExists(IEnumerable<string> combinedPath)
        {

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM tree.check_pathes(@array_pathes)";
                command.Parameters.Add(new NpgsqlParameter("array_pathes", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = (object)combinedPath });
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                var res = (bool)await command.ExecuteScalarAsync();

            }

            var collectionOfPathes = new NpgsqlParameter("array_pathes", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = (object)combinedPath };

            var a = await dbSet.FromSql("SELECT * FROM tree.check_pathes(@array_pathes)", collectionOfPathes).FirstOrDefaultAsync();

            return await dbSet.FromSql("SELECT * FROM tree.check_pathes(@array_pathes)", collectionOfPathes).AnyAsync();
        }
    }
}
