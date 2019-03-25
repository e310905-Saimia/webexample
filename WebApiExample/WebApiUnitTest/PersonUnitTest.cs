using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiExample.Controllers;
using WebApiExample.Models;
using WebApiExample.Repositories;
using WebApiExample.Services;
using Xunit;

namespace WebApiUnitTest
{
    [TestClass]
    public class PersonUnitTest
    {
        private PersonsController _personsController;
        private IPersonService _personService;
        private IPersonRepository _personRepository;


        public PersonUnitTest()
        {
            _personService = new PersonService(_personRepository);
            _personsController = new PersonsController(_personRepository,_personService);
        }

        [TestMethod]
        public void Testi_WhenCalled_ReturnOkResult()
        {
            // Arrange

            //Act
            var result = _personsController.Testi();

            // Assert

            var noFound = Assert.That(result == )
        }

        [TestMethod]
        public void Testi_WhenCalled_ReturnTrue()
        {
            // Arrange

            //Act
            var result = _personsController.TestiReturnTrue();

            // Assert

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetPersons_WhenCalled_ReturnsOkResult()
        {

            //Act
            var result = _personsController.GetPersons();

            // Assert
            Assert.AreEqual(typeof(ActionResult), result);
            //Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPersons_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _personsController.GetPersons().Result as OkObjectResult;

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), okResult);
        }
    }
}
