//using DocumentFormat.OpenXml.Office.Word;


using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Contracts.Interfaces;

namespace Quiz.Contracts.Services
{
    public class DatabaseManagerService : IDatabaseManager
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection { get; }

        private string _connString { get; set; }

        public DatabaseManagerService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionSQLServer"));
            _connString = _dbConnection.ConnectionString;
        }

        public List<T> ExecuteSP<T>(string spName, List<SqlParameter> parameter)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection Connection = new SqlConnection(_connString))
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand(spName, Connection);
                    cmd.Parameters.AddRange(parameter.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();

                    cmd.Parameters.Clear();
                    Connection.Close();
                }
                var retVal = ConvertToList<T>(dataTable);
                return retVal;
            }
            catch (SqlException e)
            {
                Console.WriteLine("ConvertToList Exception: " + e.ToString());
                return new List<T>();
            }
        }

        public int? ExecuteSPCommandInt(string spName, List<SqlParameter> parameter)
        {

            DataTable dataTable = new DataTable();

            using (SqlConnection Connection = new SqlConnection(_connString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(spName, Connection);
                cmd.Parameters.AddRange(parameter.ToArray());
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                da.Dispose();

                Connection.Close();
            }

            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0][0]);
            }
            else
            {
                return null;
            }
        }


        public decimal ExecuteSPCommandDecimal(string spName, List<SqlParameter> parameter)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection Connection = new SqlConnection(_connString))
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand(spName, Connection);
                    cmd.Parameters.AddRange(parameter.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();

                    Connection.Close();
                }


                return Convert.ToDecimal(dataTable.Rows[0][0]);
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public bool ExecuteSPCommandValidation(string spName, List<SqlParameter> parameter)
        {
            using (SqlConnection Connection = new SqlConnection(_connString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(spName, Connection);
                cmd.Parameters.AddRange(parameter.ToArray());
                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.ExecuteScalar() == null)
                    return true;

                int result = (int)cmd.ExecuteScalar();
                Connection.Close();
                return result > 0;
            }
        }

        public List<T> ConvertToList<T>(DataTable dt)
        {
            try
            {
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();

                var properties = typeof(T).GetProperties();

                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();

                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name))
                        {
                            if (row[pro.Name].GetType() == typeof(DBNull)) pro.SetValue(objT, null, null);
                            else pro.SetValue(objT, row[pro.Name], null);
                        }
                    }

                    return objT;
                }).ToList();
            }
            catch (Exception e)
            {
                List<T> output = new List<T>();

                if (dt.Rows.Count > 0)
                {
                    foreach (var row in dt.Select())
                    {
                        var value = (T)Convert.ChangeType(Convert.ToString(row[0]), typeof(T));
                        output.Add(value);
                    }
                }

                return output;


            }
        }

        public int? ExecuteCommand(string sql, List<SqlParameter>? parameter)
        {
            using (SqlConnection Connection = new SqlConnection(_connString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(sql, Connection);

                if (parameter != null)
                {
                    cmd.Parameters.AddRange(parameter.ToArray());
                }

                cmd.CommandType = CommandType.Text;

                int result = cmd.ExecuteNonQuery();
                Connection.Close();

                return result;
            }
        }

        public DataTable ExecuteQuery(string sql, string conString = "")
        {
            DataTable dataTable = new DataTable();

            if (!string.IsNullOrEmpty(conString))
                _connString = conString;

            using (SqlConnection Connection = new SqlConnection(_connString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(sql, Connection);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                da.Dispose();

                Connection.Close();

                return dataTable;
            }
        }
    }

}
