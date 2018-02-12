using System;
using ReactiveUI;

namespace Flutter.POCOs
{
    public abstract class BaseObject : ReactiveObject
    {
        public string Uid { get; set; }

        protected BaseObject()
        {
            Uid = Guid.NewGuid().ToString();
        }

        protected BaseObject(string uid)
        {
            Uid = uid;
        }
    }
}