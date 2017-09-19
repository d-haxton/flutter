using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Dragablz;
using Flutter.DI;
using Flutter.POCOs;
using ReactiveUI;
using Flutter.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace Flutter.View
{
    public partial class MainWindow
    {
        private readonly ObservableCollection<TabItem> tabItems = new ObservableCollection<TabItem>();

        public MainWindow()
        {
            InitializeComponent();
            tabControl.ClosingItemCallback = ClosingItemCallback;
            openRepositoryItem.Command = ReactiveCommand.CreateFromTask(async () => await NewItemFactory());

            tabControl.ItemsSource = tabItems;
            ViewModel.Repositories
                .CountChanged
                .StartWith(0)
                .Subscribe(() =>
                {
                    tabItems.Clear();
                    foreach (var tabItem in ViewModel.Repositories.Select(x => BuildTabItem(x.Name)))
                    {
                        tabItems.Add(tabItem);
                    }
                });
        }

        private async void ClosingItemCallback(ItemActionCallbackArgs<TabablzControl> args)
        {
            args.Cancel();

            var nameOfRepo = ((TabItem) args.DragablzItem.Content).Header as string;
            var deleteRepo = await this.ShowMessageAsync("Delete repository",
                $"Are you sure you want to delete {nameOfRepo}",
                MessageDialogStyle.AffirmativeAndNegative);

            if (deleteRepo == MessageDialogResult.Affirmative)
            {
                var item = ViewModel.Repositories.FirstOrDefault(x => x.Name == nameOfRepo);
                ViewModel.Repositories.Remove(item);
            }
        }

        private async Task NewItemFactory()
        {
            var repo = await this.ShowInputAsync("New repository",
                "Please enter a new repository name");
            if (!string.IsNullOrEmpty(repo))
            {
                // todo :: path
                ViewModel.Repositories.Add(new GitRepository(repo, ""));
            }
        }

        private static TabItem BuildTabItem(string repositoryName)
        {
            return new TabItem
            {
                Content = Bootstrap.Container.GetInstance<RepositoryControl>(),
                Header = repositoryName
            };
        }
    }
}