using System;
using System.Windows;
using System.Windows.Controls;
using Flutter.Reactive;
using ReactiveUI;
using StructureMap;

namespace Flutter.View.Controls
{
    public partial class GitBranchTreeView
    {
        public EBranchType BranchType { get; set; }

        public GitBranchTreeView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.RootNodes, v => v.branchesTreeView.ItemsSource);
            this.Bind(ViewModel, vm => vm.Branch, v => v.branchesTreeView.SelectedValue);

            this.WhenAnyValue(x => x.ViewModel)
                .NotNull()
                .Subscribe(vm => vm.BranchType = BranchType);
        }

        private void TreeViewItem_RequestBringIntoView(object _, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }

    public enum EBranchType
    {
        Unknown,
        Local,
        Remote
    }
}
