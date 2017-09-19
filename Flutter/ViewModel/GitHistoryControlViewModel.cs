using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace Flutter.ViewModel
{
    public class GitHistoryControlViewModel : ReactiveObject
    {
        public ObservableCollection<GitLogDataGridViewModel> GitLogViewModels { get; }

        public GitHistoryControlViewModel()
        {
            GitLogViewModels = new ObservableCollection<GitLogDataGridViewModel>();

            // dumby data for visual effect
            for (var i = 0; i < 100; i++)
            {
                GitLogViewModels.Add(new GitLogDataGridViewModel($"bond/WIN-00{i}", "The name's Bond. James Bond.", DateTime.Now, "James Bond", Guid.NewGuid().ToString().Replace("-", "")));
            }
        }
    }
}
