using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Controllers;
using VideoGameDBServices.Models;
using VideoGameDBServices.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameDBServiceTests
{
    [TestClass]
    public class SystemTests
    {
        const int _SystemNumber = 4;
        const string _SystemName = "TestSystem";

        private SystemRepository CreateSystemTestRepo(string dbName, bool games = false)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext SystemContext = new VideogamesContext(options);
            Systems testsystem = new Systems();

            //Check to see if we need to add a game relation to the database
            if (games == true)
            {
                Games testgame = new Games()
                {
                    Title = "TestGameTitle",
                    Id = 0,
                    SystemName = _SystemName + (_SystemNumber - 1).ToString()
                };
                SystemContext.Games.Add(testgame);
                SystemContext.SaveChanges();
            }

            for (int i = 0; i < _SystemNumber; i++)
            {
                testsystem.SystemName = _SystemName + i.ToString();
                testsystem.Id = i;
                SystemContext.Systems.Add(testsystem);
                SystemContext.SaveChanges();
            }

            return new SystemRepository(SystemContext);
        }

        [TestMethod]
        public void TestSystemRepo_GetAll()
        {

            //Arrange
            SystemRepository TestRepo = CreateSystemTestRepo("SystemGetAll");

            //Act
            var result = TestRepo.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == _SystemNumber);

        }

        [TestMethod]
        public async Task TestSystemRepo_GetSingle()
        {
            //Arrange
            SystemRepository TestRepo = CreateSystemTestRepo("SystemGetSingle");

            //Act
            var result = await TestRepo.Find(_SystemNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id == (_SystemNumber - 1));
            Assert.IsTrue(result.SystemName == _SystemName + (_SystemNumber - 1).ToString());
        }

        [TestMethod]
        public async Task TestSystemRepo_Exists()
        {
            //Arrange
            SystemRepository TestRepo = CreateSystemTestRepo("SystemExists");

            //Act
            var result = await TestRepo.Exist(_SystemNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestSystemRepo_GetGamesBySystem()
        {
            //Arrange
            SystemRepository TestRepo = CreateSystemTestRepo("GamesBySystem", true);

            //Act
            var result = await TestRepo.GetGamesBySystem(_SystemNumber - 1);

            //Assert
            Assert.IsTrue(result.Count() == 1);
        }
    }
}
