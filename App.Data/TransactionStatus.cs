using System;
using System.Data;

namespace App.Data
{
    public class TransactionStatus
    {
        public TransactionStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string TransactionInfo { get; set; }
        public TransactionStatus()
        {
            StatusCode = (TransactionStatusCode) Enum.Parse(typeof(TransactionStatusCode), "-1");
            ErrorMessage = string.Empty;
            TransactionInfo = string.Empty;
        }
        public TransactionStatus(DataTable dt)
        {
            if (dt.Columns.Contains("ErrorCode") && dt.Columns.Contains("ErrorMessage") && dt.Columns.Contains("TransactionInfo"))
            {
                if (dt.Rows.Count == 1)
                {
                    var rowOne = dt.Rows[0];
                    StatusCode = (TransactionStatusCode) Enum.Parse(typeof(TransactionStatusCode), rowOne["ErrorCode"].ToString());
                    ErrorMessage = rowOne["ErrorMessage"].ToString();
                    TransactionInfo = rowOne["TransactionInfo"].ToString();
                }
                else
                {
                    throw new ExecuteTransactionFormatException("The TransactionStatus object expects exactly one row to be returned by the procedure.");
                }
            }
            else
            {
                throw new ExecuteTransactionFormatException("The data table returned cannot be used by the TransactionStatus object since it does not contain one "
                                                            + "or all of the following columns: ErrorCode, ErrorMessage, TransactionInfo.");
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", StatusCode, ErrorMessage, TransactionInfo);
        }
    }
}
