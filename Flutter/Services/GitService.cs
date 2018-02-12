using System;
using System.Collections.Generic;
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
        ImmutableArray<Commit> Commits { get; }
    }

    public class GitService : IGitService, IDisposable
    {
        private readonly IGitSettings settings;
        private readonly ICredentialProvider credentialProvider;
        private readonly Repository repository;

        public GitService(IGitSettings settings, ICredentialProvider credentialProvider)
        {
            this.settings = settings;
            this.credentialProvider = credentialProvider;
            repository = new Repository(settings.PathToRepository, settings.Options);
        }

        public ImmutableArray<Branch> Branches => repository.Branches.ToImmutableArray();

        public ImmutableArray<Commit> Commits => repository.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repository.Refs }).ToImmutableArray();

        public void FetchRemotes()
        {
            foreach (var remote in repository.Network.Remotes)
            {
                var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                Commands.Fetch(repository, remote.Name, refSpecs, credentialProvider.GithubFetchOptions, "");
            }
        }

        public void Dispose()
        {
            repository?.Dispose();
        }
    }
}
