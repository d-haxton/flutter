using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flutter.Services;
using ReactiveUI;

namespace Flutter.ViewModel
{
    public class GitMenuControlViewModel : ReactiveObject
    {
        public ICommand Fetch { get; set; }

        public GitMenuControlViewModel(IGitService gitService)
        {
            Fetch = ReactiveCommand.Create(gitService.FetchRemotes);
        }
    }
}
