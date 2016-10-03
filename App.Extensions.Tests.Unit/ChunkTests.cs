using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ChunkTests
    {

        [TestMethod]
        public void Chunk_Given_a_null_IEnumerable_returns_an_empty_IEnumerable()
        {
            // Arrange
            IEnumerable<string> testList = null;

            // Act
            var actual = testList.Chunk(4);

            // Assert
            Assert.IsTrue(actual.Any());
        }
        [TestMethod]
        public void Chunk_Given_a_List_of_4_strings_returns_4_Lists_using_Chunk_of_1()
        {
            // Arrange
            IEnumerable<string> testList = new List<string>
            {
                "test1",
                "test2",
                "test3",
                "test4",
            };

            // Act
            var actual = testList.Chunk(1);

            // Assert
            Assert.IsTrue(actual.Count() == 4);
        }
    }
}
