
namespace CMPG315GUI
{
    partial class frmNewGroup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.dgvNewGroup = new System.Windows.Forms.DataGridView();
            this.lblGrpName = new System.Windows.Forms.Label();
            this.txtGrpName = new System.Windows.Forms.TextBox();
            this.btnCreateGroup = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNxtStep = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblTempName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNewGroup
            // 
            this.dgvNewGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewGroup.Location = new System.Drawing.Point(32, 164);
            this.dgvNewGroup.Name = "dgvNewGroup";
            this.dgvNewGroup.RowHeadersWidth = 51;
            this.dgvNewGroup.Size = new System.Drawing.Size(471, 305);
            this.dgvNewGroup.TabIndex = 0;
            this.dgvNewGroup.Visible = false;
            this.dgvNewGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNewGroup_CellClick);
            this.dgvNewGroup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNewGroup_CellContentClick);
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Location = new System.Drawing.Point(62, 77);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(70, 13);
            this.lblGrpName.TabIndex = 1;
            this.lblGrpName.Text = "Group Name:";
            // 
            // txtGrpName
            // 
            this.txtGrpName.Location = new System.Drawing.Point(157, 70);
            this.txtGrpName.Name = "txtGrpName";
            this.txtGrpName.Size = new System.Drawing.Size(178, 20);
            this.txtGrpName.TabIndex = 2;
            // 
            // btnCreateGroup
            // 
            this.btnCreateGroup.Location = new System.Drawing.Point(157, 96);
            this.btnCreateGroup.Name = "btnCreateGroup";
            this.btnCreateGroup.Size = new System.Drawing.Size(137, 22);
            this.btnCreateGroup.TabIndex = 3;
            this.btnCreateGroup.Text = "CREATE GROUP";
            this.btnCreateGroup.UseVisualStyleBackColor = true;
            this.btnCreateGroup.Click += new System.EventHandler(this.btnCreateGroup_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(62, 49);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(292, 18);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "First step: Create an instance of your group";
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(389, 473);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(114, 22);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "ADD MEMBER";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(389, 501);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(114, 22);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "DONE";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Visible = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(389, 529);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 22);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNxtStep
            // 
            this.lblNxtStep.AutoSize = true;
            this.lblNxtStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNxtStep.Location = new System.Drawing.Point(29, 141);
            this.lblNxtStep.Name = "lblNxtStep";
            this.lblNxtStep.Size = new System.Drawing.Size(532, 18);
            this.lblNxtStep.TabIndex = 8;
            this.lblNxtStep.Text = "Great! Your group is created. Now add your members into the group(1 at a time)";
            this.lblNxtStep.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(195, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "CREATE NEW GROUP";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(29, 475);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(26, 18);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "ID:";
            this.lblID.Visible = false;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(61, 476);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(39, 20);
            this.txtID.TabIndex = 11;
            this.txtID.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(29, 505);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 18);
            this.lblName.TabIndex = 12;
            this.lblName.Text = " Name:";
            this.lblName.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 503);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(178, 20);
            this.txtName.TabIndex = 13;
            this.txtName.Visible = false;
            // 
            // lblTempName
            // 
            this.lblTempName.AutoSize = true;
            this.lblTempName.Location = new System.Drawing.Point(398, 96);
            this.lblTempName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTempName.Name = "lblTempName";
            this.lblTempName.Size = new System.Drawing.Size(10, 13);
            this.lblTempName.TabIndex = 14;
            this.lblTempName.Text = ".";
            this.lblTempName.Visible = false;
            // 
            // frmNewGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 569);
            this.Controls.Add(this.lblTempName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNxtStep);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnCreateGroup);
            this.Controls.Add(this.txtGrpName);
            this.Controls.Add(this.lblGrpName);
            this.Controls.Add(this.dgvNewGroup);
            this.Name = "frmNewGroup";
            this.Text = "FrmNewGroup";
            this.Load += new System.EventHandler(this.FrmNewGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNewGroup;
        private System.Windows.Forms.Label lblGrpName;
        private System.Windows.Forms.TextBox txtGrpName;
        private System.Windows.Forms.Button btnCreateGroup;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNxtStep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblTempName;
    }
}