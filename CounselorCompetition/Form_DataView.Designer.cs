namespace CounselorCompetition
{
    partial class Form_DataView
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_Teacher = new System.Windows.Forms.DataGridView();
            this.dgv_Student = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Teacher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Student)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 492);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_Teacher);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(703, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "教师数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_Student);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(703, 466);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "学生数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_Teacher
            // 
            this.dgv_Teacher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Teacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Teacher.Location = new System.Drawing.Point(3, 3);
            this.dgv_Teacher.Name = "dgv_Teacher";
            this.dgv_Teacher.RowTemplate.Height = 23;
            this.dgv_Teacher.Size = new System.Drawing.Size(697, 460);
            this.dgv_Teacher.TabIndex = 1;
            this.dgv_Teacher.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Teacher_CellContentClick);
            // 
            // dgv_Student
            // 
            this.dgv_Student.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Student.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Student.Location = new System.Drawing.Point(3, 3);
            this.dgv_Student.Name = "dgv_Student";
            this.dgv_Student.RowTemplate.Height = 23;
            this.dgv_Student.Size = new System.Drawing.Size(697, 460);
            this.dgv_Student.TabIndex = 2;
            // 
            // Form_DataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 492);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form_DataView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据浏览";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_DataView_FormClosed);
            this.Load += new System.EventHandler(this.Form_DataView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Teacher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Student)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_Teacher;
        private System.Windows.Forms.DataGridView dgv_Student;
    }
}