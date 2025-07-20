using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CMPG315GUI
{
    public partial class frmForgotPassword : Form
    {
        public frmForgotPassword()
        {
            InitializeComponent();

            string conStr = ConfigurationManager.ConnectionStrings["CMPG315"].ConnectionString;
            con = new SqlConnection(conStr);
        }

        private SqlConnection con;
        private SqlCommand command;
        private SqlDataReader reader;

        public void openCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public bool containPhoneNo(string phoneNo)
        {
            bool isFound = false;

            try
            {
                openCon();
                string query = "SELECT FirstName, LastName FROM tblUsers WHERE phoneNo = @phoneNo";
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@phoneNo", phoneNo);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    string firstName = reader.GetString(0);
                    string lastName = reader.GetString(1);
                    lblName.Text = "Hey, " + firstName + " " + lastName + "!\n" +
                        "Please create a new password.";
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                closeCon();
            }

            return isFound;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmForgotPassword_Load(object sender, EventArgs e)
        {
            hideControls();
        }

        private void hideControls()
        {
            lblName.Visible = false;

            lblNewPassword.Visible = false;
            txtNewPassword.Visible = false;

            lblConfirmPassword.Visible = false;
            txtConfirmPassword.Visible = false;

            btnCreate.Visible = false;
        }

        private void unhideControls()
        {
            lblName.Visible = true;

            lblNewPassword.Visible = true;
            txtNewPassword.Visible = true;

            lblConfirmPassword.Visible = true;
            txtConfirmPassword.Visible = true;

            btnCreate.Visible = true;
        }

        private void unableControls()
        {
            txtPhoneNo.Enabled = false;
            btnRecover.Enabled = false;
        }

        public void replacePassword(string phoneNo, string password)
        {
            try
            {
                openCon();
                string query = "UPDATE tblUsers SET Password = @Password WHERE phoneNo = @PhoneNo";
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@PhoneNo", phoneNo);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password successfully updated!");
                }
                else
                {
                    MessageBox.Show("Failed to update password. Phone number not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                closeCon();
            }
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            string phoneNo = txtPhoneNo.Text.Trim();

            if (!containPhoneNo(phoneNo))
            {
                MessageBox.Show("Phone number not found in the system.");
                txtPhoneNo.Focus();
                txtPhoneNo.SelectionStart = 0;
                txtPhoneNo.SelectionLength = txtPhoneNo.Text.Length;
                return;
            }

            unhideControls();
            unableControls();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string phoneNo = txtPhoneNo.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Password does not match with the confirmed password.");
                txtNewPassword.Focus();
                txtNewPassword.SelectionStart = 0;
                txtNewPassword.SelectionLength = txtNewPassword.Text.Length;
                txtConfirmPassword.Text = "";
                return;
            }

            replacePassword(phoneNo, newPassword);
            MessageBox.Show("Password created successfully.");
            this.Close();
        }
    }
}
