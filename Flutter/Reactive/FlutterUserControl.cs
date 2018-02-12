using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Flutter.Annotations;
using Flutter.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using StructureMap.Attributes;
using IContainer = StructureMap.IContainer;

namespace Flutter.Reactive
{
    public abstract class FlutterUserControl : UserControl, IContainerController, INotifyPropertyChanged
    {
        private IContainer scopedContainer;

        [Reactive]
        [SetterProperty]
        public IContainer ScopedContainer
        {
            get => scopedContainer;
            set
            {
                scopedContainer = value;
                OnPropertyChanged(nameof(ScopedContainer));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class FlutterUserControl<T> : FlutterUserControl, IViewFor where T : ReactiveObject
    {
        private T viewModel;

        protected T ViewModel
        {
            get => viewModel;
            private set
            {
                viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as T;
        }

        protected FlutterUserControl()
        {
            this.WhenAnyValue(x => x.ScopedContainer)
                .NotNull()
                .Subscribe(container =>
                {
                    ViewModel = container.GetInstance<T>();
                });

            this.WhenAnyValue(x => x.ViewModel)
                .NotNull()
                .Subscribe(vm => DataContext = vm);

            this.Events().LayoutUpdated.Subscribe(() =>
            {
                var parent = this.FindParent<FlutterUserControl>() ?? (IContainerController)this.FindParent<FlutterWindow>();
                parent?.WhenAnyValue(x => x.ScopedContainer)
                    .NotNull()
                    .Where(_ => ScopedContainer == null)
                    .Subscribe(container =>
                {
                    ScopedContainer = container;
                    //ViewModel = ScopedContainer.GetInstance<T>();
                });
            });
        }
    }
}