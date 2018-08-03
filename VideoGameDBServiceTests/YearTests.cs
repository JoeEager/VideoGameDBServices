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
    public class YearTests
    {
        const int _YearNumber = 4;
        const string _YearName = "TestYear";

        private YearRepository CreateYearTestRepo(string dbName, bool games = false)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext YearContext = new VideogamesContext(options);
            Years testyear = new Years();

            //Check to see if we need to add a game relation to the database
            if (games == true)
            {
                Games testgame = new Games()
                {
                    Title = "TestGameTitle",
                    Id = 0,
                    Year = _YearName + (_YearNumber - 1).ToString()
                };
                YearContext.Games.Add(testgame);
                YearContext.SaveChanges();
            }

            for (int i = 0; i < _YearNumber; i++)
            {
                testyear.Year = _YearName + i.ToString();
                testyear.Id = i;
                YearContext.Years.Add(testyear);
                YearContext.SaveChanges();
            }

            return new YearRepository(YearContext);
        }

        [TestMethod]
        public void TestYearRepo_GetAll()
        {

            //Arrange
            YearRepository TestRepo = CreateYearTestRepo("YearGetAll");

            //Act
            var result = TestRepo.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == _YearNumber);

        }

        [TestMethod]
        public async Task TestYearRepo_GetSingle()
        {
            //Arrange
            YearRepository TestRepo = CreateYearTestRepo("YearGetSingle");

            //Act
            var result = await TestRepo.Find(_YearNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id == (_YearNumber - 1));
            Assert.IsTrue(result.Year == _YearName + (_YearNumber - 1).ToString());
        }

        [TestMethod]
        public async Task TestSystemRepo_Exists()
        {
            //Arrange
            YearRepository TestRepo = CreateYearTestRepo("YearExists");

            //Act
            var result = await TestRepo.Exist(_YearNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestYearRepo_GetGamesByYear()
        {
            //Arrange
            YearRepository TestRepo = CreateYearTestRepo("GamesByYear", true);

            //Act
            var result = await TestRepo.GetGamesByYear(_YearNumber - 1);

            //Assert
            Assert.IsTrue(result.Count() == 1);

        }
    }
}
