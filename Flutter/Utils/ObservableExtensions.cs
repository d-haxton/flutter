using System;

namespace Flutter.Utils
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action action)
        {
            return observable.Subscribe(_ => action());
        }
    }
}
