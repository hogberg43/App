using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Tests.Unit
{
    public class DataSetAssert
    {
        public static void AllDataMatches(DataSet expectedDataSet, DataSet actualDataSet)
        {
            try
            {
                var expectedTableCount = expectedDataSet.Tables.Count;
                var actualTableCount = actualDataSet.Tables.Count;

                if (expectedTableCount != actualTableCount)
                    throw new AssertFailedException(string.Format("Record count doesn't match Expected Count: {0}, Actual Count: {1}", expectedTableCount, actualTableCount));

                for (int i = 0; i < expectedTableCount; i++)
                {
                    DataTableAssert.AllDataMatches(expectedDataSet.Tables[i], actualDataSet.Tables[i]);
                }
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AssertFailedException("The assert failed and couldn't verify that these DataSets contain the same data.", ex);
            }
        }
    }
}