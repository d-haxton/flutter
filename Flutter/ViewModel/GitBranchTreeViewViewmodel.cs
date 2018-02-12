using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Flutter.Data;
using Flutter.POCOs;
using Flutter.Reactive;
using Flutter.Services;
using Flutter.Settings;
using Flutter.View.Controls;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Alm.Authentication;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flutter.ViewModel
{
    public class GitBranchTreeViewViewModel : ReactiveObject
    {
        private readonly IGitService gitService;
        private readonly IGitRepositoryViewState viewState;
        private readonly DatabaseCollection<GitRepository> repositories;

        [Reactive] public EBranchType BranchType { get; set; }
        [Reactive] public GitTreeItem Branch { get; set; }

        public ObservableCollection<GitTreeItem> RootNodes { get; private set; }

        public GitBranchTreeViewViewModel(IGitService service, IGitRepositoryViewState viewState)
        {
            gitService = service;
            this.viewState = viewState;
            RootNodes = new ObservableCollection<GitTreeItem>();
            repositories = new DatabaseCollection<GitRepository>();

            this.WhenAnyValue(x => x.BranchType)
                .Where(x => x != EBranchType.Unknown)
                .Select(x => x == EBranchType.Remote)
                .Subscribe(UpdateRootNodes);

            this.WhenAnyValue(x => x.Branch)
                .BindTo(this, vm => vm.viewState.Branch);
        }

        private void UpdateRootNodes(bool isRemote)
        {
            RootNodes.Clear();

            var nodes = gitService.Branches
                .Where(x => x.IsRemote == isRemote)
                .OrderByDescending(x => x.FriendlyName.Count(c => c == '/'))
                .ToArray();

            var paths = nodes.Select(x => x.FriendlyName)
                .Where(x => x.LastIndexOf('/') != -1)
                .Select(x => x.Substring(0, x.LastIndexOf('/')))
                .Distinct().ToList();

            paths.Sort();

            foreach (var path in paths)
            {
                CreatePath(RootNodes, path, part => new GitTreeFolder(part));
            }

            foreach (var branch in nodes.Where(x => x.FriendlyName.IndexOf('/') == -1))
            {
                RootNodes.Add(new GitTreeBranch(branch.FriendlyName, branch));
            }

            foreach (var branch in nodes)
            {
                CreatePath(RootNodes, branch.FriendlyName, part => new GitTreeBranch(part, branch));
            }
        }

        private static void CreatePath(IList<GitTreeItem> nodeList, string path, Func<string, GitTreeItem> gitItemFunc)
        {
            while (true)
            {
                string folder;

                var p = path.IndexOf('/');

                if (p == -1)
                {
                    folder = path;
                    path = "";
                }
                else
                {
                    folder = path.Substring(0, p);
                    path = path.Substring(p + 1, path.Length - (p + 1));
                }

                GitTreeItem node = null;

                foreach (var item in nodeList)
                {
                    if (item.Name == folder)
                    {
                        node = item;
                    }
                }

                if (node == null)
                {
                    node = gitItemFunc(folder);
                    nodeList.Add(node);
                }

                if (path != "")
                {
                    nodeList = node.Children;
                    continue;
                }

                break;
            }
        }
    }
}
