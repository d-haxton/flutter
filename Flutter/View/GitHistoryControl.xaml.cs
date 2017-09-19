using System;
using System.Collections.Generic;
using System.Linq;
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
using Flutter.Reactive;
using Flutter.ViewModel;
using ReactiveUI;

namespace Flutter.View
{
    /// <summary>
    /// Interaction logic for GitHistoryControl.xaml
    /// </summary>
    public partial class GitHistoryControl : ViewForUserControl<GitHistoryControlViewModel>
    {
        public GitHistoryControl()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.GitLogViewModels, v => v.gitLogDataGrid.ItemsSource);
        }
    }
}
