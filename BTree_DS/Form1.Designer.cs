namespace BTree_DS
{
    partial class Form1
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
            this.txtPrint = new System.Windows.Forms.TextBox();
            this.MaxDegree = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInsertFile = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtDel = new System.Windows.Forms.TextBox();
            this.txtInsert = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPrint
            // 
            this.txtPrint.Location = new System.Drawing.Point(60, 174);
            this.txtPrint.Multiline = true;
            this.txtPrint.Name = "txtPrint";
            this.txtPrint.ReadOnly = true;
            this.txtPrint.Size = new System.Drawing.Size(933, 522);
            this.txtPrint.TabIndex = 29;
            this.txtPrint.TextChanged += new System.EventHandler(this.txtPrint_TextChanged);
            // 
            // MaxDegree
            // 
            this.MaxDegree.FormattingEnabled = true;
            this.MaxDegree.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.MaxDegree.Location = new System.Drawing.Point(279, 33);
            this.MaxDegree.Name = "MaxDegree";
            this.MaxDegree.Size = new System.Drawing.Size(100, 23);
            this.MaxDegree.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Max Degree";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // btnInsertFile
            // 
            this.btnInsertFile.Location = new System.Drawing.Point(60, 142);
            this.btnInsertFile.Name = "btnInsertFile";
            this.btnInsertFile.Size = new System.Drawing.Size(184, 23);
            this.btnInsertFile.TabIndex = 26;
            this.btnInsertFile.Text = "Insert from file";
            this.btnInsertFile.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(279, 143);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 25);
            this.txtSearch.TabIndex = 25;
            // 
            // txtDel
            // 
            this.txtDel.Location = new System.Drawing.Point(279, 90);
            this.txtDel.Name = "txtDel";
            this.txtDel.Size = new System.Drawing.Size(100, 25);
            this.txtDel.TabIndex = 24;
            // 
            // txtInsert
            // 
            this.txtInsert.Location = new System.Drawing.Point(60, 92);
            this.txtInsert.Name = "txtInsert";
            this.txtInsert.Size = new System.Drawing.Size(100, 25);
            this.txtInsert.TabIndex = 23;
            this.txtInsert.TextChanged += new System.EventHandler(this.txtInsert_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(385, 142);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(385, 92);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(169, 92);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 20;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click_1);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(60, 33);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 724);
            this.Controls.Add(this.txtPrint);
            this.Controls.Add(this.MaxDegree);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsertFile);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.txtDel);
            this.Controls.Add(this.txtInsert);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnPrint);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrint;
        private System.Windows.Forms.ComboBox MaxDegree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInsertFile;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtDel;
        private System.Windows.Forms.TextBox txtInsert;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnPrint;
    }
}

