using System.Configuration;
using System.Data.SqlClient;

namespace App.Data.Sql
{
    public abstract class SqlConnectionsBase
    {
        protected static SqlConnection GetConnection(string configKey)
        {
            SqlConnection conn = null;
            if (ConfigurationManager.ConnectionStrings[configKey] != null)
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings[configKey].ConnectionString);
            }
            if (conn == null)
            {
                if (ConfigurationManager.AppSettings[configKey] != null)
                {
                    conn = new SqlConnection(ConfigurationManager.AppSettings[configKey]);
                }
            }

            if (conn == null)
            {
                throw new SqlConnectionException(string.Format("The {0} Connection was not made. Verify that '{0}' is contained in the ConnectionsStrings or the AppSettings sections in the web.config and that the connection string is valid.", configKey));
            }
            return conn;
        }
    }
}
