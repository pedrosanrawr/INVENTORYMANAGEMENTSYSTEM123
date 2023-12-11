using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INVENTORYMANAGEMENTSYSTEM123
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = textBox2.Text;
            string password = textBox3.Text;

            string query = $"SELECT * FROM account WHERE (Username = '{usernameOrEmail}' OR Email = '{usernameOrEmail}') AND Password = '{password}'";
            DataTable result = DatabaseHelper.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                inventorysystem inventoryForm = new inventorysystem(usernameOrEmail);
                inventoryForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username/email or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign sign = new sign();
            sign.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
