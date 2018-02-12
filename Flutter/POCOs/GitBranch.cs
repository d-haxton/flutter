using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Flutter.POCOs
{
    public class GitTreeItem
    {
        public IList<GitTreeItem> Children { get; } = new List<GitTreeItem>();
        public virtual string Name { get; }

        protected GitTreeItem()
        {

        }

        protected GitTreeItem(string name)
        {
            Name = name;
        }
    }

    public class GitTreeFolder : GitTreeItem
    {
        public GitTreeFolder(string part) : base(part)
        {
        }
    }

    public class GitTreeBranch : GitTreeItem
    {
        public override string Name => branch.FriendlyName;

        private readonly Branch branch;

        public GitTreeBranch(Branch branch)
        {
            this.branch = branch;
        }
    }
}
