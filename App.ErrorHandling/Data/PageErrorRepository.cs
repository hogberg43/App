using System.Data.SqlClient;
using App.Data.Sql;
using App.ErrorHandling.Domain;
using App.Extensions;

namespace App.ErrorHandling.Data
{
    public class PageErrorRepository : IPageErrorRepository
    {
        private readonly SqlConnection _connection;
        private readonly string _errorLoggingProcedure;

        public PageErrorRepository(SqlConnection connection, string errorLoggingProcedure)
        {
            _connection = connection;
            _errorLoggingProcedure = errorLoggingProcedure;
        }

        public int Add(ErrorInfo errorInfo)
        {
            var db = new SqlDb(_connection);
            var com = StoredProcedureFactory.Create(_errorLoggingProcedure,
                                                    new SqlParameter("@OffendingURL", errorInfo.OffendingUrl),
                                                    new SqlParameter("@OccuredDate", errorInfo.ErrorDate),
                                                    new SqlParameter("@ErrorTitle", errorInfo.ErrorTitle),
                                                    new SqlParameter("@ErrorDescription", errorInfo.ErrorDescription),
                                                    new SqlParameter("@CurrentLogin", errorInfo.UserId),
                                                    new SqlParameter("@ApplicationName", errorInfo.ApplicationName));

            return db.ExecuteAndReturnScalar(com, "There was a problem logging the error to the database.").ToInt();
                
        }
    }
}