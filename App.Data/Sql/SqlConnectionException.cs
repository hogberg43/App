using System;

namespace App.Data.Sql
{
    public class SqlConnectionException : Exception
    {
        public SqlConnectionException()
        {
            
        }
        public SqlConnectionException(string message) : base(message)
        {
            

        }

        public SqlConnectionException(string message, Exception ex) : base(message, ex)
        {
            
        }
    }
}