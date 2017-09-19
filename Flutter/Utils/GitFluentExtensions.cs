using Flutter.POCOs;
using Flutter.Services;
using Flutter.Settings;

namespace Flutter.Utils
{
    public static class GitFluentExtensions
    {
        public static GitService BuildService(this GitSettings settings)
        {
            return new GitService(settings);
        }

        public static GitSettings BuildSettings(this GitRepository repository)
        {
            return new GitSettings(repository);
        }
    }
}
