using DirectoryWalker.Database.Entities;
using DirectoryWalker.Database.Repositories.Interfaces;
using DirectoryWalker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryWalker.Services
{
    public class HierarchyService : IHierarchyService
    {
        private readonly ITreeReadRepository treeReadRepository;

        public HierarchyService(ITreeReadRepository treeReadRepository)
        {
            this.treeReadRepository = treeReadRepository;
        }

        public IEnumerable<string> GetFilteredNodesNames(string path)
        {
            var nodesPathes = path.Split('/');
            return IsPathFitToPattern(nodesPathes) ? nodesPathes : new string[] { };
        }

        public async Task<IEnumerable<TreeNode>> GetChildrenNodes(TreeNode parentNode)
        {
            return await this.treeReadRepository.GetNodesChildren(parentNode);
        }

        public async Task<TreeNode> GetNodeByCombinedPath(IEnumerable<string> combinedPath)
        {
            return await this.treeReadRepository.GetNodeByCombinedPath(combinedPath);
        }

        public bool IsFoundNodeEmpty(TreeNode treeNode)
        {
            return treeNode.Id == 0 && treeNode.AmountOfChildren == 0;
        }

        private bool IsPathFitToPattern(IEnumerable<string> nodesPathes)
        {
            return nodesPathes.All(node => !string.IsNullOrEmpty(node) && IsUnderPattern(node));
        }

        private bool IsUnderPattern(string itemPath)
        {
            return itemPath.ToLowerInvariant().All(letter => (letter >= 'a' && letter <= 'z') || Char.IsWhiteSpace(letter));
        }
    }
}