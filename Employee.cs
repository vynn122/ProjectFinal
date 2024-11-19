using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFinal
{
    public partial class Employee : Form
    {
        private int selectedEmployeeID;
        public Employee()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Login login = new Login();
            login.Show();
            this.Close();
        }

       

        private void firstnameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastNameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AdressTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
      


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
     

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
        
            string firstName = firstnameTxt.Text;
            string lastName = lastNameTxt.Text;
            DateTime dob = dateTimePicker1.Value;
            string address = AdressTxt.Text;

       
            int departmentID;
            if (comboBox1.SelectedIndex >= 0)
            {
                departmentID = comboBox1.SelectedIndex + 1; 
            }
            else
            {
                MessageBox.Show("Please select a valid department.");
                return;
            }

            if (!decimal.TryParse(comboBox2.Text, out decimal salary))
            {
                MessageBox.Show("Please enter a valid salary.");
                return;
            }

            try
            {
                DB_connection conn = new DB_connection();
                conn.Insert(firstName, lastName, dob, address, departmentID, salary);
                firstnameTxt.Clear();
                lastNameTxt.Clear();
                AdressTxt.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
                LoadEmployeeData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeID != 0)
            {
                string firstName = firstnameTxt.Text;
                string lastName = lastNameTxt.Text;
                DateTime dob = dateTimePicker1.Value;
                string address = AdressTxt.Text;
                int departmentID = Convert.ToInt32(comboBox1.SelectedItem);
                decimal salary = Convert.ToDecimal(comboBox2.Text);

                DB_connection conn = new DB_connection();
                conn.Update(selectedEmployeeID, firstName, lastName, dob, address, departmentID, salary);

                MessageBox.Show("Employee updated successfully!");
                LoadEmployeeData(); 
            }
            else
            {
                MessageBox.Show("No employee selected for updating.");
            }
        }
        private void DltBtn_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeID != 0) 
            {
                DB_connection conn = new DB_connection();
                conn.Delete(selectedEmployeeID);  
                MessageBox.Show("Employee has been deleted successfully!");
                selectedEmployeeID = 0;
                firstnameTxt.Clear();
                lastNameTxt.Clear();
                AdressTxt.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
                editBtn.Enabled = false;
                DltBtn.Enabled = false;
                LoadEmployeeData();
            }
            else
            {
                MessageBox.Show("No employee selected for deletion.");
            }
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            selectedEmployeeID = 0;
            firstnameTxt.Clear();
            lastNameTxt.Clear();
            AdressTxt.Clear();
            //-1 ng mean no item was selected
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow row = dataGridView1.SelectedRows[0];
            //    int employeeID = Convert.ToInt32(row.Cells["EmployeeID"].Value);
            //    string firstName = row.Cells["FirstName"].Value.ToString();
            //    string lastName = row.Cells["LastName"].Value.ToString();
            //    string department = row.Cells["Department ID"].Value.ToString();
            //    decimal salary = Convert.ToDecimal(row.Cells["Salary"].Value);

            //    Report report = new Report(employeeID, firstName, lastName, department, salary);
            //    report.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Please select a row to view the report.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            Reportt report = new Reportt();
            report.Show();
        }

      
        private void LoadEmployeeData()
        {
            DB_connection dbConn = new DB_connection();
            DataTable employeeData = dbConn.GetEmployees();

            employeeData.Columns[0].ColumnName = "EmployeeID";
            employeeData.Columns[1].ColumnName = "FirstName";
            employeeData.Columns[2].ColumnName = "LastName";
            employeeData.Columns[3].ColumnName = "DateOFBirth";
            employeeData.Columns[4].ColumnName = "Address";
            employeeData.Columns[5].ColumnName = "Department ID";
            employeeData.Columns[6].ColumnName = "Salary";


            dataGridView1.DataSource = employeeData;     
            dataGridView1.ReadOnly = true;
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Items.Add(1);
            comboBox1.Items.Add(2);
            comboBox1.Items.Add(3);
            comboBox1.Items.Add(4);
    
            comboBox2.Items.Add(1200);
            comboBox2.Items.Add(2000);
            comboBox2.Items.Add(400);
            comboBox2.Items.Add(500);
            comboBox2.Items.Add(270);
            comboBox2.Items.Add(800);
            editBtn.Enabled = false;
            DltBtn.Enabled = false;
            LoadEmployeeData();
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            //    //show data nv row del yg select 
            //    selectedEmployeeID = Convert.ToInt32(row.Cells["EmployeeID"].Value);
            //    firstnameTxt.Text = row.Cells["FirstName"].Value.ToString();
            //    lastNameTxt.Text = row.Cells["LastName"].Value.ToString();
            //    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateOFBirth"].Value);
            //    AdressTxt.Text = row.Cells["Address"].Value.ToString();
            //    comboBox1.SelectedIndex = Convert.ToInt32(row.Cells["Department ID"].Value) - 1;
            //    comboBox2.Text = row.Cells["Salary"].Value.ToString();
            //    int departmentID = Convert.ToInt32(row.Cells["Department ID"].Value);
            //    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(departmentID);
            //    comboBox2.Text = row.Cells["Salary"].Value.ToString();
            //    editBtn.Enabled = true;
            //    DltBtn.Enabled = true;

            //}

        }

        private void salaryBtn_Click(object sender, EventArgs e)
        {
            Salary salary = new Salary();
            salary.Show();
            this.Close();
        }

        private void firstnameTxt_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                //show data nv row del yg select 
                selectedEmployeeID = Convert.ToInt32(row.Cells["EmployeeID"].Value);
                firstnameTxt.Text = row.Cells["FirstName"].Value.ToString();
                lastNameTxt.Text = row.Cells["LastName"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateOFBirth"].Value);
                AdressTxt.Text = row.Cells["Address"].Value.ToString();
                comboBox1.SelectedIndex = Convert.ToInt32(row.Cells["Department ID"].Value) - 1;
                comboBox2.Text = row.Cells["Salary"].Value.ToString();
                int departmentID = Convert.ToInt32(row.Cells["Department ID"].Value);
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(departmentID);
                comboBox2.Text = row.Cells["Salary"].Value.ToString();
                editBtn.Enabled = true;
                DltBtn.Enabled = true;

            }
        }
    }
}
