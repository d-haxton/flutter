using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flutter.Data;
using Flutter.POCOs;
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
            var secrets = new SecretStore("git");
            var auth = new BasicAuthentication(secrets);
            var creds = auth.GetCredentials(new TargetUri("https://github.com"));

            RootNodes = new ObservableCollection<GitBranch>();
            repositories = new DatabaseCollection<GitRepository>();
            this.WhenAnyValue(x => x.IsLocal)
                .Subscribe(_ =>
                {
                    using (var repo = new Repository(repositories.FirstOrDefault()?.Path))
                    {
                        FetchOptions options = new FetchOptions();
                        options.CredentialsProvider = new CredentialsHandler((url, usernameFromUrl, types) =>
                            new UsernamePasswordCredentials()
                            {
                                Username = creds.Username,
                                Password = creds.Password
                            });
                        foreach (var remote in repo.Network.Remotes)
                        {

                            var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                            Commands.Fetch(repo, remote.Name, refSpecs, options, "");
                        }

                        var query = repo.Branches.OrderBy(x => x.RemoteName.Count(y => y == '/'));
                        var root = new GitBranch("Root");

                        foreach (var node in query)
                        {
                            RootNodes.Add(new GitBranch(node.FriendlyName));
                        }
                    }
                });
        }
    }
}
