using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace sqlForm
{
    class SQLHelper 
    {
        //string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id = sa; Password=mindfire@1";
        SqlConnection connection;
        //SqlCommand comm;

        /*public SQLHelper()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            comm = new SqlCommand();
            comm.Connection = conn;
        }*/



        public void CreateConnection()
        {
            string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id = sa; Password=mindfire@1";
            connection = new SqlConnection(connString);
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void ExecuteNonQuery(SqlCommand command,string storedProcedure)
        {
            command.Connection = connection;
            command.CommandText = storedProcedure;
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();

            MessageBox.Show("Executed Non-Query");
        }
        

       public int ExecuteScalar(SqlCommand command,string commandString)
        {
            command.Connection = connection;
            command.CommandText = commandString;
            command.CommandType = CommandType.StoredProcedure;
            int id=Convert.ToInt32(command.ExecuteScalar());
            MessageBox.Show("Execute Scalar");
            return id;
        }

        public DataTable SqlDataAdapter()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Student",connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        void AddParameter(SqlCommand command, string name, SqlDbType type, ParameterDirection direction, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Direction = direction;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        public SqlCommand InsertParameter(Student std)
        {
            SqlCommand command = new SqlCommand();
            //AddParameter(command, "@StudentID", SqlDbType.Int, ParameterDirection.Input, std.firstName);
            AddParameter(command, "@FirstName", SqlDbType.NVarChar, ParameterDirection.Input, std.firstName);
            AddParameter(command, "@LastName", SqlDbType.NVarChar, ParameterDirection.Input, std.lastName);
            AddParameter(command, "@PhoneNumber", SqlDbType.NVarChar, ParameterDirection.Input, std.phoneNumber);
            AddParameter(command, "@EmailID", SqlDbType.NVarChar, ParameterDirection.Input, std.emailID);
            AddParameter(command, "@Gender", SqlDbType.NVarChar, ParameterDirection.Input, std.Gender);
            AddParameter(command, "@State", SqlDbType.NVarChar, ParameterDirection.Input, std.State);
            AddParameter(command, "@Country", SqlDbType.NVarChar, ParameterDirection.Input, std.Country);
            return command;
        }

        public SqlCommand DeleteParameter(Student std)
        {
            SqlCommand command = new SqlCommand();
            AddParameter(command, "@StudentID", SqlDbType.Int, ParameterDirection.Input, std.StudentID);
            return command;
        }

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
