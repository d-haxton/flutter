using System.Xaml;
using Flutter.IO;
using Flutter.Library.IO;
using Flutter.Settings;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Flutter.DI
{
    public class FlutterUIContainer : Registry
    {
        public FlutterUIContainer()
        {
            //For<IMainViewModel>().Use<MainViewModel>();
            For<DatabaseSettings>().Use<DatabaseSettings>().Ctor<string>().Is("flutter.db").Singleton();
            For<IFolderDialog>().Use<FolderDialog>();
        }
    }

    public static class Bootstrap
    {
        public static IContainer Container { get; set; }
    }
}
