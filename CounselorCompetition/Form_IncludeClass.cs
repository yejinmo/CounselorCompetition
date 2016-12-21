using CounselorCompetition.Database;
using CounselorCompetition.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CounselorCompetition
{
    public partial class Form_IncludeClass : Form
    {
        List<TeacherInfoStruct> TeacherList;
        List<string> AllMajorList;
        List<string> CurrentAllClassList;

        string CurrentMajor;
        string CurrentName;
        string CurrentSelectMajor;
        string CurrentSelectClass;

        public Form_IncludeClass()
        {
            InitializeComponent();
        }

        public Form_IncludeClass(List<TeacherInfoStruct> teacherList, List<string> allMajorList)
        {
            TeacherList = teacherList;
            AllMajorList = allMajorList;
            InitializeComponent();
        }

        private void Form_IncludeClass_Load(object sender, EventArgs e)
        {
            ListView_Teahcer.Items.Clear();
            foreach (var st in TeacherList)
            {
                var lvi = new ListViewItem(st.Major);
                lvi.SubItems.Add(st.Name);
                ListView_Teahcer.Items.Add(lvi);
            }
            ListView_AllMajor.Items.Clear();
            foreach (var st in AllMajorList)
            {
                var lvi = new ListViewItem(st);
                ListView_AllMajor.Items.Add(lvi);
            }
        }

        private void CheckBox_AllowUserEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_AllowUserEdit.Checked)
                Text_IncludeClass.ReadOnly = false;
            else
                Text_IncludeClass.ReadOnly = true;
        }

        private void Text_IncludeClass_TextChanged(object sender, EventArgs e)
        {
            Button_Save.Enabled = true;
            Button_Save.Text = "保存";
            if (!string.IsNullOrEmpty(Text_IncludeClass.Text))
            {
                Button_Clear.Enabled = true;
                Button_Clear.Text = "清空";
            }
            else
            {
                Button_Clear.Enabled = false;
                Button_Clear.Text = "清空";
            }
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否清空？", "提示", messButton);
            if (dr == DialogResult.OK)
            {
                Text_IncludeClass.Text = string.Empty;
                Button_Clear.Enabled = false;
                Button_Clear.Text = "已清空";
            }
        }

        private void ListView_Teahcer_DoubleClick(object sender, EventArgs e)
        {
            if (ListView_Teahcer.SelectedItems.Count > 0)
            {
                Enabled = false;
                CurrentMajor = ListView_Teahcer.SelectedItems[0].Text;
                CurrentName = ListView_Teahcer.SelectedItems[0].SubItems[1].Text;
                Text_IncludeClass.Text = new SQLiteHelper().GetTeacherIncludeClass(CurrentMajor, CurrentName); 
                Text = "当前选中教师： " + CurrentMajor + " - " + CurrentName;
                Enabled = true;
            }
        }

        private void ListView_AllMajor_DoubleClick(object sender, EventArgs e)
        {
            if (ListView_AllMajor.SelectedItems.Count > 0)
            {
                Enabled = false;
                CurrentSelectMajor = ListView_AllMajor.SelectedItems[0].Text;
                CurrentAllClassList = new SQLiteHelper().GetAllClassByMajor(CurrentSelectMajor);
                ListView_Class.Items.Clear();
                foreach (var st in CurrentAllClassList)
                {
                    var lvi = new ListViewItem(st);
                    if (Directory.Exists(string.Format("Data/IMG/{0}/{1}", CurrentSelectMajor, st)))
                        lvi.SubItems.Add("相册存在[" + new DirectoryInfo(string.Format("Data/IMG/{0}/{1}", CurrentSelectMajor, st)).GetFiles().Length + "张]");
                    else
                        lvi.SubItems.Add("相册不存在");
                    ListView_Class.Items.Add(lvi);
                }
                Enabled = true;
            }
        }

        private void ListView_Class_DoubleClick(object sender, EventArgs e)
        {
            if (ListView_Class.SelectedItems.Count > 0)
            {
                CurrentSelectClass = ListView_Class.SelectedItems[0].Text;
                Text_IncludeClass.Text = Text_IncludeClass.Text + (string.IsNullOrEmpty(Text_IncludeClass.Text) ? "" : ",") + CurrentSelectMajor + "." + CurrentSelectClass;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ListView_Class.SelectedItems.Count > 0)
            {
                CurrentSelectClass = ListView_Class.SelectedItems[0].Text;
                Text_IncludeClass.Text = Text_IncludeClass.Text + (string.IsNullOrEmpty(Text_IncludeClass.Text) ? "" : ",") + CurrentSelectMajor + "." + CurrentSelectClass;
            }
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否保存？", "提示", messButton);
            if (dr == DialogResult.OK)
            {
                Enabled = false;
                Button_Save.Text = "保存中";
                new SQLiteHelper().SetTeacherIncludeClass(CurrentName, CurrentMajor, Text_IncludeClass.Text);
                Button_Save.Enabled = false;
                Button_Save.Text = "已保存";
                Enabled = true;
            }
            
        }
    }
}
