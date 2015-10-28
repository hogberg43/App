using System;
using System.Data;
using System.Data.SqlClient;

namespace App.Data.Sql
{
    public class SqlDb
    {
        private readonly SqlConnection _conn;
        private readonly ConnectionInfo _connInfo = new ConnectionInfo();
        public SqlDb(SqlConnection conn)
        {
            _conn = conn;
            _connInfo.ConnectionString = conn.ConnectionString;
        }
        public void OpenConnection()
        {
            try
            {
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.ConnectionString = _connInfo.ConnectionString;
                    _conn.Open();
                }
            }
            catch (InvalidOperationException ioEx)
            {
                throw new DBException("The database connection string is not valid.", ioEx);
            }
        }
        public void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public void Execute(SqlCommand com, string exceptionMessage)
        {
            using (_conn)
            {
                try
                {
                    OpenConnection();
                    com.Connection = _conn;
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var ae = new DBCommandException(exceptionMessage, ex);
                    ae.AttachDbCommandMetaData(com);
                    throw ae;
                }
                finally
                {
                    com.Dispose();
                    CloseConnection();
                }
            }
        }

        public TransactionStatus ExecuteTransaction(SqlCommand com, string exceptionMessage)
        {
            using (_conn)
            {
                var dt = new DataTable();
                try
                {
                    OpenConnection();
                    com.Connection = _conn;
                    new SqlDataAdapter(com).Fill(dt);

                }
                catch (Exception ex)
                {
                    var ae = new DBCommandException(exceptionMessage, ex);
                    ae.AttachDbCommandMetaData(com);
                    throw ae;
                }
                finally
                {
                    com.Dispose();
                    CloseConnection();
                }
                return new TransactionStatus(dt);
            }
        }

        public object ExecuteAndReturnScalar(SqlCommand com, string exceptionMessage)
        {
            using (_conn)
            {
                object result;
                try
                {
                    OpenConnection();
                    com.Connection = _conn;
                    result = com.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    var ae = new DBCommandException(exceptionMessage, ex);
                    ae.AttachDbCommandMetaData(com);
                    throw ae;
                }
                finally
                {
                    com.Dispose();
                    CloseConnection();
                }
                return result;
            }
        }
        public DataTable ExecuteAndReturnDataTable(SqlCommand com, string exceptionMessage)
        {
            using (_conn)
            {
                var result = new DataTable();
                try
                {
                    OpenConnection();
                    com.Connection = _conn;
                    new SqlDataAdapter(com).Fill(result);
                }
                catch (Exception ex)
                {
                    var ae = new DBCommandException(exceptionMessage, ex);
                    ae.AttachDbCommandMetaData(com);
                    throw ae;
                }
                finally
                {
                    com.Dispose();
                    CloseConnection();
                }
                return result;
            }
        }

        public DataSet ExecuteAndReturnDataSet(SqlCommand com, string exceptionMessage)
        {
            using (_conn)
            {
                var result = new DataSet();
                try
                {
                    OpenConnection();
                    com.Connection = _conn;
                    new SqlDataAdapter(com).Fill(result);
                }
                catch (Exception ex)
                {
                    var ae = new DBCommandException(exceptionMessage, ex);
                    ae.AttachDbCommandMetaData(com);
                    throw ae;
                }
                finally
                {
                    com.Dispose();
                    CloseConnection();
                }
                return result;
            }
        }
    }
}
