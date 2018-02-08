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
}
