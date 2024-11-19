using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFinal
{
    public partial class Report : Form
    {
        public Report(int employeeID, string firstName, string lastName, string department, decimal salary)
        {
            InitializeComponent();
            txtEmployeeID.Text = employeeID.ToString();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtDepartment.Text = department;
            txtSalary.Text = "$ " + salary.ToString();

        }

        private void Report_Load(object sender, EventArgs e)
        {
        
        }

        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
