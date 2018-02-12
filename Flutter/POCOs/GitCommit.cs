using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flutter.POCOs
{
    public class GitCommit : ReactiveObject
    {
        [Reactive] public string CommitMessage { get; private set; }
        [Reactive] public string Date { get; private set; }
        [Reactive] public string Author { get; private set; }
        [Reactive] public string CommitHash { get; private set; }
        [ObservableAsProperty] public string CommitHashTruncated { get; private set; }

        public GitCommit(string commitMessage, DateTime date, string author, string commitHash)
        {
            CommitMessage = commitMessage;
            Date = date.ToString("d MMM yyyy HH:mm");
            Author = author;
            CommitHash = commitHash;

            this.WhenAnyValue(x => x.CommitHash)
                .Select(x => x.Substring(0, 7))
                .ToPropertyEx(this, x => x.CommitHashTruncated);
        }
    }
}
