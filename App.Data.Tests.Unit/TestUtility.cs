using System;

namespace App.Data.Tests.Unit
{
    public class TestUtility
    {
        public static void TestExceptionMessage(ExecuteTransactionFormatException testException, string expectedMessage)
        {
            //const string expectedMessage = "The data table returned cannot be used by the TransactionStatus object since it does not contain one or all of the following columns: ErrorCode, ErrorMessage, TransactionInfo.";
            var actualMessage = testException.Message;
            if (actualMessage == expectedMessage)
            {
                throw testException;
            }
            throw new Exception(string.Format("Test failed because exception message didn't match the expected result: Actual<{0}>, Expected<{1}>", actualMessage, expectedMessage));
        }
    }
}
