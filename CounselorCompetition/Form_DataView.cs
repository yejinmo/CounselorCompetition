using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CounselorCompetition.Database;

namespace CounselorCompetition
{
    public partial class Form_DataView : Form
    {

        Form Frm_Parent;

        public Form_DataView()
        {
            InitializeComponent();
        }

        public Form_DataView(Form frm)
        {
            Frm_Parent = frm;
            InitializeComponent();
        }

        private void Form_DataView_Load(object sender, EventArgs e)
        {
            try
            {
                dgv_Teacher.AutoResizeColumns();
                dgv_Teacher.DataSource = new SQLiteHelper().GetAllTeacher();
                dgv_Teacher.DataMember = "TeacherInfo";
                dgv_Teacher.Columns[0].HeaderText = "姓名";
                dgv_Teacher.Columns[1].HeaderText = "系别";
                dgv_Teacher.Columns[2].HeaderText = "包含班级";
                /*
                 [Name], [Gender], [Class], [Major], [PoliticalStatus], 
        [Nation], [Post], [Address], [Dorm], [DormMember],
        [Economic], [BonusAndPenalty], [Study], [Habby]
                 */
                dgv_Student.AutoResizeColumns();
                dgv_Student.DataSource = new SQLiteHelper().GetAllStudent();
                dgv_Student.DataMember = "StudentInfo";
                dgv_Student.Columns[0].HeaderText = "学号";
                dgv_Student.Columns[1].HeaderText = "姓名";
                dgv_Student.Columns[2].HeaderText = "性别";
                dgv_Student.Columns[3].HeaderText = "班级";
                dgv_Student.Columns[4].HeaderText = "专业";
                dgv_Student.Columns[5].HeaderText = "政治面貌";
                dgv_Student.Columns[6].HeaderText = "民族";
                dgv_Student.Columns[7].HeaderText = "担任职务";
                dgv_Student.Columns[8].HeaderText = "家庭住址";
                dgv_Student.Columns[9].HeaderText = "宿舍号";
                dgv_Student.Columns[10].HeaderText = "宿舍成员";
                dgv_Student.Columns[11].HeaderText = "家庭经济情况";
                dgv_Student.Columns[12].HeaderText = "奖惩情况";
                dgv_Student.Columns[13].HeaderText = "学习情况";
                dgv_Student.Columns[14].HeaderText = "爱好";
                dgv_Student.Columns[15].HeaderText = "职业倾向";
            }
            catch { }
        }
         
        private void Form_DataView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Frm_Parent != null)
                Frm_Parent.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_Teacher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
