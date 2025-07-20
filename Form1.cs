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
using System.Net.Sockets;   //Added
using System.IO; //Added




namespace CMPG315GUI
{


    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer messageTimer;
        private string connectionString = ConfigurationManager.ConnectionStrings["CMPG315"].ConnectionString;
        private int currentUserId;
        private string currentUsername;

        public void Form1_Load(object sender, EventArgs e)
        {
            lblUsername.Text = "Logged in: "+ currentUsername;
            LoadContactList();
            LoadGroupList();
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        public Form1(int userId, string username)
        {
            InitializeComponent();
            currentUserId = userId;
            currentUsername = username;

            // Initialize timer for checking new messages
            messageTimer = new System.Windows.Forms.Timer();
            messageTimer.Interval = 3000; // 3 seconds
            messageTimer.Tick += CheckForNewMessages;
            messageTimer.Start();

            DisplayWelcomeMessage(username);
            StyleDataGridView(dgViewContactList);
            StyleDataGridView(dgViewGroupList);
            LoadContactList();
            LoadGroupList();
            StyleTreeView();
        }


        private void DisplayWelcomeMessage(string username)
        {
            TreeNode welcomeNode = new TreeNode($"Welcome, {username}! Select a contact or group to start chatting.")
            {
                ForeColor = Color.DarkBlue,
                BackColor = Color.LightCyan
            };
            treeViewChat1.Nodes.Add(welcomeNode);
        }

        private void CheckForNewMessages(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblViewedContact.Text))
            {
                LoadChatHistory(lblViewedContact.Text);
            }
        }
        public void LoadContactList()
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT u.UserName AS ContactName 
                      FROM tblUserContact uc
                      JOIN tblUserAccount u ON uc.ContactUserID = u.UserID
                      WHERE uc.UserID = @currentUserId", conn))
                {
                    cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
                    var adapter = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgViewContactList.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading contacts: {ex.Message}");
            }
        }
        public void LoadGroupList()
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT gc.GroupName 
                      FROM tblGroupMember gm
                      JOIN tblGroupChat gc ON gm.GroupID = gc.GroupID
                      WHERE gm.UserID = @currentUserId", conn))
                {
                    cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
                    var adapter = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgViewGroupList.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading groups: {ex.Message}");
            }
        }

        public void StyleDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void StyleTreeView()
        {
            treeViewChat1.BorderStyle = BorderStyle.None;
            treeViewChat1.BackColor = Color.FromArgb(240, 242, 245);
            treeViewChat1.Indent = 20;
            treeViewChat1.ItemHeight = 24;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Copy", null, (s, e) =>
            {
                if (treeViewChat1.SelectedNode != null)
                    Clipboard.SetText(treeViewChat1.SelectedNode.Text);
            });
            treeViewChat1.ContextMenuStrip = contextMenu;
        }

        private void SaveMessageToDatabase(int senderId, int? receiverId, int? groupId, string message)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var messageQuery = @"INSERT INTO tblMessage 
                                   (SenderID, MessageText, SentAt, IsRead, MessageStatus)
                                   VALUES (@senderId, @message, @sentAt, 0, 0);
                                   SELECT SCOPE_IDENTITY();";

                int messageId;
                using (var cmd = new SqlCommand(messageQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@senderId", senderId);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters.AddWithValue("@sentAt", DateTime.Now);
                    messageId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                var typeQuery = receiverId.HasValue ?
                    "INSERT INTO tblPrivateMessage (MessageID, ReceiverID) VALUES (@messageId, @id)" :
                    "INSERT INTO tblGroupMessage (MessageID, GroupID) VALUES (@messageId, @id)";

                using (var cmd = new SqlCommand(typeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@messageId", messageId);
                    cmd.Parameters.AddWithValue("@id", receiverId ?? groupId.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DisplayMessage(string sender, string message, DateTime timestamp, bool isMe)
        {
            if (treeViewChat1.InvokeRequired)
            {
                treeViewChat1.Invoke(new Action(() =>
                    DisplayMessage(sender, message, timestamp, isMe)));
                return;
            }

            var messageNode = new TreeNode($"{sender} ({timestamp:hh:mm tt}): {message}")
            {
                ForeColor = isMe ? Color.White : Color.Black,
                BackColor = isMe ? Color.FromArgb(0, 132, 255) : Color.FromArgb(229, 229, 229)
            };

            treeViewChat1.Nodes.Add(messageNode);
            messageNode.EnsureVisible();
        }

        private void LoadChatHistory(string contactOrGroupName)
        {
            try
            {
                treeViewChat1.Nodes.Clear();

                if (string.IsNullOrEmpty(contactOrGroupName))
                    return;

                int? contactId = GetUserIdByName(contactOrGroupName);
                int? groupId = contactId == null ? GetGroupIdByName(contactOrGroupName) : null;

                if (contactId != null)
                {
                    LoadPrivateChatHistory(contactId.Value, contactOrGroupName);
                }
                else if (groupId != null)
                {
                    LoadGroupChatHistory(groupId.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading chat history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPrivateChatHistory(int contactId, string contactName)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT m.SenderID, m.MessageText, m.SentAt
                FROM tblMessage m
                JOIN tblPrivateMessage pm ON m.MessageID = pm.MessageID
                WHERE (m.SenderID = @currentUserId AND pm.ReceiverID = @contactId)
                   OR (m.SenderID = @contactId AND pm.ReceiverID = @currentUserId)
                ORDER BY m.SentAt", conn))
            {
                cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
                cmd.Parameters.AddWithValue("@contactId", contactId);

                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int senderId = reader.GetInt32(0);
                    string text = reader.GetString(1);
                    DateTime sentAt = reader.GetDateTime(2);

                    string sender = senderId == currentUserId ? "You" : contactName;
                    bool isMe = senderId == currentUserId;
                    DisplayMessage(sender, text, sentAt, isMe);
                }
            }
        }

        private void LoadGroupChatHistory(int groupId)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT m.SenderID, m.MessageText, m.SentAt
                FROM tblMessage m
                JOIN tblGroupMessage gm ON m.MessageID = gm.MessageID
                WHERE gm.GroupID = @groupId
                ORDER BY m.SentAt", conn))
            {
                cmd.Parameters.AddWithValue("@groupId", groupId);

                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int senderId = reader.GetInt32(0);
                    string text = reader.GetString(1);
                    DateTime sentAt = reader.GetDateTime(2);

                    string sender = senderId == currentUserId ? "You" : GetUserNameById(senderId);
                    bool isMe = senderId == currentUserId;

                    DisplayMessage(sender, text, sentAt, isMe);
                }
            }
        }

        private string GetUserNameById(int userId)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT UserName FROM tblUserAccount WHERE UserID = @userId", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", userId);
                return cmd.ExecuteScalar()?.ToString() ?? "Unknown";
            }
        }

        private int? GetUserIdByName(string name)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT UserID FROM tblUserAccount WHERE UserName = @name", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@name", name);
                var result = cmd.ExecuteScalar();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
        }

        private int? GetGroupIdByName(string name)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT GroupID FROM tblGroupChat WHERE GroupName = @name", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@name", name);
                var result = cmd.ExecuteScalar();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
        }

        private void dgViewContactList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string contactName = dgViewContactList.Rows[e.RowIndex].Cells[0]?.Value?.ToString();
                if (!string.IsNullOrEmpty(contactName))
                {
                    lblViewedContact.Text = contactName;
                    LoadChatHistory(contactName);
                }
            }
        }

        private void dgViewGroupList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string groupName = dgViewGroupList.Rows[e.RowIndex].Cells[0]?.Value?.ToString();
                if (!string.IsNullOrEmpty(groupName))
                {
                    lblViewedContact.Text = groupName;
                    LoadChatHistory(groupName);
                }
            }
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {

            frmNewContact frmContact = new frmNewContact(currentUserId);
            // Subscribe to ContactAdded event
            frmContact.ContactAdded += (s, args) =>
            {
                LoadContactList(); // 🔁 Reload the contact list when a contact is added
            };
            frmContact.ShowDialog();
        }

        private void lblContacts_Click(object sender, EventArgs e)
        {

        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmNewGroup NewGroupForm = new frmNewGroup(currentUserId);
            // Subscribe to GroupAdded event
            NewGroupForm.GroupAdded += (s, args) =>
            {
                LoadGroupList(); //  Reload the contact list when a contact is added
            };
            NewGroupForm.ShowDialog();
        }

        //GetContactIdByName method
        private int? GetContactIdByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT u.UserID 
                                FROM tblUserAccount u
                                WHERE u.UserName = @name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                var result = cmd.ExecuteScalar();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
        }


        //GetCurrentUserName method
        private string GetCurrentUserName()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT UserName FROM tblUserAccount WHERE UserID = @userId", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                return cmd.ExecuteScalar()?.ToString();
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /////////

        }

        private void dataGridContactList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Search bar 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query2 = "SELECT ContactName FROM tblUserContact WHERE ContactName LIKE @search";

                    SqlDataAdapter adapter = new SqlDataAdapter(query2, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearchContact.Text + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgViewContactList.DataSource = dt;
                    }
                    else
                    {
                        dgViewContactList.DataSource = null;
                        MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgViewGroupList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ///
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query2 = "SELECT GroupName  FROM tblGroupChat WHERE GroupName  LIKE @search";

                    SqlDataAdapter adapter = new SqlDataAdapter(query2, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearchContact.Text + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgViewGroupList.DataSource = dt;
                    }
                    else
                    {
                        dgViewGroupList.DataSource = null;
                        MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string recipient = lblViewedContact.Text;
            string messageContent = textBoxMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(messageContent))
            {
                MessageBox.Show("Please enter a message");
                return;
            }

           
            try
            {
                int? contactId = GetUserIdByName(recipient);
                int? groupId = GetGroupIdByName(recipient);

                if (contactId == null && groupId == null)
                {
                    MessageBox.Show("Recipient not found");
                    return;
                }

                SaveMessageToDatabase(currentUserId, contactId, groupId, messageContent);
                DisplayMessage("You", messageContent, DateTime.Now, true);
                textBoxMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }


        public int GetCurrentUserId()
        {
            return currentUserId; // Simply return the stored user ID from login
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            messageTimer?.Stop();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query2 = "SELECT DISTINCT ContactName FROM tblUserContact WHERE ContactName LIKE @search";


                    SqlDataAdapter adapter = new SqlDataAdapter(query2, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearchContact.Text + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgViewContactList.DataSource = dt;
                    }
                    else
                    {
                        dgViewContactList.DataSource = null;
                        MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query2 = "SELECT  GroupName FROM tblGroupChat WHERE GroupName LIKE @search";

                    SqlDataAdapter adapter = new SqlDataAdapter(query2, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearchGroup.Text + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgViewGroupList.DataSource = dt;

                    }
                    else
                    {
                        dgViewGroupList.DataSource = null;
                        MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    

        private void dgViewContactList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCloseChat_Click(object sender, EventArgs e)
        {
            treeViewChat1.Nodes.Clear();  
            lblViewedContact.Text = "";   
            
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                frmLogin loginForm = new frmLogin();
                loginForm.ShowDialog();
                //Application.Exit();
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}