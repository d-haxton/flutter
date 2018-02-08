﻿using System;
using System.Windows.Controls;
using Flutter.DI;
using MahApps.Metro.Controls;
using ReactiveUI;

namespace Flutter.Reactive
{
    public abstract class FlutterWindow<T> : MetroWindow, IViewFor where T : ReactiveObject
    {
        protected T ViewModel { get; private set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as T;
        }

        protected FlutterWindow()
        {
            ViewModel = Bootstrap.Container.GetInstance<T>();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => DataContext = vm);
        }
    }
}