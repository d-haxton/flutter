using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flutter.POCOs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flutter.Services
{
    public interface IGitRepositoryViewState
    {
        GitTreeItem Branch { get; set; }
        GitRepository Repository { get; set; }

        ObservableCollection<GitCommit> Commits { get; set; }
    }

    public class GitRepositoryViewState : ReactiveObject, IGitRepositoryViewState
    {
        [Reactive] public GitTreeItem Branch { get; set; }
        [Reactive] public GitRepository Repository { get; set; }

        public ObservableCollection<GitCommit> Commits { get; set; }

        public GitRepositoryViewState()
        {
            Commits = new ObservableCollection<GitCommit>();
        }
    }
}
