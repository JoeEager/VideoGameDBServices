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
    public class RatingTests
    {
        const int _RatingNumber = 4;
        const string _RatingName = "TestRating";

        private RatingRepository CreateRatingTestRepo(string dbName)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext RatingContext = new VideogamesContext(options);
            Ratings testrating = new Ratings();

            for (int i = 0; i < _RatingNumber; i++)
            {
                testrating.Rating = _RatingName + i.ToString();
                testrating.Id = i;
                RatingContext.Ratings.Add(testrating);
                RatingContext.SaveChanges();
            }

            return new RatingRepository(RatingContext);
        }

        [TestMethod]
        public void TestRatingRepo_GetAll()
        {

            //Arrange
            RatingRepository TestRepo = CreateRatingTestRepo("RatingGetAll");

            //Act
            var result = TestRepo.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == _RatingNumber);

        }

        [TestMethod]
        public async Task TestRatingRepo_GetSingle()
        {
            //Arrange
            RatingRepository TestRepo = CreateRatingTestRepo("RatingGetSingle");

            //Act
            var result = await TestRepo.Find(_RatingNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id == (_RatingNumber - 1));
            Assert.IsTrue(result.Rating == _RatingName + (_RatingNumber - 1).ToString());
        }

        [TestMethod]
        public async Task TestRatingRepo_Exists()
        {
            //Arrange
            RatingRepository TestRepo = CreateRatingTestRepo("RatingExists");

            //Act
            var result = await TestRepo.Exist(_RatingNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }

    }
}
