using Flutter.POCOs;
using LibGit2Sharp;

namespace Flutter.Settings
{
    public class GitSettings
    {
        public string PathToRepository { get; set; }
        public RepositoryOptions Options { get; set; }

        public GitSettings(GitRepository repository)
        {
            PathToRepository = repository.Path;
        }
    }
}
