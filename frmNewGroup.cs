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
using System.Net.Sockets;   
using System.IO;

namespace CMPG315GUI
{
    public partial class frmNewGroup : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CMPG315"].ConnectionString;
        private int currentUserId;
        string groupname;
        int index = 0;
        private int groupId = -1;
        public event EventHandler GroupAdded;

        private List<int> pendingMembers = new List<int>();

        public frmNewGroup(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void FrmNewGroup_Load(object sender, EventArgs e)
        {
            Form1 tempForm = new Form1(currentUserId, groupname);

            tempForm.StyleDataGridView(dgvNewGroup);
            LoadContactList(dgvNewGroup);
        }

        private void ResizeDataGridViewToFit(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.Width = dgv.PreferredSize.Width;
            dgv.Height = dgv.PreferredSize.Height;
            dgv.ScrollBars = ScrollBars.None; // Optional
        }

        private void LoadContactList(DataGridView dgViewContactList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                /* string query = @"SELECT c.ContactUserID, u.UserName 
                          FROM tblUserContact c
                          JOIN tblUserAccount u ON c.ContactUserID = u.UserID
                          WHERE c.UserID = @currentUserId";*/
                string query = @"SELECT contactUserID,ContactName, ContactNumber 
                 FROM tblUserContact 
                 WHERE UserID = @currentUserId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@currentUserId", currentUserId);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgViewContactList.DataSource = dt;
                ResizeDataGridViewToFit(dgViewContactList);
            }
        }

        private int CreateGroup(string groupName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tblGroupChat (GroupName, AdminID) OUTPUT INSERTED.GroupID VALUES (@GroupName, @AdminID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GroupName", groupName);
                cmd.Parameters.AddWithValue("@AdminID", currentUserId);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void AddMemberToGroup(int groupId, int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // string checkQuery = "SELECT COUNT(*) FROM tblGroupMember WHERE GroupID = @GroupID AND UserID = @UserID";
                string checkQuery = "SELECT COUNT(*) FROM tblUserAccount WHERE UserID = @UserID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
               // checkCmd.Parameters.AddWithValue("@GroupID", groupId);
                checkCmd.Parameters.AddWithValue("@UserID", userId);

                int exists = (int)checkCmd.ExecuteScalar();
                /*if (exists > 0)
                {
                    MessageBox.Show("User already in group.");
                    return;
                }*/
                if (exists == 0)
                {
                    MessageBox.Show("User does not exist.");
                    return;
                }
                // Check if the group exists
                string groupCheckQuery = "SELECT COUNT(*) FROM tblGroupChat WHERE GroupID = @GroupID";
                SqlCommand groupCheckCmd = new SqlCommand(groupCheckQuery, conn);
                groupCheckCmd.Parameters.AddWithValue("@GroupID", groupId);
                int groupExists = (int)groupCheckCmd.ExecuteScalar();

                if (groupExists == 0)
                {
                    MessageBox.Show("Group does not exist.");
                    return;
                }

                // Check if the user is already a member
                string memberCheckQuery = "SELECT COUNT(*) FROM tblGroupMember WHERE GroupID = @GroupID AND UserID = @UserID";
                SqlCommand memberCheckCmd = new SqlCommand(memberCheckQuery, conn);
                memberCheckCmd.Parameters.AddWithValue("@GroupID", groupId);
                memberCheckCmd.Parameters.AddWithValue("@UserID", userId);
                int memberExists = (int)memberCheckCmd.ExecuteScalar();

                if (memberExists > 0)
                {
                    MessageBox.Show("User is already a member of the group.");
                    return;
                }
                string insertQuery = "INSERT INTO tblGroupMember (GroupID, UserID) VALUES (@GroupID, @UserID)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@GroupID", groupId);
                insertCmd.Parameters.AddWithValue("@UserID", userId);
                insertCmd.ExecuteNonQuery();
            }
        }



        private void dgvNewGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(lblTempName.Text))
            {
                MessageBox.Show("No group name. Please create one first");
                return;
            }
            if (pendingMembers.Count == 0)
            {
                MessageBox.Show("Please add at least one member before finishing.");
                return;
            }
            groupId = CreateGroup(lblTempName.Text);

            AddMemberToGroup(groupId, currentUserId);
            foreach (int memberId in pendingMembers)
            {
                AddMemberToGroup(groupId, memberId);
            }

            Form1 temp = new Form1(currentUserId, groupname);
            temp.LoadGroupList();

            MessageBox.Show("The group has been created successfully");
            pendingMembers.Clear();

        }
        private void btnDone_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(lblTempName.Text))
            {
                MessageBox.Show("No group name. Please create one first");
                return;
            }
            if (pendingMembers.Count == 0)
            {
                MessageBox.Show("Please add at least one member before finishing.");
                return;
            }
            groupId = CreateGroup(lblTempName.Text);

            AddMemberToGroup(groupId, currentUserId);
            foreach (int memberId in pendingMembers)
            {
                AddMemberToGroup(groupId, memberId);
            }

            Form1 temp = new Form1(currentUserId, groupname);
            temp.LoadGroupList();

            MessageBox.Show("The group has been created successfully");
            pendingMembers.Clear();

            //Validate again if needed
            if (groupId == -1)
            {
                MessageBox.Show("Please create and populate the group first.");
                return;
            }

            // Trigger the GroupAdded event to notify parent
            GroupAdded?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Trigger the GroupAdded event (even if closing early)
            GroupAdded?.Invoke(this, EventArgs.Empty);
            this.Close();
        }


        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtGrpName.Text))
            {
                MessageBox.Show("Group name is required.");
                return;
            }

            groupname = txtGrpName.Text.Trim();
            lblTempName.Text = groupname;
            lblTempName.Visible = false;
            MessageBox.Show("Group name saved! You can now add members.");

            showResults(groupname); // Show inputs for manual adding
        }

        private void showResults(string groupName)
        {
            lblNxtStep.Text = "Great! Your group name is saved. Now add your members into the " + groupName + " group(1 at a time).";
            lblNxtStep.Visible = true;
            dgvNewGroup.Visible = true;
            btnAdd.Visible = true;
            btnClose.Visible = true;
            btnDone.Visible = true;
            txtID.Visible = true;
            txtName.Visible = true;
            lblID.Visible = true;
            lblName.Visible = true;

        }
        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvNewGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                if(index >=0)
                {
                    DataGridViewRow row = dgvNewGroup.Rows[index];
                    txtID.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
           // int userId = Convert.ToInt32(txtID.Text);
            if (string.IsNullOrWhiteSpace(lblTempName.Text))
            {
                MessageBox.Show("You must create a group name first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtID.Text) || !int.TryParse(txtID.Text, out int userId))
            {
                MessageBox.Show("Select a contact to from the list add.");
                return;
            }
            
            if (pendingMembers.Contains(userId))
            {
                MessageBox.Show("This member is already added.");
                return;
            }
            pendingMembers.Add(userId);
           // AddMemberToGroup(groupId,userId);
          
            
            MessageBox.Show(txtName.Text +" added to the group!");

            txtName.Clear();
            txtID.Clear();
        }

    }
}

