using System;
using Flutter.DI;
using MahApps.Metro.Controls;
using ReactiveUI;

namespace Flutter.Reactive
{
    public abstract class FlutterWindow<T> : MetroWindow where T : ReactiveObject
    {
        protected T ViewModel { get; }

        protected FlutterWindow()
        {
            ViewModel = Bootstrap.Container.GetInstance<T>();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => DataContext = vm);
        }
    }
}