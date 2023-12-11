using System;
using System.Data;
using System.Windows.Forms;

namespace INVENTORYMANAGEMENTSYSTEM123
{
    public partial class inventorysystem : Form
    {
        private string currentUsername;

        public inventorysystem(string username)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            currentUsername = username;
            LoadInventoryData();
        }


        private string GetStatus(int quantity)
        {
            if (quantity <= 0)
            {
                return "Out of Stock";
            }
            else if (quantity <= 15)
            {
                return "Low Stock";
            }
            else
            {
                return "In Stock";
            }
        }



        public void LoadInventoryData()
        {
            dataGridView1.Rows.Clear(); // Clear existing rows

            string query = $"SELECT * FROM Inventory WHERE Username = '{currentUsername}'";
            DataTable result = DatabaseHelper.ExecuteQuery(query);

            int sequenceNumber = 1; // Initialize the sequence number

            foreach (DataRow row in result.Rows)
            {
                string item = row["Item"].ToString();
                decimal manufacturingCost = Convert.ToDecimal(row["ManufacturingCost"] == DBNull.Value ? 0 : row["ManufacturingCost"]);
                int quantity = Convert.ToInt32(row["Quantity"] == DBNull.Value ? 0 : row["Quantity"]);
                decimal sellingPrice = Convert.ToDecimal(row["SellingPrice"] == DBNull.Value ? 0 : row["SellingPrice"]);
                int sold = Convert.ToInt32(row["Sold"] == DBNull.Value ? 0 : row["Sold"]);

                // Calculate additional information
                decimal marketingSales = manufacturingCost * quantity;
                string status = GetStatus(quantity);
                decimal totalAmount = sellingPrice * sold;

                // Generate formatted item number (infinite sequence with leading zeros)
                string formattedItemNumber = sequenceNumber.ToString("0000");
                sequenceNumber++;

                // Assuming column indices based on your DataGridView
                int rowIndex = dataGridView1.Rows.Add(formattedItemNumber, item, manufacturingCost, quantity, marketingSales, status, sellingPrice, sold, totalAmount);

                // Optionally, you can set the ReadOnly property for specific cells if needed
                dataGridView1.Rows[rowIndex].Cells["ItemNumber"].ReadOnly = true;
                // Repeat for other cells as needed
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            edit edit = new edit(currentUsername);
            edit.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.ToLower();

            // Loop through each row and check if the searchTerm is found in any cell
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchTerm))
                    {
                        dataGridView1.ClearSelection();
                        row.Selected = true;
                        return;
                    }
                }
            }

            // If not found, show a message
            MessageBox.Show("Item not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
