using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryWalker.Database.Entities
{
    public class TreeNode
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Name { get; set; }
        public int AmountOfChildren { get; set; }
    }
}
