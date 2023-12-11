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
    public partial class sign : Form
    {
        public sign()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate that all required information is provided
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please complete all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if information is incomplete
            }

            // Validate that the passwords match
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Passwords do not match. Please re-enter your password.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if passwords do not match
            }

            // Proceed with the sign-up process if all checks pass
            try
            {
                string username = textBox1.Text;
                string email = textBox2.Text;
                string password = textBox3.Text;

                // Use parameterized query to prevent SQL injection
                string query = "INSERT INTO account (Username, Email, Password) VALUES (@Username, @Email, @Password)";

                // Hash the password before storing it
                string hashedPassword = HashPassword(password);

                // Use a dictionary to provide parameters
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                  { "@Username", username },
                  { "@Email", email },
                  { "@Password", hashedPassword }
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the inventory form with the new user
                inventorysystem inventoryForm = new inventorysystem(username);
                inventoryForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                // Handle exceptions or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Example function to hash the password (use a secure hash function in practice)
        private string HashPassword(string password)
        {
            // Replace this with a secure hash function like bcrypt or Argon2
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
