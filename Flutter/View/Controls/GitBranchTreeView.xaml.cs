using System.Windows;
using System.Windows.Controls;
using ReactiveUI;

namespace Flutter.View.Controls
{
    /// <summary>
    /// Interaction logic for GitBranchTreeView.xaml
    /// </summary>
    public partial class GitBranchTreeView
    {
        public static readonly DependencyProperty IsLocalBranchesProperty = DependencyProperty.RegisterAttached("IsLocalBranches", typeof(object), typeof(GitBranchTreeView), new PropertyMetadata(default(object)));

        public GitBranchTreeView()
        {
            InitializeComponent();

            ViewModel.IsLocal = true;

            this.OneWayBind(ViewModel, vm => vm.RootNodes, v => v.branchesTreeView.ItemsSource);
        }

        public static object GetIsLocalBranches(UIElement element)
        {
            return (object) element.GetValue(IsLocalBranchesProperty);
        }

        public static void SetIsLocalBranches(UIElement element, object value)
        {
            element.SetValue(IsLocalBranchesProperty, value);
        }
    }
}
