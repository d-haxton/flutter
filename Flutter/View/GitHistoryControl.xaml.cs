using System.Threading.Tasks;
using Flutter.Reactive;
using ReactiveUI;

namespace Flutter.View
{
    public partial class GitHistoryControl
    {
        public GitHistoryControl()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.GitLogViewModels, v => v.gitLogDataGrid.ItemsSource);

            rebaseMenuItem.Header = "Rebase {current.Branch} to {here.Branch}";
            mergeMenuItem.Header = "Merge {here.Commit} into {current.Branch}";
            checkoutMenuItem.Header = "Checkout {here.Branch}";
            branchMenuItem.Header = "Branch {here}";
            resetToHereMenuItem.Header = "Reset {current.Branch} to {here.Commit}";
            createTagMenuItem.Header = "Create tag {here.Commit}";

            rebaseMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            mergeMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            checkoutMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            branchMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            resetToHereMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
            createTagMenuItem.Command = ReactiveCommand.CreateFromTask(NeedsImplementing);
        }

        private async Task NeedsImplementing()
        {
            // TODO :: Maybe you want to implement this?
            //await this.ShowMessageAsync("")
        }
    }
}
