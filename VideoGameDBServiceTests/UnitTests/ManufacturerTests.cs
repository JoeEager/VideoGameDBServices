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
    public class ManufacturerTests
    {
        const int _ManufacturerNumber = 4;
        const string _ManufacturerName = "TestManufacturer";

        private ManufacturerRepository CreateManufacturerTestRepo(string dbName)
        {
            var options = new DbContextOptionsBuilder<VideogamesContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            VideogamesContext ManufacturerContext = new VideogamesContext(options);
            Manufacturers testmanufacturer = new Manufacturers();

            for (int i = 0; i < _ManufacturerNumber; i++)
            {
                testmanufacturer.Name = _ManufacturerName + i.ToString();
                testmanufacturer.Id = i;
                ManufacturerContext.Manufacturers.Add(testmanufacturer);
                ManufacturerContext.SaveChanges();
            }

            return new ManufacturerRepository(ManufacturerContext);
        }

        [TestMethod]
        public void TestManufacturerRepo_GetAll()
        {

            //Arrange
            ManufacturerRepository TestRepo = CreateManufacturerTestRepo("ManufacturerGetAll");

            //Act
            var result = TestRepo.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == _ManufacturerNumber);

        }

        [TestMethod]
        public async Task TestManufacturerRepo_GetSingle()
        {
            //Arrange
            ManufacturerRepository TestRepo = CreateManufacturerTestRepo("ManufacturerGetSingle");

            //Act
            var result = await TestRepo.Find(_ManufacturerNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result.Id == (_ManufacturerNumber - 1));
            Assert.IsTrue(result.Name == _ManufacturerName + (_ManufacturerNumber - 1).ToString());
        }

        [TestMethod]
        public async Task TestManufactuerRepo_Exists()
        {
            //Arrange
            ManufacturerRepository TestRepo = CreateManufacturerTestRepo("ManufacturerExists");

            //Act
            var result = await TestRepo.Exist(_ManufacturerNumber - 1);  //Subtract one since it starts at 0

            //Assert
            Assert.IsTrue(result);
        }
    }
}
