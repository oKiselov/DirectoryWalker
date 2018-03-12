using DirectoryWalker.Database.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories.Interfaces
{
    public interface ITreeRepository
    {
        TreeNode GetRootNode();
        Task<TreeNode> GetNodeByCombinedPath(IList<string> pathes);
        Task<IEnumerable<TreeNode>> GetNodesChildren(TreeNode treeNode);
    }
}
