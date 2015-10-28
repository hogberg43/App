using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Tests.Unit
{
    /// <summary>
    /// This class was created to check that entities in two DataTable objects contain the same data.
    /// </summary>
    public static class DataTableAssert
    {
        /// <summary>
        /// Checks two DataTables to be sure they contain the same data and have the same number of rows and columns.
        /// </summary>
        /// <param name="dtExpectedResult">Table of expected data</param>
        /// <param name="dtActualResult">Table of actual data</param>
        /// <exception cref="AssertFailedException">Exception thrown when assertion fails.</exception>
        public static void AllDataMatches(DataTable dtExpectedResult, DataTable dtActualResult)
        {
            try
            {
                var expectedRowCount = dtExpectedResult.Rows.Count;
                var actualRowCount = dtActualResult.Rows.Count;
                var expectedColumnCount = dtExpectedResult.Columns.Count;
                var actualColumnCount = dtActualResult.Columns.Count;

                if (expectedRowCount != actualRowCount) 
                    throw new AssertFailedException(string.Format("Record count doesn't match Expected Count: {0}, Actual Count: {1}",
                        expectedRowCount, actualRowCount));
                if (expectedColumnCount != actualColumnCount)
                    throw new AssertFailedException(string.Format("Column count doesn't match Expected Count: {0}, Actual Count: {1}",
                        expectedColumnCount, actualColumnCount));

                var rowIndex = 0;
                foreach (DataRow row in dtExpectedResult.Rows)
                {
                    var expectedRow = row;
                    var actualRow = dtActualResult.Rows[rowIndex];

                    foreach (DataColumn col in dtExpectedResult.Columns)
                    {
                        var colIndex = col.Ordinal;
                        object expectedVal = expectedRow[colIndex];
                        object actualVal = actualRow[colIndex];

                        Assert.AreEqual(expectedVal, actualVal, 
                            string.Format("DataTableAssert Failed. Expected Row-Column Index: {0}-{1}, Expected Value: {2}, Actual Value: {3}", 
                                rowIndex, colIndex, expectedVal, actualVal));
                    }
                    rowIndex++;
                }
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AssertFailedException("The assert failed and couldn't verify that these DataTables contain the same data.", ex);
            }
        }
    }
}

