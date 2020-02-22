using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace sqlForm
{
    /// <summary>
    /// This class contains the properties of the Student 
    /// </summary>
    public class Student
    {
        #region "Properties"

        public int StudentID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public long phoneNumber { get; set; }
        public string emailID { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        SQLHelper helper = new SQLHelper();

        #endregion

        #region "Functions"

        /// <summary>
        /// This function will use SQLDataAdapter and return the DataTable
        /// </summary>
        public DataTable get()
        {
            helper.CreateConnection();
            DataTable res = helper.SqlDataAdapter("DisplayRows");
            helper.CloseConnection();

            return res;
        }
        
        /// <summary>
        /// This function will add rows in the SQL datatable
        /// </summary>
        public void add()
        {
            helper.CreateConnection();
            SqlCommand command = helper.InsertParameter(this);
            int result = helper.ExecuteScalar(command, "ScalarInsertRow");
            MessageBox.Show("Row inserted at index "+result.ToString());
            helper.CloseConnection();
        }
        
        /// <summary>
        /// This function will delete the row at the desired StudentID
        /// </summary>
        public void delete()
        {
            helper.CreateConnection();
            helper.ExecuteNonQuery(helper.DeleteParameter(this), "DeleteRow");
            helper.CloseConnection();
            MessageBox.Show("Row deleted successfully");
        }

        /// <summary>
        /// This function will update the entries in the SQL table to the inserted vales
        /// </summary>
        public void update()
        {
            helper.CreateConnection();
            helper.ExecuteNonQuery(helper.UpdateParameter(this), "UpdateRow");
            helper.CloseConnection();
            MessageBox.Show("Row updated successfully");
        }


        #endregion
    }
}
