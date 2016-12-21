namespace CounselorCompetition
{
    partial class Form_Score
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
            this.dgv_Teacher = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Teacher)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Teacher
            // 
            this.dgv_Teacher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Teacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Teacher.Location = new System.Drawing.Point(0, 0);
            this.dgv_Teacher.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_Teacher.Name = "dgv_Teacher";
            this.dgv_Teacher.RowTemplate.Height = 23;
            this.dgv_Teacher.Size = new System.Drawing.Size(894, 494);
            this.dgv_Teacher.TabIndex = 2;
            // 
            // Form_Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 494);
            this.Controls.Add(this.dgv_Teacher);
            this.Name = "Form_Score";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "比赛得分";
            this.Load += new System.EventHandler(this.Form_Score_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Teacher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Teacher;
    }
}