using DirectoryWalker.Database.Entities;
using DirectoryWalker.Database.Repositories.Interfaces;
using DirectoryWalker.Models;
using DirectoryWalker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryWalker.Services
{
    public class HierarchyService : IHierarchyService
    {
        private readonly ITreeRepository treeRepository;

        public HierarchyService(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public string GetRootNode()
        {
            return this.treeRepository.GetRootNode().Name;
        }

        public IEnumerable<string> GetFilteredNodesNames(string path)
        {
            var nodesPathes = path.Split('/');
            return IsPathFitToPattern(nodesPathes) ? nodesPathes : new string[] { };
        }

        public async Task<IEnumerable<TreeNode>> GetChildrenNodes(TreeNode parentNode)
        {
            return await this.treeRepository.GetNodesChildren(parentNode);
        }

        public async Task<TreeNode> GetNodeByCombinedPath(IList<string> combinedPath)
        {
            return await this.treeRepository.GetNodeByCombinedPath(combinedPath);
        }

        public IEnumerable<LinkToChild> GetLinksToChildren(string currentPath, IEnumerable<string> nodes)
        {
            return nodes.Select(node => new LinkToChild { Name = node, FullPath = currentPath + "/" + node });
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