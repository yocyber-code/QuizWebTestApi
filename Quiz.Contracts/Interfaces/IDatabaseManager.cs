
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quiz.Contracts.Interfaces
{
    public interface IDatabaseManager
    {
        List<T> ExecuteSP<T>(string spName, List<SqlParameter> parameter);
        List<T> ConvertToList<T>(DataTable dt);
        int? ExecuteCommand(string sql, List<SqlParameter>? parameter);
        DataTable ExecuteQuery(string sql, string conString = "");
        decimal ExecuteSPCommandDecimal(string spName, List<SqlParameter> parameter);
        int? ExecuteSPCommandInt(string spName, List<SqlParameter> parameter);
        public bool ExecuteSPCommandValidation(string spName, List<SqlParameter> parameter);  
        
    }
}
