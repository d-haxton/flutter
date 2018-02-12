using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Flutter.Services;
using ReactiveUI.Legacy;
using StructureMap;
using StructureMap.Attributes;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace Flutter.View
{
    /// <summary>
    /// Interaction logic for GitMenuControl.xaml
    /// </summary>
    public partial class GitMenuControl
    {
        private ICommand FetchCommand { get; } = ReactiveCommand.Create(() => { });
        private ICommand PullFastForwardPossible { get; } = ReactiveCommand.Create(() => { });
        private ICommand PullFastForwardOnly { get; } = ReactiveCommand.Create(() => { });
        private ICommand PullRebase { get; } = ReactiveCommand.Create(() => { });

        public GitMenuControl()
        {
            InitializeComponent();

            pullButton.ItemsSource = BuildPullMenuItems();
        }


        private IEnumerable<MenuItem> BuildPullMenuItems()
        {
            var listOfItems = new Dictionary<string, ICommand>
            {
                { "Fetch All", FetchCommand},
                { "Pull (fast-forward if possible)", PullFastForwardPossible},
                { "Pull (fast-forward only)", PullFastForwardOnly},
                { "Pull (rebase)", PullRebase}
            };

            foreach (var pullAction in listOfItems)
            {
                yield return new MenuItem
                {
                    Header = pullAction.Key,
                    Command = pullAction.Value
                };
            }
        }
    }
}
