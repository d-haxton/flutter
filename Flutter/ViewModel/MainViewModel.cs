using System.Threading.Tasks;
using Flutter.Data;
using Flutter.Library.IO;
using Flutter.POCOs;
using ReactiveUI;

namespace Flutter.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IFolderDialog folderDialog;

        public DatabaseCollection<GitRepository> Repositories { get; }

        public MainViewModel(IFolderDialog folderDialog)
        {
            this.folderDialog = folderDialog;

            Repositories = new DatabaseCollection<GitRepository>();
        }

        public void CreateRepository(string name)
        {
            var folder = folderDialog.SelectFolder("Select the git repository folder");
            if (!string.IsNullOrEmpty(folder))
            {
                Repositories.Add(new GitRepository(name, folder));
            }
        }
    }
}