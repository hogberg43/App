using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ForEachOfTTests
    {
        List<string> result;
        Action<string> actn;

        [TestInitialize]
        public void Setup()
        {
            result = new List<string>();
            actn = s => result.Add(s + "Test");
        }

        [TestCleanup]
        public void TearDown()
        {
            result = null;
            actn = null;
        }
        
        [TestMethod]
        public void GivenEmptyIEnumerableOfStringDoesNothingToTheEnumeration()
        {
            var enumeration = new List<string>();
            enumeration.ForEach(actn);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GivenIEnumerableOfStringContaining3StringsAddsTestOutputToTheResult()
        {
            var enumeration = new List<string>{"1", "2", "3"};
            enumeration.ForEach(actn);
            Assert.IsTrue(result.Count == 3);
            Assert.AreEqual(result[0], "1Test");
            Assert.AreEqual(result[1], "2Test");
            Assert.AreEqual(result[2], "3Test");
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