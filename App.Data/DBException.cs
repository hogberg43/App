using System;
using System.Data.Common;

namespace App.Data
{

    public class DBException : Exception
    {
        public DBException() { }
        public DBException(string message) : base(message) { }
        public DBException(string message, Exception inner) : base(message, inner) { }

        public void AttachDbCommandMetaData(DbCommand com)
        {
            if (com.CommandType == System.Data.CommandType.StoredProcedure)
            {
                Data["Stored Procedure"] = com.CommandText;
                foreach (DbParameter parameter in com.Parameters)
                {
                    string paramName = parameter.ParameterName;
                    string paramValue = parameter.Value.ToString();
                    Data[paramName] = paramValue;
                }
            }
        }
    }
}