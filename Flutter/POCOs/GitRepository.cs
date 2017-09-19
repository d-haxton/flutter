using System;
using ReactiveUI;

namespace Flutter.POCOs
{
    public class GitRepository : BaseObject
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public GitRepository()
        {

        }

        public GitRepository(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }

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
