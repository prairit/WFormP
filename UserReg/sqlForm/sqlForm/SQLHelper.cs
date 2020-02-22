using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sqlForm
{ 
    /// <summary>
    /// This class helps in creating and closing connections as well as execute the stored procedures
    /// </summary>
    class SQLHelper
    {
        SqlConnection connection;

        /// <summary>
        /// This function establishes a connection to the SQL Server
        /// </summary>
        public void CreateConnection()
        {
            string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id = sa; Password=mindfire@1";
            connection = new SqlConnection(connString);
            connection.Open();
        }

        /// <summary>
        /// This function will close the connection with the SQL Server
        /// </summary>
        public void CloseConnection()
        {
            connection.Close();
        }

        /// <summary>
        /// This function will execute the Stored Procedure by using ExecuteNonQuery
        /// </summary>
        public void ExecuteNonQuery(SqlCommand command, string storedProcedure)
        {
            command.Connection = connection;
            command.CommandText = storedProcedure;
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();            
        }

        /// <summary>
        /// This function will execute the stored procedure by using ExecuteScalar
        /// </summary>
        public int ExecuteScalar(SqlCommand command, string commandString)
        {
            command.Connection = connection;
            command.CommandText = commandString;
            command.CommandType = CommandType.StoredProcedure;
            int id = Convert.ToInt32(command.ExecuteScalar());
            return id;
        }

        /// <summary>
        /// This function will use SQLAdapter to fill and return a datatable
        /// </summary>
        public DataTable SqlDataAdapter(string commandString)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = commandString;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// This function will add the SQLParameters to the passes command object 
        /// </summary>
        private void AddParameter(SqlCommand command, string name, SqlDbType type, ParameterDirection direction, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Direction = direction;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// This function will add the SQLParameters required for Insert stored procedure
        /// </summary>
        public SqlCommand InsertParameter(Student std)
        {
            SqlCommand command = new SqlCommand();
            AddParameter(command, "@FirstName", SqlDbType.NVarChar, ParameterDirection.Input, std.firstName);
            AddParameter(command, "@LastName", SqlDbType.NVarChar, ParameterDirection.Input, std.lastName);
            AddParameter(command, "@PhoneNumber", SqlDbType.NVarChar, ParameterDirection.Input, std.phoneNumber);
            AddParameter(command, "@EmailID", SqlDbType.NVarChar, ParameterDirection.Input, std.emailID);
            AddParameter(command, "@Gender", SqlDbType.NVarChar, ParameterDirection.Input, std.Gender);
            AddParameter(command, "@State", SqlDbType.NVarChar, ParameterDirection.Input, std.State);
            AddParameter(command, "@Country", SqlDbType.NVarChar, ParameterDirection.Input, std.Country);
            return command;
        }

        /// <summary>
        /// This function will add the SQLParameter for the Delete Stored Procedure
        /// </summary>
        public SqlCommand DeleteParameter(Student std)
        {
            SqlCommand command = new SqlCommand();
            AddParameter(command, "@StudentID", SqlDbType.Int, ParameterDirection.Input, std.StudentID);
            return command;
        }

        /// <summary>
        /// This function will add the SQLParameter for the Update Stored Procedure
        /// </summary>
        public SqlCommand UpdateParameter(Student std)
        {
            SqlCommand command = new SqlCommand();
            AddParameter(command, "@StudentID", SqlDbType.Int, ParameterDirection.Input, std.StudentID);
            AddParameter(command, "@FirstName", SqlDbType.NVarChar, ParameterDirection.Input, std.firstName);
            AddParameter(command, "@LastName", SqlDbType.NVarChar, ParameterDirection.Input, std.lastName);
            AddParameter(command, "@PhoneNumber", SqlDbType.NVarChar, ParameterDirection.Input, std.phoneNumber);
            AddParameter(command, "@EmailID", SqlDbType.NVarChar, ParameterDirection.Input, std.emailID);
            AddParameter(command, "@Gender", SqlDbType.NVarChar, ParameterDirection.Input, std.Gender);
            AddParameter(command, "@State", SqlDbType.NVarChar, ParameterDirection.Input, std.State);
            AddParameter(command, "@Country", SqlDbType.NVarChar, ParameterDirection.Input, std.Country);
            return command;
        }

    }
}
