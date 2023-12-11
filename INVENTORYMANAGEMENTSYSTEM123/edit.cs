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
    public partial class edit : Form
    {
        private string currentUsername;

        public edit(string username)
        {
            InitializeComponent();
            currentUsername = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsNumeric(textBox2.Text) || !IsNumeric(textBox3.Text) || !IsNumeric(textBox4.Text) || !IsNumeric(textBox5.Text))
            {
                MessageBox.Show("Please enter valid numeric values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Assuming you have a DatabaseHelper class with ExecuteNonQuery and ExecuteScalar methods
            string query = @"INSERT INTO inventory (Item, ManufacturingCost, Quantity, SellingPrice, Sold, Username)
            VALUES (@Item, @ManufacturingCost, @Quantity, @SellingPrice, @Sold, @Username)";

            // Use parameters to avoid SQL injection
            var parameters = new Dictionary<string, object>
            {
             { "@Item", textBox1.Text },
             { "@ManufacturingCost", Convert.ToDecimal(textBox2.Text) },
             { "@Quantity", Convert.ToInt32(textBox3.Text) },
             { "@SellingPrice", Convert.ToDecimal(textBox4.Text) },
             { "@Sold", textBox5.Text },
             { "@Username", currentUsername }
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);

            // Refresh the DataGridView with updated data
            var ownerForm = this.Owner as inventorysystem;
            if (ownerForm != null)
            {
                ownerForm.LoadInventoryData();
            }

            // Clear all textboxes
            ClearTextBoxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inventorysystem inventorysystem = new inventorysystem(currentUsername);
            inventorysystem.Show();
            this.Hide();
        }

        private void ClearTextBoxes()
        {
            // Clear all textboxes
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
