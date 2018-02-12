using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using StructureMap;

namespace Flutter.View.Controls
{
    public partial class GitBranchTreeView
    {
        public GitBranchTreeView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.RootNodes, v => v.branchesTreeView.ItemsSource);
        }

        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}
