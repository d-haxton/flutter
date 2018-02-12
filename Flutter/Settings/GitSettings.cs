using Flutter.POCOs;
using LibGit2Sharp;

namespace Flutter.Settings
{
    public interface IGitSettings
    {
        string PathToRepository { get; set; }
        RepositoryOptions Options { get; set; }
    }

    public class GitSettings : IGitSettings
    {
        public string PathToRepository { get; set; }
        public RepositoryOptions Options { get; set; }

        public GitSettings(GitRepository repository)
        {
            PathToRepository = repository.Path;
            Options = new RepositoryOptions()
            {
                // TODO :: ?
            };
        }
    }
}
