using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Flutter.DI;
using ReactiveUI;

namespace Flutter.Reactive
{
    public abstract class ViewForUserControl<T> : UserControl, IViewFor<T>, INotifyPropertyChanged where T : class
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private T viewModel;

        protected ViewForUserControl(bool generateViewModel = true)
        {
            if (generateViewModel)
            {
                this.WhenAnyValue(x => x.ViewModel)
                    .Subscribe(vm => DataContext = vm);

                ViewModel = Bootstrap.Container.GetInstance<T>();
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as T;
        }

        public T ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != value)
                {
                    viewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}