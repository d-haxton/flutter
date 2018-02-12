using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Microsoft.Alm.Authentication;

namespace Flutter.Services
{
    public interface ICredentialProvider
    {
        FetchOptions GithubFetchOptions { get; }
    }

    public class CredentialProvider : ICredentialProvider
    {
        private readonly UsernamePasswordCredentials githubCredentials;

        public FetchOptions GithubFetchOptions =>
            new FetchOptions {CredentialsProvider = (_, __, ___) => githubCredentials};

        public CredentialProvider()
        {
            var secrets = new SecretStore("git");
            var auth = new BasicAuthentication(secrets);
            var creds = auth.GetCredentials(new TargetUri("https://github.com"));

            githubCredentials = new UsernamePasswordCredentials
            {
                Username = creds.Username,
                Password = creds.Password
            };
        }
    }
}
