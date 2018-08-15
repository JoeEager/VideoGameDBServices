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
    public class DeveloperTests
    {
        const int _DeveloperNumber = 4;
        const string _DeveloperName = "TestDeveloper";

        private DeveloperRepository CreateDeveloperTestRepo(string dbName)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext DeveloperContext = new VideogamesContext(options);
            Developers testdeveloper = new Developers();

            for (int i=0; i<_DeveloperNumber;i++)
            {
                testdeveloper.DeveloperName = _DeveloperName+ i.ToString();
                testdeveloper.Id = i;
                DeveloperContext.Developers.Add(testdeveloper);
                DeveloperContext.SaveChanges();
            }

            return new DeveloperRepository(DeveloperContext);
        }

        [TestMethod]
        public void TestDeveloperRepo_GetAll()
        {

            //Arrange
            DeveloperRepository TestRepo = CreateDeveloperTestRepo("DeveloperGetAll"); 

            //Act
            var result = TestRepo.GetAll();
            
            //Assert
            Assert.IsTrue(result.Count()==_DeveloperNumber);

        }

        [TestMethod]
        public async Task TestDeveloperRepo_GetSingle()
        {
            //Arrange
            DeveloperRepository TestRepo = CreateDeveloperTestRepo("DeveloperGetSingle");

            //Act
            var result = await TestRepo.Find(_DeveloperNumber-1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id==(_DeveloperNumber-1));
            Assert.IsTrue(result.DeveloperName == _DeveloperName + (_DeveloperNumber-1).ToString());
        }

        [TestMethod]
        public async Task TestDeveloperRepo_Exists()
        {
            //Arrange
            DeveloperRepository TestRepo = CreateDeveloperTestRepo("DeveloperExists");

            //Act
            var result = await TestRepo.Exist(_DeveloperNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }
    }
}
