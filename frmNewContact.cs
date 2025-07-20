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
using System.Configuration;


namespace CMPG315GUI
{
    public partial class frmNewContact : Form
    {
        private readonly int currentUserId;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["CMPG315"].ConnectionString;
        public event EventHandler ContactAdded;



        public frmNewContact(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
        }

        private void FrmNewContact_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            btnCreate.BackColor = Color.MediumSeaGreen;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Font = new Font("Segoe UI", 10);

            label1.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label1.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string phone = txtCellPhone.Text.Trim();

            // 🔍 Validations
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

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Step 1: Check if user exists
                var checkCmd = new SqlCommand(@"
                    SELECT UserID FROM tblUserAccount 
                    WHERE PhoneNumber = @phone AND UserName = @username", conn);
                checkCmd.Parameters.AddWithValue("@phone", phone);
                checkCmd.Parameters.AddWithValue("@username", name);

                var result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("The user is not on the chatting app.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int contactUserId = Convert.ToInt32(result);

                // Step 2: Prevent adding yourself
                if (contactUserId == currentUserId)
                {
                    MessageBox.Show("You cannot add yourself as a contact.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Step 3: Add to contact list
                var insertContactCmd = new SqlCommand(
                    "INSERT INTO tblUserContact (UserID, ContactUserID, ContactNumber, ContactName) " +
                    "VALUES (@userId, @contactId, @contactnumber, @contactName)", conn);

                insertContactCmd.Parameters.AddWithValue("@userId", currentUserId);
                insertContactCmd.Parameters.AddWithValue("@contactId", contactUserId);
                insertContactCmd.Parameters.AddWithValue("@contactnumber", phone);
                insertContactCmd.Parameters.AddWithValue("@contactName", name);

                try
                {
                    insertContactCmd.ExecuteNonQuery();
                    MessageBox.Show("Contact added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string sqlName = "SELECT Username FROM tblUserAccount WHERE UserID = @userId";

                    insertContactCmd.Parameters.AddWithValue("@userId", currentUserId);
                    Form1 mainForm = new Form1(currentUserId, sqlName);
                    mainForm.LoadContactList();

                    //  Notify other forms
                    ContactAdded?.Invoke(this, EventArgs.Empty);

                    // Clear input fields
                    txtName.Clear();
                    txtCellPhone.Clear();
                }
                catch (SqlException ex) when (ex.Number == 2627)
                {
                    MessageBox.Show("This contact is already added.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding contact: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); //  closes the form
        }
    }
}
