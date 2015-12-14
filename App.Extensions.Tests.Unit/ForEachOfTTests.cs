using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ForEachOfTTests
    {
        List<string> _result;
        Action<string> _actn;

        [TestInitialize]
        public void Setup()
        {
            _result = new List<string>();
            _actn = s => _result.Add(s + "Test");
        }

        [TestCleanup]
        public void TearDown()
        {
            _result = null;
            _actn = null;
        }
        
        [TestMethod]
        public void GivenEmptyIEnumerableOfStringDoesNothingToTheEnumeration()
        {
            var enumeration = new List<string>();
            enumeration.ForEach(_actn);
            Assert.IsTrue(_result.Count == 0);
        }

        [TestMethod]
        public void GivenIEnumerableOfStringContaining3StringsAddsTestOutputToTheResult()
        {
            var enumeration = new List<string>{"1", "2", "3"};
            enumeration.ForEach(_actn);
            Assert.IsTrue(_result.Count == 3);
            Assert.AreEqual(_result[0], "1Test");
            Assert.AreEqual(_result[1], "2Test");
            Assert.AreEqual(_result[2], "3Test");
        }

        [TestMethod]
        public void GivenListOfListItemSelectsEachItem()
        {
            var enumeration = new List<ListItem>{
                new ListItem { Text = "Item1"}, 
                new ListItem { Text = "Item2"},
                new ListItem { Text = "Item3"}
            };

            enumeration.ForEach(i => i.Selected = true);
            Assert.IsTrue(enumeration.Count == 3);
            Assert.IsTrue(enumeration[0].Selected);
            Assert.IsTrue(enumeration[1].Selected);
            Assert.IsTrue(enumeration[2].Selected);
        }
    }
}