using System.Collections.Immutable;
using System.Linq;
using Flutter.Settings;
using LibGit2Sharp;
using StructureMap.Attributes;

namespace Flutter.Services
{
    public interface IGitService
    {
        ImmutableArray<Branch> Branches { get; }
        void FetchRemotes();
    }

    public class GitService : IGitService
    {
        private readonly IGitSettings settings;
        private readonly ICredentialProvider credentialProvider;

        public GitService(IGitSettings settings, ICredentialProvider credentialProvider)
        {
            this.settings = settings;
            this.credentialProvider = credentialProvider;
        }

        private Repository BuildRepository()
        {
            return new Repository(settings.PathToRepository, settings.Options);
        }

        public ImmutableArray<Branch> Branches
        {
            get
            {
                using (var repo = BuildRepository())
                {
                    return repo.Branches.ToImmutableArray();
                }
            }
        }

        public void FetchRemotes()
        {
            using (var repo = BuildRepository())
            {
                foreach (var remote in repo.Network.Remotes)
                {
                    var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                    Commands.Fetch(repo, remote.Name, refSpecs, credentialProvider.GithubFetchOptions, "");
                }
            }
        }
    }
}
