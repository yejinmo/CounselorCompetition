using CounselorCompetition.Database;
using CounselorCompetition.Struct;
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
    public partial class Form_Score : Form
    {
        public Form_Score()
        {
            InitializeComponent();
        }

        private void Form_Score_Load(object sender, EventArgs e)
        {
            try
            {
                dgv_Teacher.DataSource = new SQLiteHelper().GetAllTeacherScore();
                dgv_Teacher.DataMember = "ScoreTable";
                dgv_Teacher.Columns[0].HeaderText = "系别";
                dgv_Teacher.Columns[1].HeaderText = "姓名";
                dgv_Teacher.Columns[2].HeaderText = "得分";
                dgv_Teacher.Columns[3].HeaderText = "耗时";
            }
            catch { }
        }

    }
}
