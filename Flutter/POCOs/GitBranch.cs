using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Flutter.POCOs
{
    public class GitBranch
    {
        public string Name => branch.FriendlyName;
        public IList<GitBranch> Children { get; }

        private readonly Branch branch;
        public GitBranch(Branch branch, IEnumerable<GitBranch> children)
        {
            this.branch = branch;
            Children = new ObservableCollection<GitBranch>(children);
        }
    }
}
