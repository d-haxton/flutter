using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Subjects;
using Flutter.DI;
using Flutter.POCOs;
using ReactiveUI;
using PropertyChangingEventArgs = ReactiveUI.PropertyChangingEventArgs;
using PropertyChangingEventHandler = ReactiveUI.PropertyChangingEventHandler;

namespace Flutter.Data
{
    /// <summary>
    /// reactive database collection with predicate logic to only load into memory what you want to track
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DatabaseCollection<T> : IReactiveCollection<T>, IList<T>, INotifyCollection<T> where T : BaseObject
    {
        public static readonly Dictionary<Type, List<DatabaseCollection<T>>> DatabaseCollectionByType = new Dictionary<Type, List<DatabaseCollection<T>>>();

        public Expression<Func<T, bool>> Predicate { get; }

        public DatabaseCollection(Expression<Func<T, bool>> predicate = null)
        {
            Predicate = predicate;
            Hook(this);
        }

        private static void Hook(DatabaseCollection<T> context)
        {
            var type = typeof(T);
            if (!DatabaseCollectionByType.ContainsKey(type))
            {
                DatabaseCollectionByType[type] = new List<DatabaseCollection<T>>();
            }

            DatabaseCollectionByType[type].Add(context);
        }

        Subject<T> INotifyCollection<T>.BeforeItemsAdded { get; } = new Subject<T>();
        Subject<T> INotifyCollection<T>.ItemsRemoved { get; } = new Subject<T>();
        Subject<T> INotifyCollection<T>.BeforeItemsRemoved { get; } = new Subject<T>();
        Subject<IMoveInfo<T>> INotifyCollection<T>.BeforeItemsMoved { get; } = new Subject<IMoveInfo<T>>();
        Subject<IMoveInfo<T>> INotifyCollection<T>.ItemsMoved { get; } = new Subject<IMoveInfo<T>>();
        Subject<NotifyCollectionChangedEventArgs> INotifyCollection<T>.Changing { get; } = new Subject<NotifyCollectionChangedEventArgs>();
        Subject<NotifyCollectionChangedEventArgs> INotifyCollection<T>.Changed { get; } = new Subject<NotifyCollectionChangedEventArgs>();
        Subject<int> INotifyCollection<T>.CountChanging { get; } = new Subject<int>();
        Subject<int> INotifyCollection<T>.CountChanged { get; } = new Subject<int>();
        Subject<bool> INotifyCollection<T>.IsEmptyChanged { get; } = new Subject<bool>();

        public IObservable<Unit> ShouldReset => throw new NotImplementedException();

        Subject<IReactivePropertyChangedEventArgs<T>> INotifyCollection<T>.ItemChanging { get; } = new Subject<IReactivePropertyChangedEventArgs<T>>();
        Subject<IReactivePropertyChangedEventArgs<T>> INotifyCollection<T>.ItemChanged { get; } = new Subject<IReactivePropertyChangedEventArgs<T>>();
        Subject<T> INotifyCollection<T>.ItemsAdded { get; } = new Subject<T>();

        public IObservable<T> ItemsAdded => ((INotifyCollection<T>)this).ItemsAdded;
        public IObservable<T> BeforeItemsAdded => ((INotifyCollection<T>) this).BeforeItemsAdded;
        public IObservable<T> ItemsRemoved => ((INotifyCollection<T>)this).ItemsRemoved;
        public IObservable<T> BeforeItemsRemoved => ((INotifyCollection<T>)this).BeforeItemsRemoved;
        public IObservable<IMoveInfo<T>> BeforeItemsMoved => ((INotifyCollection<T>)this).BeforeItemsMoved;
        public IObservable<IMoveInfo<T>> ItemsMoved => ((INotifyCollection<T>)this).ItemsMoved;
        public IObservable<NotifyCollectionChangedEventArgs> Changing => ((INotifyCollection<T>)this).Changing;
        public IObservable<NotifyCollectionChangedEventArgs> Changed => ((INotifyCollection<T>)this).Changed;
        public IObservable<int> CountChanging => ((INotifyCollection<T>)this).CountChanging;
        public IObservable<int> CountChanged => ((INotifyCollection<T>)this).CountChanged;
        public IObservable<bool> IsEmptyChanged => ((INotifyCollection<T>)this).IsEmptyChanged;
        public IObservable<IReactivePropertyChangedEventArgs<T>> ItemChanging => ((INotifyCollection<T>)this).ItemChanging;
        public IObservable<IReactivePropertyChangedEventArgs<T>> ItemChanged => ((INotifyCollection<T>)this).ItemChanged;
        public bool ChangeTrackingEnabled { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            using (var disk = Bootstrap.Container.GetInstance<DiskRepository>())
            {
                return disk.GetDataModel<T>().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        void IReactiveObject.RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        void IReactiveObject.RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        public void Add(T item)
        {
            using (var disk = Bootstrap.Container.GetInstance<DiskRepository>())
            {
                disk.InsertDataModel(item);
            }
        }

        public void Clear()
        {
            using (var disk = Bootstrap.Container.GetInstance<DiskRepository>())
            {
                disk.DeleteDataModel(Predicate);
            }
        }

        public bool Contains(T item)
        {
            using (var disk = Bootstrap.Container.GetInstance<DiskRepository>())
            {
                return disk.ContainsDataModel(item);
            }
        }


        public bool Remove(T item)
        {
            using (var disk = Bootstrap.Container.GetInstance<DiskRepository>())
            {
                return disk.DeleteDataModel(item);
            }
        }

        public int Count
        {
            get
            {
                using (var disk = Bootstrap.Container.GetInstance<DiskRepository>())
                {
                    return disk.GetDataModelCount<T>();
                }
            }
        }

        public bool IsReadOnly => false;

        IDisposable IReactiveNotifyCollectionChanged<T>.SuppressChangeNotifications() => throw new NotImplementedException();
        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();
        int IList<T>.IndexOf(T item) => throw new NotImplementedException();

        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();

        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();
        void IReactiveCollection<T>.Reset() => throw new NotImplementedException();

        T IList<T>.this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
