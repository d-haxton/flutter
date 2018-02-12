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
        private readonly IGitService gitService;
        private readonly DatabaseCollection<GitRepository> repositories;

        public ObservableCollection<GitTreeItem> RootNodes { get; private set; }

        public GitBranchTreeViewViewModel(IGitService service)
        {
            gitService = service;
            bool isLocal = true;
            RootNodes = new ObservableCollection<GitTreeItem>();
            repositories = new DatabaseCollection<GitRepository>();

            var nodes = gitService.Branches
                .Where(x => x.IsRemote != isLocal)
                .OrderByDescending(x => x.FriendlyName.Count(c => c == '/'))
                .ToArray();

            var paths = nodes.Select(x => x.FriendlyName)
                .Where(x => x.LastIndexOf('/') != -1)
                .Select(x => x.Substring(0, x.LastIndexOf('/')))
                .Distinct().ToList();

            paths.Sort();

            foreach (var path in paths)
            {
                CreatePath(RootNodes, path);
            }

            foreach (var branch in nodes.Where(x => x.FriendlyName.IndexOf('/') == -1))
            {
                RootNodes.Add(new GitTreeBranch(branch));
            }

            foreach (var branch in nodes)
            {
                // TODO :: Create GitTreeBranch instead of folders
                CreatePath(RootNodes, branch.FriendlyName);
            }
        }

        private void CreatePath(IList<GitTreeItem> nodeList, string path)
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
                    node = new GitTreeFolder(folder);
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
