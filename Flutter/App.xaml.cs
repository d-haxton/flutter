using System.Windows;
using Flutter.DI;
using Flutter.View;
using StructureMap;

namespace Flutter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Bootstrap.Container = Container.For<FlutterUIContainer>();

            var app = Bootstrap.Container.GetInstance<MainWindow>();
            app.Show();
        }
    }
}
