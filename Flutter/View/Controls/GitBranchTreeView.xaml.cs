using System.Windows;
using System.Windows.Controls;
using ReactiveUI;

namespace Flutter.View.Controls
{
    public partial class GitBranchTreeView
    {

        public GitBranchTreeView()
        {
            InitializeComponent();

            ViewModel.IsLocal = true;
            this.OneWayBind(ViewModel, vm => vm.RootNodes, v => v.branchesTreeView.ItemsSource);
        }

        public static bool GetIsLocalBranches(UIElement element)
        {
            return (bool)element.GetValue(IsLocalBranchesProperty);
        }

        public static void SetIsLocalBranches(UIElement element, bool value)
        {
            element.SetValue(IsLocalBranchesProperty, value);
        }

        public static readonly DependencyProperty IsLocalBranchesProperty =
            DependencyProperty.RegisterAttached("IsLocalBranches", typeof(bool), typeof(GitBranchTreeView), new PropertyMetadata(false));
    }
}
