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
    /// <summary>
    /// This class defines the functionality of the form
    /// </summary>
    public partial class Form1 : Form
    {
        #region"Properties"
        DataTable result = new DataTable();

        Student std = new Student();
        #endregion

        #region "Functions"

        /// <summary>
        /// Constructor for initializing form conpoents
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event triggered on clicking the add button
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            Student newStudent = new Student(); 
            newStudent = readData();
            newStudent.add();
            clearEntries();
        }

        /// <summary>
        /// This function will update the datagrid
        /// </summary>
        void bindGrid()
        {
            result = std.get();
            dataGridView1.DataSource = result;
        }

        /// <summary>
        /// This function will read data from winform and store it in the object
        /// </summary>
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
        /// Event triggered on loading of the winform
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            createColumns();
        }

        /// <summary>
        /// This function will add columns in the datagrid
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
        /// This function  is triggered upon clicking the delete button
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            readData();
            std.delete();
            clearEntries();
        }

        /// <summary>
        /// This function is triggered upon clicking the load button
        /// </summary>
        private void Loadbutton_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        /// <summary>
        /// This function is triggered upon clicking the update button
        /// </summary>
        private void updateButton_Click(object sender, EventArgs e)
        {
            readData();
            std.update();
            clearEntries();
        }

        /// <summary>
        /// This function will reset the entries of the windows form
        /// </summary>
        private void clearEntries()
        {
            idBox.Clear();
            fnameBox.Clear();
            lnameBox.Clear();
            numberBox.Clear();
            emailBox.Clear();
            genderBox.Clear();
            countryBox.Clear();
            stateBox.Clear();
        }

        #endregion
    }

}
