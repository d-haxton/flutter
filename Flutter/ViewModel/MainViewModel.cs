using Flutter.Data;
using Flutter.POCOs;
using ReactiveUI;

namespace Flutter.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private readonly DiskRepository disk;

        public DatabaseCollection<GitRepository> Repositories { get; }

        public MainViewModel(DiskRepository diskStorage)
        {
            disk = diskStorage;

            Repositories = new DatabaseCollection<GitRepository>();
        }

        public void CreateRepository(string name, string path)
        {
            //var repository = new GitRepository()
            //{
            //    Name = name,
            //    Path = path
            //};

            //disk.InsertDataModel(repository);
        }
    }
}