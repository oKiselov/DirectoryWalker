﻿using DirectoryWalker.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories.Interfaces
{
    public interface ITreeReadRepository
    {
        TreeNode GetRootNode();
        Task<TreeNode> GetNodeByCombinedPath(IEnumerable<string> combinedPath);
        Task<IEnumerable<TreeNode>> GetNodesChildren(TreeNode treeNode);
    }
}
