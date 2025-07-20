using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Practice
{
    public partial class FrmAddNew : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Admin\\Documents\\2025 Materials\\First Semester\\CMPG 315\\Group Project\\p\\Practice\\Practice\\MessagingDB.mdf\";Integrated Security=True";
   
     

        public FrmAddNew()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

            // Optional: UI enhancements
            btnCreate.BackColor = Color.MediumSeaGreen;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Font = new Font("Segoe UI", 10);

            label1.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label1.TextAlign = ContentAlignment.MiddleCenter;

            lblName.Font = lblSurname.Font = lblNum.Font = new Font("Segoe UI", 10);

        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string phone = txtCellPhone.Text.Trim();

            // ===== Field Validations =====
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a contact name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter a phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (phone.Length != 10 || !phone.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must be exactly 10 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ===== Insert into Database =====
            string query = @"INSERT INTO tblUserContactList (ContactName, Surname, PhoneNumber) 
                             VALUES (@ContactName, @Surname, @PhoneNumber)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 30).Value = name;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 30).Value = surname;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10).Value = phone;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contact added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optional: Clear fields after insertion
                    txtName.Clear();
                    txtCellPhone.Clear();
                    txtSurname.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding contact: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

