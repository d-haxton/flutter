using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flutter.POCOs;
using Flutter.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flutter.ViewModel
{
    public class GitHistoryControlViewModel : ReactiveObject
    {
        private readonly IGitRepositoryViewState viewState;

        public ObservableCollection<GitCommit> GitCommits { get; }
        public ObservableCollection<GitCommit> SelectedCommits { get; set; }


        public GitHistoryControlViewModel(IGitService gitService, IGitRepositoryViewState viewState)
        {
            this.viewState = viewState;

            this.WhenAnyValue(x => x.viewState.Commits)
                .BindTo(this, vm => vm.SelectedCommits);

            GitCommits = new ObservableCollection<GitCommit>();

            this.WhenAnyValue(x => x.viewState.Branch)
                .Subscribe(branch =>
                {
                    Console.WriteLine("branch changed");
                });

            foreach (var commit in gitService.Commits)
            {
                GitCommits.Add(new GitCommit(commit.MessageShort, commit.Author.When.LocalDateTime, commit.Author.Name, commit.Sha));
            }

            //gitService.
            //// dumby data for visual effect
            //for (var i = 0; i < 100; i++)
            //{
            //    GitCommits.Add(new GitCommit($"bond/WIN-00{i}", "The name's Bond. James Bond.", DateTime.Now, "James Bond", Guid.NewGuid().ToString().Replace("-", "")));
            //}
        }
    }
}
