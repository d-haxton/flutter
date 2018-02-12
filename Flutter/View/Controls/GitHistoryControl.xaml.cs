using System.Threading.Tasks;
using Flutter.Reactive;
using ReactiveUI;
using StructureMap;

namespace Flutter.View
{
    public partial class GitHistoryControl
    {
        public GitHistoryControl()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.GitCommits, v => v.gitLogDataGrid.ItemsSource);
            //this.OneWayBind(ViewModel, vm => vm.Commits, v => v.gitLogDataGrid.SelectedItems);

            rebaseMenuItem.Header = "Rebase {current.Branch} to {here.Branch}";
            mergeMenuItem.Header = "Merge {here.Commits} into {current.Branch}";
            checkoutMenuItem.Header = "Checkout {here.Branch}";
            branchMenuItem.Header = "Branch {here}";
            resetToHereMenuItem.Header = "Reset {current.Branch} to {here.Commits}";
            createTagMenuItem.Header = "Create tag {here.Commits}";

            rebaseMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            mergeMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            checkoutMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            branchMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            resetToHereMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            createTagMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);

            this.WhenAnyValue(x => x.ViewModel)
                .NotNull()
                .Subscribe(() =>
                {
                    gitLogDataGrid.SelectedValue = ViewModel.SelectedCommits;
                });
        }

        private async Task NeedsImplementing()
        {
            // TODO :: Maybe you want to implement this?
            //await this.ShowMessageAsync("")
        }
    }
}
