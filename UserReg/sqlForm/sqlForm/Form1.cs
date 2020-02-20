using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sqlForm
{
    public partial class Form1 : Form
    {
        #region"Properties"
        DataTable result = new DataTable();

        Student std = new Student();
        #endregion

        #region "Functions"
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor for initializing form conpoents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            Student newStudent = new Student(); 
            newStudent = readData();
            newStudent.add();
            clearEntries();
            //bindGrid();
        }
        /// <summary>
        /// Event triggered on clicking the add button
        /// </summary>

        void bindGrid()
        {
            result = std.get();
            dataGridView1.DataSource = result;
        }
        /// <summary>
        /// This function will update the datagrid
        /// </summary>
        /// <returns></returns>
        Student readData()
        {
            std = new Student();
            std.firstName = fnameBox.Text;
            std.lastName = lnameBox.Text;
            try
            {
                std.StudentID = int.Parse(idBox.Text);
                std.phoneNumber = long.Parse(numberBox.Text);
            }
            catch (Exception) { }
            std.emailID = emailBox.Text;
            std.Gender= genderBox.Text;
            std.State = stateBox.Text;
            std.Country = countryBox.Text;

            return std;
        }
        /// <summary>
        /// This function will read data from winform and store it in the object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            createColumns();
        }
        /// <summary>
        /// Event triggered on loading of the winform
        /// </summary>
        void createColumns()
        {
            result.Columns.Add("StudentID");
            result.Columns.Add("First Name");
            result.Columns.Add("Last Name");
            result.Columns.Add("Phone Number");
            result.Columns.Add("EmailID");
            result.Columns.Add("Gender");
            result.Columns.Add("State");
            result.Columns.Add("Country");
        }
        /// <summary>
        /// This function will add columns in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            std.delete(int.Parse(idBox.Text));
            clearEntries();
        }
        /// <summary>
        /// This function  is triggered upon clicking the delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Loadbutton_Click(object sender, EventArgs e)
        {
            bindGrid();
        }
        /// <summary>
        /// This function is triggered upon clicking the load button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void updateButton_Click(object sender, EventArgs e)
        {
            readData();
            std.update();
            clearEntries();
        }
       
        void clearEntries()
        {
            idBox.Clear();
            fnameBox.Clear();
            lnameBox.Clear();
            numberBox.Clear();
            emailBox.Clear();
            genderBox.Clear();
            countryBox.ResetText();
            stateBox.ResetText();
        }
        

        #endregion
    }

    public class Student
    {
        #region "Properties"

        public int StudentID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public long phoneNumber  { get; set; }
        public string emailID { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        SQLHelper helper = new SQLHelper();

        #endregion

        #region 


        public DataTable get()
        {
            string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id=sa;Password=mindfire@1";
            SqlConnection conn = new SqlConnection(connString);

            conn.Open();
            /*
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Student", conn);
            DataTable res = new DataTable();
                adapter.Fill(res);*/
            //DataTable res = helper.SqlDataAdapter();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;

            comm.CommandText = "DisplayRows";
            comm.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable res = new DataTable();
            adapter.Fill(res);

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        public void add()
        {
            //string connString = "Data Source=SQLEXPRESS;Initial Catalog=TestDB;User ID =sa; Password =mindfire@1";
            string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id=sa;Password=mindfire@1";
            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;


            /*SqlParameter PmtrId = new SqlParameter();
            PmtrId.ParameterName = "@StudentID";
            PmtrId.SqlDbType = System.Data.SqlDbType.Int;
            PmtrId.Direction = ParameterDirection.Input;
            PmtrId.Value = this.StudentID;
            comm.Parameters.Add(PmtrId);*/

            SqlParameter PmtrfName = new SqlParameter();
            PmtrfName.ParameterName = "@FirstName";
            PmtrfName.SqlDbType = SqlDbType.NVarChar;
            PmtrfName.Direction = ParameterDirection.Input;
            PmtrfName.Value = this.firstName;
            comm.Parameters.Add(PmtrfName);

            SqlParameter PmtrlName = new SqlParameter();
            PmtrlName.ParameterName = "@LastName";
            PmtrlName.SqlDbType = SqlDbType.NVarChar;
            PmtrlName.Direction = ParameterDirection.Input;
            PmtrlName.Value = this.lastName;
            comm.Parameters.Add(PmtrlName);

            SqlParameter Pmtrnumber = new SqlParameter();
            Pmtrnumber.ParameterName = "@PhoneNumber";
            Pmtrnumber.SqlDbType = SqlDbType.NVarChar;
            Pmtrnumber.Direction = ParameterDirection.Input;
            Pmtrnumber.Value = this.phoneNumber;
            comm.Parameters.Add(Pmtrnumber);

            SqlParameter Pmtremail = new SqlParameter();
            Pmtremail.ParameterName = "@EmailID";
            Pmtremail.SqlDbType = SqlDbType.NVarChar;
            Pmtremail.Direction = ParameterDirection.Input;
            Pmtremail.Value = this.emailID;
            comm.Parameters.Add(Pmtremail);

            SqlParameter Pmtrgender = new SqlParameter();
            Pmtrgender.ParameterName = "@Gender";
            Pmtrgender.SqlDbType = SqlDbType.NVarChar;
            Pmtrgender.Direction = ParameterDirection.Input;
            Pmtrgender.Value = this.Gender;
            comm.Parameters.Add(Pmtrgender);

            SqlParameter Pmtrstate = new SqlParameter();
            Pmtrstate.ParameterName = "@State";
            Pmtrstate.SqlDbType = SqlDbType.NVarChar;
            Pmtrstate.Direction = ParameterDirection.Input;
            Pmtrstate.Value = this.Country;
            comm.Parameters.Add(Pmtrstate);

            SqlParameter Pmtrcountry = new SqlParameter();
            Pmtrcountry.ParameterName = "@Country";
            Pmtrcountry.SqlDbType = SqlDbType.NVarChar;
            Pmtrcountry.Direction = ParameterDirection.Input;
            Pmtrcountry.Value = this.Country;
            comm.Parameters.Add(Pmtrcountry);


            //string commandText = $"INSERT INTO Student Values (@id,@fn,@ln,@pn,@em,@gn,@st,@cn);";
            //MessageBox.Show(commandText);
            //comm.CommandText = commandText;
            //comm.ExecuteNonQuery();

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ScalarInputRow";
            Int32 row=Convert.ToInt32(comm.ExecuteScalar());
            MessageBox.Show(row.ToString());
            conn.Close();

            //helper.ExecuteNonQuery(commandText);

            //MessageBox.Show("inserted");

            //int result =helper.ExecuteScalar($"INSERT INTO Student Values ({this.StudentID},'{this.firstName}','{this.lastName}',{this.phoneNumber},'{this.emailID}','{this.Gender}','{this.State}','{this.Country}');SELECT * FROM Student WHERE StudentID={this.StudentID}");
            //MessageBox.Show("The inserted id is"+result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void delete(int id)
        {
            string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id=sa;Password=mindfire@1";

            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DeleteRow";

            SqlParameter PmtrId = new SqlParameter();
            PmtrId.ParameterName = "@StudentID";
            PmtrId.SqlDbType = System.Data.SqlDbType.Int;
            PmtrId.Direction = ParameterDirection.Input;
            PmtrId.Value = id;
            comm.Parameters.Add(PmtrId);

            comm.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("completed");
            //string commandText = $"DELETE FROM Student WHERE StudentID={id};";

            //MessageBox.Show(commandText);
            /*comm.CommandText = commandText;
            int n =comm.ExecuteNonQuery();
            MessageBox.Show("Number of rows deleted:"+n);
            conn.Close();*/


            //helper.ExecuteNonQuery(commandText);
        }
        
        public void update()
        {
            string connString = @"Server=PRAIRIT-PC\SQLEXPRESS;Database=TestDB;User Id=sa;Password=mindfire@1";
            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;

            SqlParameter PmtrId = new SqlParameter();
            PmtrId.ParameterName = "@StudentID";
            PmtrId.SqlDbType = System.Data.SqlDbType.Int;
            PmtrId.Direction = ParameterDirection.Input;
            PmtrId.Value = this.StudentID;
            comm.Parameters.Add(PmtrId);

            SqlParameter PmtrfName = new SqlParameter();
            PmtrfName.ParameterName = "@FirstName";
            PmtrfName.SqlDbType = SqlDbType.NVarChar;
            PmtrfName.Direction = ParameterDirection.Input;
            PmtrfName.Value = this.firstName;
            comm.Parameters.Add(PmtrfName);

            SqlParameter PmtrlName = new SqlParameter();
            PmtrlName.ParameterName = "@LastName";
            PmtrlName.SqlDbType = SqlDbType.NVarChar;
            PmtrlName.Direction = ParameterDirection.Input;
            PmtrlName.Value = this.lastName;
            comm.Parameters.Add(PmtrlName);

            SqlParameter Pmtrnumber = new SqlParameter();
            Pmtrnumber.ParameterName = "@PhoneNumber";
            Pmtrnumber.SqlDbType = SqlDbType.NVarChar;
            Pmtrnumber.Direction = ParameterDirection.Input;
            Pmtrnumber.Value = this.phoneNumber;
            comm.Parameters.Add(Pmtrnumber);

            SqlParameter Pmtremail = new SqlParameter();
            Pmtremail.ParameterName = "@EmailID";
            Pmtremail.SqlDbType = SqlDbType.NVarChar;
            Pmtremail.Direction = ParameterDirection.Input;
            Pmtremail.Value = this.emailID;
            comm.Parameters.Add(Pmtremail);

            SqlParameter Pmtrgender = new SqlParameter();
            Pmtrgender.ParameterName = "@Gender";
            Pmtrgender.SqlDbType = SqlDbType.NVarChar;
            Pmtrgender.Direction = ParameterDirection.Input;
            Pmtrgender.Value = this.Gender;
            comm.Parameters.Add(Pmtrgender);

            SqlParameter Pmtrstate = new SqlParameter();
            Pmtrstate.ParameterName = "@State";
            Pmtrstate.SqlDbType = SqlDbType.NVarChar;
            Pmtrstate.Direction = ParameterDirection.Input;
            Pmtrstate.Value = this.Country;
            comm.Parameters.Add(Pmtrstate);

            SqlParameter Pmtrcountry = new SqlParameter();
            Pmtrcountry.ParameterName = "@Country";
            Pmtrcountry.SqlDbType = SqlDbType.NVarChar;
            Pmtrcountry.Direction = ParameterDirection.Input;
            Pmtrcountry.Value = this.Country;
            comm.Parameters.Add(Pmtrcountry);

            //string commandText = $"UPDATE Student SET FirstName=@fn,LastName=@ln,PhoneNumber=@pn,EmailID=@em,Gender=@gn,State=@st,Country=@cn WHERE StudentID=@id;";
            //MessageBox.Show(commandText);
            //comm.CommandText = commandText;
            //comm.ExecuteNonQuery();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "UpdateRow";
            comm.ExecuteNonQuery();

            conn.Close();
        }

        #endregion
    }
}
