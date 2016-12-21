namespace CounselorCompetition
{
    partial class Form_IncludeClass
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListView_Teahcer = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Text_IncludeClass = new System.Windows.Forms.TextBox();
            this.CheckBox_AllowUserEdit = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ListView_AllMajor = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ListView_Class = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_Clear = new System.Windows.Forms.Button();
            this.Button_Save = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListView_Teahcer);
            this.groupBox1.Location = new System.Drawing.Point(18, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 421);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "教师列表";
            // 
            // ListView_Teahcer
            // 
            this.ListView_Teahcer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ListView_Teahcer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Teahcer.FullRowSelect = true;
            this.ListView_Teahcer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Teahcer.HideSelection = false;
            this.ListView_Teahcer.Location = new System.Drawing.Point(3, 17);
            this.ListView_Teahcer.MultiSelect = false;
            this.ListView_Teahcer.Name = "ListView_Teahcer";
            this.ListView_Teahcer.Size = new System.Drawing.Size(199, 401);
            this.ListView_Teahcer.TabIndex = 0;
            this.ListView_Teahcer.UseCompatibleStateImageBehavior = false;
            this.ListView_Teahcer.View = System.Windows.Forms.View.Details;
            this.ListView_Teahcer.DoubleClick += new System.EventHandler(this.ListView_Teahcer_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "系别";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "教师姓名";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Text_IncludeClass);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(601, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "包含班级";
            // 
            // Text_IncludeClass
            // 
            this.Text_IncludeClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text_IncludeClass.Location = new System.Drawing.Point(3, 17);
            this.Text_IncludeClass.Multiline = true;
            this.Text_IncludeClass.Name = "Text_IncludeClass";
            this.Text_IncludeClass.ReadOnly = true;
            this.Text_IncludeClass.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Text_IncludeClass.Size = new System.Drawing.Size(595, 82);
            this.Text_IncludeClass.TabIndex = 0;
            this.Text_IncludeClass.TextChanged += new System.EventHandler(this.Text_IncludeClass_TextChanged);
            // 
            // CheckBox_AllowUserEdit
            // 
            this.CheckBox_AllowUserEdit.AutoSize = true;
            this.CheckBox_AllowUserEdit.Location = new System.Drawing.Point(622, 11);
            this.CheckBox_AllowUserEdit.Name = "CheckBox_AllowUserEdit";
            this.CheckBox_AllowUserEdit.Size = new System.Drawing.Size(144, 16);
            this.CheckBox_AllowUserEdit.TabIndex = 2;
            this.CheckBox_AllowUserEdit.Text = "允许手动编辑包含班级";
            this.CheckBox_AllowUserEdit.UseVisualStyleBackColor = true;
            this.CheckBox_AllowUserEdit.CheckedChanged += new System.EventHandler(this.CheckBox_AllowUserEdit_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ListView_AllMajor);
            this.groupBox3.Location = new System.Drawing.Point(229, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 421);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "全部专业列表";
            // 
            // ListView_AllMajor
            // 
            this.ListView_AllMajor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.ListView_AllMajor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_AllMajor.FullRowSelect = true;
            this.ListView_AllMajor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListView_AllMajor.HideSelection = false;
            this.ListView_AllMajor.Location = new System.Drawing.Point(3, 17);
            this.ListView_AllMajor.MultiSelect = false;
            this.ListView_AllMajor.Name = "ListView_AllMajor";
            this.ListView_AllMajor.Size = new System.Drawing.Size(239, 401);
            this.ListView_AllMajor.TabIndex = 1;
            this.ListView_AllMajor.UseCompatibleStateImageBehavior = false;
            this.ListView_AllMajor.View = System.Windows.Forms.View.Details;
            this.ListView_AllMajor.DoubleClick += new System.EventHandler(this.ListView_AllMajor_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "专业";
            this.columnHeader3.Width = 215;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ListView_Class);
            this.groupBox4.Location = new System.Drawing.Point(480, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(286, 421);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "专业下属班级列表";
            // 
            // ListView_Class
            // 
            this.ListView_Class.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.ListView_Class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Class.FullRowSelect = true;
            this.ListView_Class.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListView_Class.HideSelection = false;
            this.ListView_Class.Location = new System.Drawing.Point(3, 17);
            this.ListView_Class.MultiSelect = false;
            this.ListView_Class.Name = "ListView_Class";
            this.ListView_Class.Size = new System.Drawing.Size(280, 401);
            this.ListView_Class.TabIndex = 1;
            this.ListView_Class.UseCompatibleStateImageBehavior = false;
            this.ListView_Class.View = System.Windows.Forms.View.Details;
            this.ListView_Class.DoubleClick += new System.EventHandler(this.ListView_Class_DoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "班级";
            this.columnHeader4.Width = 140;
            // 
            // Button_Clear
            // 
            this.Button_Clear.Enabled = false;
            this.Button_Clear.Location = new System.Drawing.Point(622, 62);
            this.Button_Clear.Name = "Button_Clear";
            this.Button_Clear.Size = new System.Drawing.Size(141, 23);
            this.Button_Clear.TabIndex = 5;
            this.Button_Clear.Text = "清空";
            this.Button_Clear.UseVisualStyleBackColor = true;
            this.Button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // Button_Save
            // 
            this.Button_Save.Enabled = false;
            this.Button_Save.Location = new System.Drawing.Point(622, 33);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(141, 23);
            this.Button_Save.TabIndex = 6;
            this.Button_Save.Text = "保存";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(622, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "添加选中班级";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "备注";
            this.columnHeader5.Width = 110;
            // 
            // Form_IncludeClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 545);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Button_Save);
            this.Controls.Add(this.Button_Clear);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CheckBox_AllowUserEdit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_IncludeClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑教师班级";
            this.Load += new System.EventHandler(this.Form_IncludeClass_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView ListView_Teahcer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Text_IncludeClass;
        private System.Windows.Forms.CheckBox CheckBox_AllowUserEdit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView ListView_AllMajor;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView ListView_Class;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button Button_Clear;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}