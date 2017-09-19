using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flutter.ViewModel
{
    public class GitLogDataGridViewModel : ReactiveObject
    {
        [Reactive] public string Branch { get; private set; }
        [Reactive] public string CommitMessage { get; private set; }
        [Reactive] public DateTime Date { get; private set; }
        [Reactive] public string Author { get; private set; }
        [ObservableAsProperty] public string CommitHashTruncated { get; private set; }
        [Reactive] public string CommitHash { get; private set; }

        public GitLogDataGridViewModel(string branch, string commitMessage, DateTime date, string author, string commitHashTruncated)
        {
            Branch = branch;
            CommitMessage = commitMessage;
            Date = date;
            Author = author;
            CommitHash = commitHashTruncated;

            this.WhenAnyValue(x => x.CommitHash)
                .Select(x => x.Substring(0, 7))
                .ToPropertyEx(this, x => x.CommitHashTruncated);
        }
    }
}
