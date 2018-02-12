using System;
using System.Windows.Controls;
using Flutter.DI;
using Flutter.Utils;
using MahApps.Metro.Controls;
using ReactiveUI;
using StructureMap;

namespace Flutter.Reactive
{
    public abstract class FlutterWindow : MetroWindow, IContainerController
    {
        public IContainer ScopedContainer { get; set; }
    }

    public abstract class FlutterWindow<T> : FlutterWindow, IViewFor where T : ReactiveObject
    {
        protected T ViewModel { get; private set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as T;
        }

        protected FlutterWindow()
        {
            //var parent = this.FindParent<FlutterUserControl>() ?? (IContainerController)this.FindParent<FlutterWindow>();
            ScopedContainer = Bootstrap.Container;
            ViewModel = ScopedContainer.GetInstance<T>();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => DataContext = vm);
        }
    }
}