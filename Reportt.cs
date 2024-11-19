using Microsoft.Reporting.WinForms;
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
    public partial class Reportt : Form
    {
        public Reportt()
        {
            InitializeComponent();
        }

        private void Reportt_Load(object sender, EventArgs e)
        {
            DB_connection con = new DB_connection();
            DataTable table = con.LoadReport();
            ReportDataSource dataSource = new ReportDataSource("DataSet1", table);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(dataSource);
            reportViewer1.RefreshReport();

            // TODO: This line of code loads data into the 'finalCSharpDataSet1.Report' table. You can move, or remove it, as needed.
            this.reportTableAdapter.Fill(this.finalCSharpDataSet1.Report);
            reportViewer1.RefreshReport();

 
            //con.count();
            //int employeeCount = con.count();
            //label3.Text = $"Total Employees: {employeeCount}"; // Update label text

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
      

    }
}
