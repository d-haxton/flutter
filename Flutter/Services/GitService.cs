using System.Collections.Immutable;
using Flutter.Settings;
using LibGit2Sharp;

namespace Flutter.Services
{
    public class GitService
    {
        private Repository repository;

        public GitService(GitSettings settings)
        {
            repository = new Repository(settings.PathToRepository);
            //repository = new Repository(gitSettings.LocalPathToRepository, gitSettings.Options);
        }

        public ImmutableArray<Branch> Branches()
        {
            return repository.Branches.ToImmutableArray();
        }
    }
}
