using AuthDynamic.Controllers;
using AutoMapper;
using DataAccessLayer.Model;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDynamic.Testing
{
    [TestClass]
    public class Test
    {
        private readonly Mock<SampleDBContext> mock;
        private readonly Mock<IUserData> mockUserdata;
        private readonly Mock<IMapper> mockMapper;
        private readonly HomeController homeController;

        public Test()
        {
            mock = new Mock<SampleDBContext>();
            mockUserdata = new Mock<IUserData>();
            mockMapper = new Mock<IMapper>();
            homeController = new HomeController(mock.Object, mockUserdata.Object, mockMapper.Object);

        }

        [TestMethod]
        public void PrimeTest()
        {
            //arrange
            int num = 10;
            string result = "Not a Prime number";

            //act
            var res = homeController.Prime(num);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(res, result);

        }
        [TestMethod]
        public void NonPrimeTest()
        {
            //arrange
            int num = 10;
            string result = "Prime number";

            //act
            var res = homeController.Prime(num);

            //Assert
            Assert.AreNotEqual(res, result);

        }
    }
}
