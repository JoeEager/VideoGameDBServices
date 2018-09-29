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
    public class GameTests
    {
        const int _GameNumber = 4;
        const string _GameTitle = "TestGame";
        const string _GameDescription = "TestDescription";
        const string _GameDeveloper = "TestDeveloper";

        private GameRepository CreateGameTestRepo(string dbName)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext GameContext = new VideogamesContext(options);
            Games testgame = new Games();

            for (int i = 0; i < _GameNumber; i++)
            {
                testgame.Title = _GameTitle + i.ToString();
                testgame.Id = i;
                testgame.Description = _GameDescription + i.ToString();
                testgame.DeveloperName = _GameDeveloper + i.ToString();
                GameContext.Games.Add(testgame);
                GameContext.SaveChanges();
            }

            return new GameRepository(GameContext);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void TestGameRepo_GetAll()
        {

            //Arrange
            GameRepository TestRepo = CreateGameTestRepo("GameGetAll");

            //Act
            var result = TestRepo.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == _GameNumber);

        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public async Task TestGameRepo_GetSingle()
        {
            //Arrange
            GameRepository TestRepo = CreateGameTestRepo("GameGetSingle");

            //Act
            var result = await TestRepo.Find(_GameNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id == (_GameNumber - 1));
            Assert.IsTrue(result.Title == _GameTitle + (_GameNumber - 1).ToString());
            Assert.IsTrue(result.Description == _GameDescription + (_GameNumber - 1).ToString());
            Assert.IsTrue(result.DeveloperName == _GameDeveloper + (_GameNumber - 1).ToString());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public async Task TestGameRepo_Exists()
        {
            //Arrange
            GameRepository TestRepo = CreateGameTestRepo("GameExists");

            //Act
            var result = await TestRepo.Exist(_GameNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }
    }
}
