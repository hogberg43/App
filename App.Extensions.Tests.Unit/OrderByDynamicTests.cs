using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class OrderByDynamicTests
    {

        public class SomeClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
        }

        private List<SomeClass> sample = new List<SomeClass>();

        [TestInitialize]
        public void Setup()
        {
            sample = new List<SomeClass>
                    {
                        new SomeClass { Id = 4, Name = "ABC", Date = new DateTime(2000, 1, 1)},
                        new SomeClass { Id = 1, Name = "XYZ", Date = new DateTime(2001, 1, 1)},
                        new SomeClass { Id = 2, Name = "JKL", Date = new DateTime(2002, 1, 1)}
                    };
        }

        [TestCleanup]
        public void TearDown()
        {
            sample = null;
        }

        [TestMethod]
        public void SortsByIdAscending()
        {
            var result = sample.OrderByDynamic("Id").ToList();
            Assert.IsTrue(result[0].Name == "XYZ");
            Assert.IsTrue(result[1].Name == "JKL");
            Assert.IsTrue(result[2].Name == "ABC");
        }

        [TestMethod]
        public void SortsByIdDescending()
        {
            var result = sample.OrderByDynamic("Id", SortDirection.Descending).ToList();
            Assert.IsTrue(result[2].Name == "XYZ");
            Assert.IsTrue(result[1].Name == "JKL");
            Assert.IsTrue(result[0].Name == "ABC");
        }

        [TestMethod]
        public void SortsByNameAscending()
        {
            var result = sample.OrderByDynamic("Name").ToList();
            Assert.IsTrue(result[2].Name == "XYZ");
            Assert.IsTrue(result[1].Name == "JKL");
            Assert.IsTrue(result[0].Name == "ABC");
        }

        [TestMethod]
        public void SortsByNameDescending()
        {
            var result = sample.OrderByDynamic("Name", SortDirection.Descending).ToList();
            Assert.IsTrue(result[0].Name == "XYZ");
            Assert.IsTrue(result[1].Name == "JKL");
            Assert.IsTrue(result[2].Name == "ABC");
        }
        [TestMethod]
        public void SortsByDateAscending()
        {
            var result = sample.OrderByDynamic("Date").ToList();
            Assert.IsTrue(result[0].Name == "ABC");
            Assert.IsTrue(result[1].Name == "XYZ");
            Assert.IsTrue(result[2].Name == "JKL");
        }

        [TestMethod]
        public void SortsByDateDescending()
        {
            var result = sample.OrderByDynamic("Date", SortDirection.Descending).ToList();
            Assert.IsTrue(result[2].Name == "ABC");
            Assert.IsTrue(result[1].Name == "XYZ");
            Assert.IsTrue(result[0].Name == "JKL");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentExceptionWhenPropertyNotFound()
        {
            sample.OrderByDynamic("name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentExceptionPropertyParameterIsNull()
        {
            sample.OrderByDynamic(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentExceptionPropertyParameterIsEmpty()
        {
            sample.OrderByDynamic("");
        }
    }
}
