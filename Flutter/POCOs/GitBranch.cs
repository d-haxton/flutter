using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.POCOs
{
    public class GitBranch
    {
        public IList<GitBranch> Children { get; }
        public string Name { get; set; }

        public GitBranch(string name)
        {
            Children = new List<GitBranch>();
            Name = name;
        }
    }
}
