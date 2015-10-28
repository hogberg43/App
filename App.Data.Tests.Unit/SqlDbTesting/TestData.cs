using System;
using System.Collections.Generic;
using System.Data;

namespace App.Data.Tests.Unit.SqlDbTesting
{
    public class TestData
    {
        /// <summary>
        /// Converts a list of strongly typed objects into a DataTable where 
        /// column names equal property names and data types equal property 
        /// data types.
        /// </summary>
        /// <remarks>Credit To: http://jopinblog.wordpress.com/2008/08/22/easily-create-datatables-for-unit-tests/ </remarks>
        /// <typeparam name="T">Type of objects in the list.</typeparam>
        /// <param name="rows">List containing the data to convert.</param>
        /// <returns>A DataTable object that is the result of the conversion.</returns>
        public static DataTable ListToTable<T>(List<T> rows)
        {
            var dt = new DataTable();
            var props = typeof(T).GetProperties();
            Array.ForEach(props, p => dt.Columns.Add(p.Name, p.PropertyType));
            foreach (var r in rows)
            {
                var vals = new object[props.Length];
                for (int idx = 0; idx < vals.Length; idx++)
                {
                    vals[idx] = props[idx].GetValue(r, null);
                }
                dt.Rows.Add(vals);
            }
            return dt;
        }
    }
}