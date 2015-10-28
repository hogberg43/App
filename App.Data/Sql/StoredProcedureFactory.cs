using System.Data;
using System.Data.SqlClient;

namespace App.Data.Sql
{
    public class StoredProcedureFactory
    {
        /// <summary>
        /// Replaces repetitious code
        /// </summary>
        /// <param name="name">Procedure name</param>
        /// <param name="parameterPairs">Procedure parameters</param>
        /// <returns>SqlCommand as a result of the name and parameters</returns>
        public static SqlCommand Create(string name, params SqlParameter[] parameterPairs)
        {
            var command = new SqlCommand(name);
            command.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in parameterPairs)
            {
                var p = parameter;
                if (parameter.SqlDbType == SqlDbType.NVarChar)
                {
                    if (parameter.Value == null)
                    {
                        p = new SqlParameter(parameter.ParameterName, "");
                    }
                } 
                command.Parameters.Add(p);
            }

            return command;
        }
    }
}
