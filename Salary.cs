using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFinal
{
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
        }

        private void Salary_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("EmployeeID", 100);
            listView1.Columns.Add("FirstName", 150);
            listView1.Columns.Add("LastName", 150);
            listView1.Columns.Add("Salary", 100);
            listView1.View = View.Details;
            LoadEmployeeIDs();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // Check if an employee is selected from the ListBox
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an EmployeeID from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedEmployeeID = listBox1.SelectedItem.ToString();

            // Clear previous items in the ListView
            listView1.Items.Clear();

            // Get the employee data based on the selected EmployeeID
            //we can get tha data by select employeeID in listBox
            DataTable employeeData = dB_Connection.GetEmployeeByID(selectedEmployeeID);

   
            if (employeeData.Rows.Count > 0)
            {
                DataRow row = employeeData.Rows[0];
                //here in listView
                ListViewItem item = new ListViewItem(row["EmployeeID"].ToString());
                item.SubItems.Add(row["FirstName"].ToString());
                item.SubItems.Add(row["LastName"].ToString());
                ///.ToString("C") mean currency
                item.SubItems.Add(Convert.ToDecimal(row["SalaryAmount"]).ToString("C"));
                listView1.Items.Add(item);
            }
            else
            {
                MessageBox.Show("Employee not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        DB_connection dB_Connection = new DB_connection();
        private void LoadEmployeeIDs()
        {
            ///this is show the employeeID int the listBox
            DataTable employeeIDs = dB_Connection.GetEmployeesInSalaryForm(); 

            // Add each EmployeeID to the ListBox
            foreach (DataRow row in employeeIDs.Rows)
            {
                listBox1.Items.Add(row["EmployeeID"].ToString());
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Close();

        }
    }
}
