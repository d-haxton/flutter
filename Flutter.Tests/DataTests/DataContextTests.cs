using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Flutter.Data;
using Flutter.DI;
using Flutter.POCOs;
using Flutter.Settings;
using StructureMap;
using Xunit;

namespace Flutter.Tests.DataTests
{
    public class DataContextTests
    {
        public DataContextTests()
        {
            Bootstrap.Container = Container.For<FlutterUIContainer>();
            var disk = new DatabaseSettings(@"unittest.db");
            Bootstrap.Container.Inject(disk);
        }

        [Fact]
        public void TestAddRemoveCollection()
        {
            var poco = new GitRepository
            {
                Name = "xyz",
                Path = "123"
            };

            var dbCollection = new DatabaseCollection<GitRepository>();
            var dbCount = dbCollection.Count;

            dbCollection.Add(poco);

            Assert.Equal(dbCollection.Count, dbCount + 1);

            var poco2 = new GitRepository
            {
                Name = "foo",
                Path = "bar"
            };

            Bootstrap.Container.GetInstance<DiskRepository>().InsertDataModel(poco2);
            Assert.Equal(dbCollection.Count, dbCount + 2);
        }
    }
}
