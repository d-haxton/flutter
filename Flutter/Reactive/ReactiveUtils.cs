using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.Reactive
{
    public static class ReactiveUtils
    {
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action onNext)
        {
            return source.Subscribe(_ => onNext());
        }
    }
}
