
namespace CMPG315GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //private System.Windows.Forms.TreeView treeViewChat;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgViewContactList = new System.Windows.Forms.DataGridView();
            this.btnAddNewContact = new System.Windows.Forms.Button();
            this.txtSearchContact = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgViewGroupList = new System.Windows.Forms.DataGridView();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.txtSearchGroup = new System.Windows.Forms.TextBox();
            this.lblContacts = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblViewedContact = new System.Windows.Forms.Label();
            this.lblChat = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.RichTextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.btnCloseChat = new System.Windows.Forms.Button();
            this.treeViewChat1 = new System.Windows.Forms.TreeView();
            this.lblDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewContactList)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewGroupList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.lblContacts);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblDate);
            this.splitContainer1.Panel2.Controls.Add(this.btnLogOut);
            this.splitContainer1.Panel2.Controls.Add(this.lblUsername);
            this.splitContainer1.Panel2.Controls.Add(this.lblViewedContact);
            this.splitContainer1.Panel2.Controls.Add(this.lblChat);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxMessage);
            this.splitContainer1.Panel2.Controls.Add(this.btnSendMessage);
            this.splitContainer1.Panel2.Controls.Add(this.btnCloseChat);
            this.splitContainer1.Panel2.Controls.Add(this.treeViewChat1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1283, 554);
            this.splitContainer1.SplitterDistance = 386;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(16, 39);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 500);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgViewContactList);
            this.tabPage1.Controls.Add(this.btnAddNewContact);
            this.tabPage1.Controls.Add(this.txtSearchContact);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(336, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Contacts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgViewContactList
            // 
            this.dgViewContactList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewContactList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgViewContactList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgViewContactList.ColumnHeadersHeight = 29;
            this.dgViewContactList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgViewContactList.Location = new System.Drawing.Point(5, 43);
            this.dgViewContactList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgViewContactList.Name = "dgViewContactList";
            this.dgViewContactList.ReadOnly = true;
            this.dgViewContactList.RowHeadersWidth = 51;
            this.dgViewContactList.Size = new System.Drawing.Size(320, 374);
            this.dgViewContactList.TabIndex = 4;
            this.dgViewContactList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContactList_CellClick);
            this.dgViewContactList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewContactList_CellContentClick);
            this.dgViewContactList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewContactList_CellDoubleClick);
            // 
            // btnAddNewContact
            // 
            this.btnAddNewContact.Location = new System.Drawing.Point(225, 7);
            this.btnAddNewContact.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddNewContact.Name = "btnAddNewContact";
            this.btnAddNewContact.Size = new System.Drawing.Size(100, 28);
            this.btnAddNewContact.TabIndex = 1;
            this.btnAddNewContact.Text = "New";
            this.btnAddNewContact.UseVisualStyleBackColor = true;
            this.btnAddNewContact.Click += new System.EventHandler(this.btnAddNewContact_Click);
            // 
            // txtSearchContact
            // 
            this.txtSearchContact.Location = new System.Drawing.Point(4, 7);
            this.txtSearchContact.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearchContact.Name = "txtSearchContact";
            this.txtSearchContact.Size = new System.Drawing.Size(205, 22);
            this.txtSearchContact.TabIndex = 0;
            this.txtSearchContact.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgViewGroupList);
            this.tabPage2.Controls.Add(this.btnNewGroup);
            this.tabPage2.Controls.Add(this.txtSearchGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(336, 471);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Groups";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgViewGroupList
            // 
            this.dgViewGroupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewGroupList.Location = new System.Drawing.Point(5, 42);
            this.dgViewGroupList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgViewGroupList.Name = "dgViewGroupList";
            this.dgViewGroupList.RowHeadersWidth = 51;
            this.dgViewGroupList.Size = new System.Drawing.Size(320, 418);
            this.dgViewGroupList.TabIndex = 8;
            this.dgViewGroupList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewGroupList_CellDoubleClick);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(228, 6);
            this.btnNewGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(100, 28);
            this.btnNewGroup.TabIndex = 5;
            this.btnNewGroup.Text = "New";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // txtSearchGroup
            // 
            this.txtSearchGroup.Location = new System.Drawing.Point(7, 6);
            this.txtSearchGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearchGroup.Name = "txtSearchGroup";
            this.txtSearchGroup.Size = new System.Drawing.Size(205, 22);
            this.txtSearchGroup.TabIndex = 4;
            this.txtSearchGroup.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // lblContacts
            // 
            this.lblContacts.AutoSize = true;
            this.lblContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacts.Location = new System.Drawing.Point(16, 11);
            this.lblContacts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContacts.Name = "lblContacts";
            this.lblContacts.Size = new System.Drawing.Size(52, 25);
            this.lblContacts.TabIndex = 0;
            this.lblContacts.Text = "Lists";
            this.lblContacts.Click += new System.EventHandler(this.lblContacts_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(701, 4);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(141, 36);
            this.btnLogOut.TabIndex = 8;
            this.btnLogOut.Text = "LOGOUT";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(121, 30);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(12, 17);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = ".";
            // 
            // lblViewedContact
            // 
            this.lblViewedContact.AutoSize = true;
            this.lblViewedContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewedContact.Location = new System.Drawing.Point(17, 50);
            this.lblViewedContact.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblViewedContact.Name = "lblViewedContact";
            this.lblViewedContact.Size = new System.Drawing.Size(13, 20);
            this.lblViewedContact.TabIndex = 6;
            this.lblViewedContact.Text = ".";
            // 
            // lblChat
            // 
            this.lblChat.AutoSize = true;
            this.lblChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChat.Location = new System.Drawing.Point(13, 9);
            this.lblChat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(120, 42);
            this.lblChat.TabIndex = 4;
            this.lblChat.Text = "CHAT";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(19, 476);
            this.textBoxMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(673, 62);
            this.textBoxMessage.TabIndex = 3;
            this.textBoxMessage.Text = "";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(701, 476);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(141, 63);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "SEND";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCloseChat
            // 
            this.btnCloseChat.BackColor = System.Drawing.Color.White;
            this.btnCloseChat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCloseChat.Location = new System.Drawing.Point(701, 46);
            this.btnCloseChat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCloseChat.Name = "btnCloseChat";
            this.btnCloseChat.Size = new System.Drawing.Size(141, 32);
            this.btnCloseChat.TabIndex = 1;
            this.btnCloseChat.Text = "CLOSE CHAT";
            this.btnCloseChat.UseVisualStyleBackColor = false;
            this.btnCloseChat.Click += new System.EventHandler(this.btnCloseChat_Click);
            // 
            // treeViewChat1
            // 
            this.treeViewChat1.Location = new System.Drawing.Point(19, 80);
            this.treeViewChat1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeViewChat1.Name = "treeViewChat1";
            this.treeViewChat1.Size = new System.Drawing.Size(823, 388);
            this.treeViewChat1.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(282, 9);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(12, 17);
            this.lblDate.TabIndex = 9;
            this.lblDate.Text = ".";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 554);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Tet Messaging App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewContactList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewGroupList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblContacts;
        private System.Windows.Forms.Button btnAddNewContact;
        private System.Windows.Forms.TextBox txtSearchContact;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox textBoxMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Button btnCloseChat;
        private System.Windows.Forms.TreeView treeViewChat1;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.TextBox txtSearchGroup;
        private System.Windows.Forms.Label lblChat;
        private System.Windows.Forms.DataGridView dgViewContactList;
        private System.Windows.Forms.DataGridView dgViewGroupList;
        private System.Windows.Forms.Label lblViewedContact;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Label lblDate;
    }
}

