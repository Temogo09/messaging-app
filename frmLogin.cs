using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CMPG315GUI
{
    public partial class frmLogin : Form
    {
        private string connectionString;
        public string Username { get; private set; }
        public int UserId { get; private set; }

        public frmLogin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["CMPG315"].ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection configuration is missing", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT UserID, UserName FROM tblUserAccount 
                                   WHERE UserName = @Username 
                                   AND Passwordd = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Username", SqlDbType.VarChar, 30).Value = username;
                        command.Parameters.Add("@Password", SqlDbType.VarChar, 15).Value = password;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserId = reader.GetInt32(0);
                                Username = reader.GetString(1);

                                // Update online status
                                UpdateOnlineStatus(UserId, true);

                                // Proceed to main form
                                this.Hide();
                                Form1 mainForm = new Form1(UserId, Username);
                                mainForm.FormClosed += (s, args) => this.Close();
                                mainForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password", "Login Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtUsername.Focus();
                                txtUsername.SelectAll();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleDatabaseError(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
   
        }

        private void UpdateOnlineStatus(int userId, bool isOnline)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(
                    "UPDATE tblUserAccount SET IsOnline = @IsOnline WHERE UserID = @UserId", connection))
                {
                    command.Parameters.Add("@IsOnline", SqlDbType.Bit).Value = isOnline;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user status: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HandleDatabaseError(SqlException ex)
        {
            string errorMessage;

            switch (ex.Number)
            {
                case 53:    // SQL Server not found
                case -1:    // Connection timeout
                    errorMessage = "Cannot connect to database server. Please check your internet connection.";
                    break;
                case 18456: // Login failed
                    errorMessage = "Database login failed. Please contact support.";
                    break;
                default:
                    errorMessage = $"Database error: {ex.Message}";
                    break;
            }

            MessageBox.Show(errorMessage, "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {
            var forgotForm = new frmForgotPassword();
            forgotForm.ShowDialog();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var signUpForm = new frmSignUp();
            signUpForm.ShowDialog();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) ||
                (!char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '_' &&
                e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (UserId > 0)
            {
                UpdateOnlineStatus(UserId, false);
            }
            base.OnFormClosing(e);
        }
    }
}