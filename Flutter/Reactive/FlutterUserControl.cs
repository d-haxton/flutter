using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Flutter.DI;
using ReactiveUI;

namespace Flutter.Reactive
{
    public abstract class FlutterUserControl<T> : UserControl, IViewFor where T : ReactiveObject
    {
        protected T ViewModel { get; private set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as T;
        }

        protected FlutterUserControl()
        {
            ViewModel = Bootstrap.Container.GetInstance<T>();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => DataContext = vm);
        }
    }
}