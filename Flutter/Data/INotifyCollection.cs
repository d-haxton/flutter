using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reactive.Subjects;
using ReactiveUI;

namespace Flutter.Data
{
    public interface INotifyCollection<T> : IEnumerable<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
        Subject<T> ItemsAdded { get; }
        Subject<T> BeforeItemsAdded { get; }
        Subject<T> ItemsRemoved { get; }
        Subject<T> BeforeItemsRemoved { get; }
        Subject<IMoveInfo<T>> BeforeItemsMoved { get; }
        Subject<IMoveInfo<T>> ItemsMoved { get; }
        Subject<NotifyCollectionChangedEventArgs> Changing { get; }
        Subject<NotifyCollectionChangedEventArgs> Changed { get; }
        Subject<int> CountChanging { get; }
        Subject<int> CountChanged { get; }
        Subject<bool> IsEmptyChanged { get; }
        Subject<IReactivePropertyChangedEventArgs<T>> ItemChanging { get; }
        Subject<IReactivePropertyChangedEventArgs<T>> ItemChanged { get; }
    }
}