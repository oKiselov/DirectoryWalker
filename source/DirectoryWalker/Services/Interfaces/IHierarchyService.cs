using DirectoryWalker.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryWalker.Services.Interfaces
{
    public interface IHierarchyService
    {
        IEnumerable<string> GetFilteredNodesNames(string path);
        Task<TreeNode> GetNodeByCombinedPath(IEnumerable<string> combinedPath);
        Task<IEnumerable<TreeNode>> GetChildrenNodes(TreeNode parentNode);
        bool IsFoundNodeEmpty(TreeNode treeNode);
    }
}
