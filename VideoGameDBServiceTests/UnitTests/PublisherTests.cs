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
    public class PublisherTests
    {
        const int _PublisherNumber = 4;
        const string _PublisherName = "TestPublisher";

        private PublisherRepository CreatePublisherTestRepo(string dbName, bool games=false)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext PublisherContext = new VideogamesContext(options);
            Publishers testpublisher = new Publishers();

            //Check to see if we need to add a game relation to the database
            if (games == true)
            {
                Games testgame = new Games()
                {
                    Title = "TestGameTitle",
                    Id = 0,
                    PublisherName = _PublisherName + (_PublisherNumber-1).ToString()
                };
                PublisherContext.Games.Add(testgame);
                PublisherContext.SaveChanges();
            }
            
            for (int i = 0; i < _PublisherNumber; i++)
            {
                testpublisher.Name = _PublisherName + i.ToString();
                testpublisher.Id = i;
                PublisherContext.Publishers.Add(testpublisher);
                PublisherContext.SaveChanges();
            }

            return new PublisherRepository(PublisherContext);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void TestPublisherRepo_GetAll()
        {

            //Arrange
            PublisherRepository TestRepo = CreatePublisherTestRepo("PublisherGetAll");

            //Act
            var result = TestRepo.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == _PublisherNumber);

        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public async Task TestPublisherRepo_GetSingle()
        {
            //Arrange
            PublisherRepository TestRepo = CreatePublisherTestRepo("PublisherGetSingle");

            //Act
            var result = await TestRepo.Find(_PublisherNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id == (_PublisherNumber - 1));
            Assert.IsTrue(result.Name == _PublisherName + (_PublisherNumber - 1).ToString());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public async Task TestPublisherRepo_Exists()
        {
            //Arrange
            PublisherRepository TestRepo = CreatePublisherTestRepo("PublisherExists");

            //Act
            var result = await TestRepo.Exist(_PublisherNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public async Task TestPublisherRepo_GetGamesByPublisher()
        {
            //Arrange
            PublisherRepository TestRepo = CreatePublisherTestRepo("GamesByPublisher",true);

            //Act
            var result = await TestRepo.GetGamesByPublisher(_PublisherNumber - 1);

            //Assert
            Assert.IsTrue(result.Count()==1);
        }
    }
}

