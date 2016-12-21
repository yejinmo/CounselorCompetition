using CounselorCompetition.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CounselorCompetition
{
    public partial class Form_DeleteStudentsWithoutNoImg : Form
    {
        public Form_DeleteStudentsWithoutNoImg()
        {
            InitializeComponent();
        }
        public Form_DeleteStudentsWithoutNoImg(List<string> StudentList)
        {
            InitializeComponent();
            ListView_Student.Items.Clear();
            foreach (var st in StudentList)
            {
                var str = st.Split('-');
                if (str.Length == 3)
                {
                    var lvi = new ListViewItem(str[0].Trim());
                    lvi.SubItems.Add(str[1].Trim());
                    lvi.SubItems.Add(str[2].Trim());
                    ListView_Student.Items.Add(lvi);
                }
            }
            groupBox1.Text = groupBox1.Text + " - 共计 " + StudentList.Count + " 项";
        }

        private void Form_DeleteStudentsWithoutNoImg_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("此操作将永久性地删除不存在照片的学生信息\n\n建议已经导入所有学生信息后在进行此项操作\n\n请确认操作", "提示", messButton);
                if (dr == DialogResult.OK)
                {
                    Enabled = false;
                    button1.Text = "删除中";
                    new SQLiteHelper().DeleteStudentsWithoutNoImg(null, true);
                    MessageBox.Show(this, "操作成功完成，以上学生信息因不存在照片文件已被成功删除\n\n", "提示");
                    Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Enabled = true;
            }
        }
    }
}
