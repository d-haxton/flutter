using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Flutter.POCOs;
using Flutter.Settings;
using LiteDB;

namespace Flutter.Data
{
    public class DiskRepository : IDisposable
    {
        private readonly LiteDatabase data;
        private static readonly Dictionary<string, LiteDatabase> databases = new Dictionary<string, LiteDatabase>();

        public DiskRepository(DatabaseSettings settings)
        {
            if (!databases.TryGetValue(settings.DatabaseName, out data))
            {
                databases[settings.DatabaseName] = data = new LiteDatabase(settings.DatabaseName);
            }
        }

        public IEnumerable<T> GetDataModel<T>(Expression<Func<T, bool>> predicate = null) where T : BaseObject
        {
            return data
                .GetDataModelCollection<T>()
                .FindDataModel(predicate);
        }

        public int GetDataModelCount<T>(Expression<Func<T, bool>> predicate = null) where T : BaseObject
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            return data
                .GetDataModelCollection<T>()
                .Count(predicate);
        }

        public void UpdateDataModel<T>(T model) where T : BaseObject
        {
            data.GetDataModelCollection<T>()
                .Update(model);
        }

        public void UpdateDataModel<T>(IEnumerable<T> models) where T : BaseObject
        {
            foreach (var model in models)
            {
                UpdateDataModel(model);
            }
        }

        public bool DeleteDataModel<T>(T model) where T : BaseObject
        {
            foreach (var collection in GetCollections<T>())
            {
                if (collection.Predicate?.Compile().Invoke(model) ?? true)
                {
                    collection.BeforeItemsRemoved.OnNext(model);
                    collection.CountChanging.OnNext(collection.Count() - 1);
                    collection.Changing.OnNext(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new [] { model }));
                }
            }

            var result = data.GetDataModelCollection<T>()
                .DeleteDataModel(model);

            foreach (var collection in GetCollections<T>())
            {
                if (collection.Predicate?.Compile().Invoke(model) ?? true)
                {
                    collection.ItemsRemoved.OnNext(model);
                    collection.CountChanged.OnNext(collection.Count());
                    collection.Changed.OnNext(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new[] { model }));

                    if (!collection.Any())
                    {
                        collection.IsEmptyChanged.OnNext(true);
                    }
                }
            }

            return result;
        }

        public bool DeleteDataModel<T>(Expression<Func<T, bool>> predicate) where T : BaseObject
        {
            var models = data.GetDataModelCollection<T>().Find(predicate).ToArray();
            var result = models.Select(DeleteDataModel).All(x => x);
            return result;
        }

        public void InsertDataModel<T>(T model) where T : BaseObject
        {
            foreach (var collection in GetCollections<T>())
            {
                if (collection.Predicate?.Compile().Invoke(model) ?? true)
                {
                    collection.BeforeItemsAdded.OnNext(model);
                    collection.CountChanging.OnNext(collection.Count() + 1);
                }
            }

            data.GetDataModelCollection<T>()
                .Insert(model);

            foreach (var collection in GetCollections<T>())
            {
                if (collection.Predicate?.Compile().Invoke(model) ?? true)
                {
                    collection.ItemsAdded.OnNext(model);
                    collection.CountChanged.OnNext(collection.Count());
                }
            }
        }

        public bool ContainsDataModel<T>(T item) where T : BaseObject
        {
            return data.GetDataModelCollection<T>()
                .Exists(x => item.Uid == x.Uid);
        }

        public void InsertDataModel<T>(IEnumerable<T> models) where T : BaseObject
        {
            var modelsArray = models.ToArray();

            foreach (var collection in GetCollections<T>())
            {
                foreach (var model in modelsArray)
                {
                    if (collection.Predicate?.Compile().Invoke(model) ?? true)
                    {
                        collection.BeforeItemsAdded.OnNext(model);
                        collection.CountChanging.OnNext(collection.Count() + 1);
                    }
                }
            }

            data.GetDataModelCollection<T>()
                .Insert(modelsArray);

            foreach(var collection in GetCollections<T>())
            {
                foreach (var model in modelsArray)
                {
                    if (collection.Predicate?.Compile().Invoke(model) ?? true)
                    {
                        collection.ItemsAdded.OnNext(model);
                        collection.CountChanged.OnNext(collection.Count());
                    }
                }
            }
        }

        private IEnumerable<INotifyCollection<T>> GetCollections<T>() where T : BaseObject
        {
            var type = typeof(T);
            if (DatabaseCollection<T>.DatabaseCollectionByType.ContainsKey(type))
            {
                foreach (DatabaseCollection<T> databaseCollection in DatabaseCollection<T>.DatabaseCollectionByType[type])
                {
                    yield return databaseCollection;
                }
            }
        }

        public void Dispose()
        {
            //data?.Dispose();
        }
    }
}
