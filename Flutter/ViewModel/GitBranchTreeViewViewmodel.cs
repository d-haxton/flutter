using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flutter.Data;
using Flutter.POCOs;
using Flutter.Reactive;
using Flutter.Services;
using Flutter.Settings;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Alm.Authentication;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flutter.ViewModel
{
    public class GitBranchTreeViewViewModel : ReactiveObject
    {
        [Reactive] public bool IsLocal { get; set; }

        private DatabaseCollection<GitRepository> repositories;

        [Reactive] public ObservableCollection<GitBranch> RootNodes { get; private set; }

        public GitBranchTreeViewViewModel()
        {
            RootNodes = new ObservableCollection<GitBranch>();
            repositories = new DatabaseCollection<GitRepository>();

            var gitService = new GitService(new GitSettings(repositories.FirstOrDefault()), new CredentialProvider());

            this.WhenAnyValue(x => x.IsLocal)
                .Subscribe(isLocal =>
                {
                    var nodes = gitService.Branches
                        .Where(x => x.IsRemote != isLocal)
                        .OrderByDescending(x => x.FriendlyName.Count(c => c == '/'));

                    foreach (var branch in GetNodes(nodes))
                    {
                        // TODO :: Will not track new branches very well, need to update later
                        RootNodes.Add(branch);
                    }
                });
        }

        private static IEnumerable<GitBranch> GetNodes(IEnumerable<Branch> items, string prefix = "/")
        {
            var preLen = prefix.Length;
            var candidates = items.Where(i => i.FriendlyName.Length > preLen && i.FriendlyName[preLen] != '0' && i.FriendlyName.StartsWith(prefix)).ToArray();
            return candidates.Where(i => i.FriendlyName.Length > preLen + 1 && i.FriendlyName[preLen + 1] == '0')
                .Select(i => new GitBranch(i, GetNodes(candidates, prefix + i.FriendlyName[preLen])))
                .ToArray();
        }
    }
}
