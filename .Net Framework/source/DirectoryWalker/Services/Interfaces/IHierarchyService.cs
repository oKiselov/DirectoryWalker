using DirectoryWalker.Database.Entities;
using DirectoryWalker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectoryWalker.Services.Interfaces
{
    public interface IHierarchyService
    {
        string GetRootNode();
        IEnumerable<string> GetFilteredNodesNames(string path);
        //Task<TreeNode> GetNodeByCombinedPath(IEnumerable<string> combinedPath);
        Task<IEnumerable<TreeNode>> GetChildrenNodes(TreeNode parentNode);
        bool IsFoundNodeEmpty(TreeNode treeNode);
        IEnumerable<LinkToChild> GetLinksToChildren(string enteredPath, IEnumerable<string> nodes);
    }
}
