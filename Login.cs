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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTxt.Text) || string.IsNullOrEmpty(passwordTxt.Text))
            {
                MessageBox.Show("Please input Username and Password!!");
                return;
            }

          
            DB_connection conn = new DB_connection();
            string username = userNameTxt.Text;  
            string password = passwordTxt.Text;

           
            conn.Login(username, password);
            this.Hide();  
        }

        private void userNameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
