using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Flutter.POCOs;
using LiteDB;

namespace Flutter.Data
{
    public static class LiteDatabaseExtensions
    {
        public static LiteCollection<T> GetDataModelCollection<T>(this LiteDatabase db) where T : BaseObject
        {
            return db.GetCollection<T>(nameof(T));
        }

        public static IEnumerable<T> FindDataModel<T>(this LiteCollection<T> collection, Expression<Func<T, bool>> predicate = null) where T : BaseObject
        {
            return predicate == null ? collection.FindAll() : collection.Find(predicate);
        }

        public static bool DeleteDataModel<T>(this LiteCollection<T> collection, T item) where T : BaseObject
        {
            return collection.Delete(t => t.Uid == item.Uid) >= 1;
        }

        public static bool DeleteDataModel<T>(this LiteCollection<T> collection, IEnumerable<T> items) where T : BaseObject
        {
            return items.Select(collection.DeleteDataModel<T>).All(x => x);
        }
    }
}
