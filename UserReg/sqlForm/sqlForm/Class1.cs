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
    class SQLHelper : IDisposable
    {
        string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id = sa; Password=mindfire@1";
        SqlConnection conn;
        SqlCommand comm;

        public SQLHelper()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            comm = new SqlCommand();
            comm.Connection = conn;
        }

        public void ExecuteNonQuery(string command)
        {
            comm.CommandText = command;
            comm.ExecuteNonQuery();
            MessageBox.Show("Executed non query");
        }

        /*public int ExecuteNonQuery(List<SqlParameter> param,string command)
        {
            
            return ;
        }*/

       public int ExecuteScalar(string command)
        {
            comm.CommandText = command;
            Int32 id=(Int32)comm.ExecuteScalar();
            return (int) id;
        }

        public DataTable SqlDataAdapter()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Student",conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }


        public void Dispose()
        {
            conn.Close();
            MessageBox.Show("Connection Closed");
        }
        

    }
}
