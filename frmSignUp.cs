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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;

namespace CMPG315GUI
{
    public partial class frmSignUp : Form
    {

        string connectionString;
        public frmSignUp()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["CMPG315"].ConnectionString;
            con = new SqlConnection(connectionString);
        }

        private SqlConnection con;
        private SqlCommand command;

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

        public bool insertUserData(string firstName, string lastName, string phoneNo, string username, string password)
        {
            bool isInserted = false;
            try
            {
                openCon();
                string query = "INSERT INTO tblUserAccount (PhoneNumber, Username, Passwordd) VALUES (@FirstName, @LastName, @phoneNo, @Username, @Password)";
                command = new SqlCommand(query, con);

                command.Parameters.Add("@FirstName", SqlDbType.VarChar, 35).Value = firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar, 35).Value = lastName;
                command.Parameters.Add("@phoneNo", SqlDbType.Char, 10).Value = phoneNo;
                command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;


                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isInserted = true;
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please contact our IT Team.");
            }
            finally
            {
                closeCon();
            }

            return isInserted;
        }

        public bool containUsername(string username)
        {
            bool isFound = false;

            try
            {
                openCon();
                string query = "SELECT COUNT(*) FROM tblUsers WHERE Username = @Username";
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Username", username);

                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    isFound = true;
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

            return isFound;
        }

        public bool containPhoneNo(string phoneNo)
        {
            bool isFound = false;

            try
            {
                openCon();
                string query = "SELECT COUNT(*) FROM tblUsers WHERE PhoneNo = @PhoneNo";
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PhoneNo", phoneNo);

                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    isFound = true;
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
            return isFound;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (ValidateSignUp())
            {
                try
                {
                    if (CreateUserAccount())
                    {
                        MessageBox.Show("Account created successfully!", "Success");
                        this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    HandleSignUpError(ex);
                }
            }
        }

        private bool ValidateSignUp()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CreateUserAccount()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM tblUserAccount WHERE UserName = @username", connection))
                {
                    checkCmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Username already exists", "Sign Up Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                using (var createCmd = new SqlCommand(
                    @"INSERT INTO tblUserAccount (UserName,PhoneNumber,ProfilePicture, Passwordd, IsOnline) 
                      VALUES (@username, @phonenumber,null, @password, 0);
                      SELECT SCOPE_IDENTITY();", connection))
                {
                    createCmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    createCmd.Parameters.AddWithValue("@phonenumber", txtPhoneNumber.Text.Trim());
                    createCmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    var userId = createCmd.ExecuteScalar();
                    return userId != null;
                }
            }
        }

        private void HandleSignUpError(SqlException ex)
        {
            string errorMessage;
            switch (ex.Number)
            {
                case 2627: // Primary key violation
                case 2601: // Unique constraint
                    errorMessage = "Username already exists.";
                    break;
                default:
                    errorMessage = $"Database error: {ex.Message}";
                    break;
            }

            MessageBox.Show(errorMessage, "Sign Up Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void frmSignUp_Load(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) || (!char.IsLetterOrDigit(e.KeyChar)
                && e.KeyChar != '_') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}