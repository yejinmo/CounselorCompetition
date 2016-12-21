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
    public partial class Form_Config : Form
    {

        Form Frm_Parent;
        Color CurrentTextColor = Color.White;

        public Form_Config(Form frm)
        {
            
            Frm_Parent = frm;
            InitializeComponent();
        }

        public Form_Config()
        {
            InitializeComponent();
        }

        private void Form_Config_Load(object sender, EventArgs e)
        {
            LoadBGImg();
            ListScreen();
            if (CurrentTextColor == Color.White)
                radioButton1.Checked = true;
            else if (CurrentTextColor == Color.Black)
                radioButton2.Checked = true;
            else
                radioButton3.Checked = true;
            Link_Version.Text = "版本号： " + Application.ProductVersion.ToString();    
        }

        private void Form_Config_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            //new SQLiteHelper().SetConfig(CurrentTextColor);

            if (Frm_Parent != null)
                Frm_Parent.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(LoadExcelProcess));
            t.Start();
        }

        private void LoadExcelProcess()
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                bool NeedReturn = false;
                Invoke((EventHandler)delegate
                {
                    Enabled = false;
                    MessageBox.Show(this, "\n\n数据表格式如下且需包含表头：\n序号|姓名|性别|班级|专业|政治|面貌|民族|担任职务|家庭住址|宿舍|宿舍成员|家庭经济状况|奖惩情况|学习状况|兴趣爱好\n\n不能重复导入相同的数据表\n\n", "重要提示");
                    openFile.Title = "请选择目标Excel表";
                    openFile.Filter = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx";
                    openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    openFile.Multiselect = true;
                    if (openFile.ShowDialog() == DialogResult.Cancel)
                        NeedReturn = true;
                });
                if (NeedReturn)
                    return;
                Invoke((EventHandler)delegate
                {
                    Text = "正在导入中，请耐心等候";
                });
                //var dt = ExcelHelper.GetDataFromExcelByConn(openFile.FileName, true);
                string input_success = string.Empty;
                string input_error = string.Empty;
                int input_success_total = 0;
                SQLiteHelper sqlLiteHelper = new SQLiteHelper();
                foreach (var fs in openFile.FileNames)
                {
                    var dt = ExcelHelper.GetDataFromExcelByConn(fs, true);
                    int temp_input_success_total = 0;
                    if (sqlLiteHelper.InsertNewStudent(dt, input_success_total, ref temp_input_success_total, this))
                    {
                        input_success = input_success + fs + "\n";
                        input_success_total += temp_input_success_total;
                    }
                    else
                    {
                        input_error = input_error + fs + "\n";
                        //Invoke((EventHandler)delegate
                        //{
                        //    MessageBox.Show(this, "未导入任何数据\n\n\"" + fs + "\"\n\n请检查格式是否有误", "导入失败");
                        //});
                    }
                }
                sqlLiteHelper.Close();
                Invoke((EventHandler)delegate
                {
                    MessageBox.Show(this, string.Format("{0}\n\n{1}\n{2}",
                        (string.Format("成功导入 {0} 名学生数据", input_success_total)),
                        (string.IsNullOrEmpty(input_success) ? "" : string.Format("以下文件的数据均已成功导入\n\n{0}", input_success)),
                        (string.IsNullOrEmpty(input_error) ? "" : string.Format("以下文件的数据导入失败，请检查格式是否有误\n\n{0}", input_error))
                        ), "导入完成");
                    if(input_success_total > 0)
                        MessageBox.Show(this, "\n\n请确保学生照片位于(程序运行目录/Data/IMG/[对应专业]/[对应年级]/[对应姓名.jpg])中\n\n如不存在，学生照片将不能正常显示", "提示");
                });
            }
            catch (Exception e)
            {
                Invoke((EventHandler)delegate
                {
                    MessageBox.Show(this, e.Message, "导入失败");
                });
            }
            finally
            {
                Invoke((EventHandler)delegate
                {
                    Invoke((EventHandler)delegate
                    {
                        Text = "设置";
                    });
                    Enabled = true;
                });
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("此操作将永久性地清空学生表数据库，图片文件将会被保留，请确认操作", "提示", messButton);
                if (dr == DialogResult.OK)
                {
                    Text = "重置中，请耐心等候";
                    new SQLiteHelper().DeleteTableStudent();
                    MessageBox.Show(this, "操作成功完成", "提示");
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Text = "设置";
                Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(LoadTeacherProcess));
            t.Start();
        }

        private void LoadTeacherProcess()
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                bool NeedReturn = false;
                Invoke((EventHandler)delegate
                {
                    Enabled = false;
                    MessageBox.Show(this, "*导入数据仅支持(.xls)格式的数据文件，如不是请自行更改保存格式\n\n*数据表格式如下且需包含表头：\n姓名|系别|包含班级\n*姓名：参赛教师姓名\n*系别：参赛教师所属系\n*包含班级：参赛教师的所有班级，格式为\n[专业][.][班级][,][专业][.][班级][,]……………………[专业][.][班级]\n如：软件工程.2016级01班,软件工程.2016级02班\n\".\"及\",\"均为英文状态下标点，[专业]及[班级]需与学生数据表中的对应字段相匹配\n\n*不能重复导入相同的数据表\n\n*由于不遵守此规定导致的一切错误请自行承担", "重要提示");
                    openFile.Title = "请选择目标Excel表";
                    openFile.Filter = "Excel(*.xls)|*.xls";
                    openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    openFile.Multiselect = false;
                    if (openFile.ShowDialog() == DialogResult.Cancel)
                        NeedReturn = true;
                });
                if (NeedReturn)
                    return;
                var dt = ExcelHelper.GetDataFromExcelByConn(openFile.FileName, true);
                SQLiteHelper sqlLiteHelper = new SQLiteHelper();
                if (sqlLiteHelper.InsertNewTeacher(dt))
                {
                    Invoke((EventHandler)delegate
                    {
                        MessageBox.Show(this, "数据已经成功导入", "导入成功");
                    });
                }
                sqlLiteHelper.Close();
            }
            catch (Exception e)
            {
                Invoke((EventHandler)delegate
                {
                    MessageBox.Show(this, e.Message, "导入失败");
                });
            }
            finally
            {
                Invoke((EventHandler)delegate
                {
                    Text = "设置";
                    Enabled = true;
                });
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                //MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                //DialogResult dr = MessageBox.Show("此操作将永久性地删除不存在照片的学生信息\n\n建议已经导入所有学生信息后在进行此项操作\n\n请确认操作", "提示", messButton);
                //if (dr == DialogResult.OK)
                //{
                    Text = "检测中，请耐心等候";
                    var list = new SQLiteHelper().DeleteStudentsWithoutNoImg(this);
                    if (list.Count > 0)
                    {
                        var frm = new Form_DeleteStudentsWithoutNoImg(list);
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show(this, "操作成功完成，没有发现需要优化的学生信息\n\n", "提示");
                    //Text = "删除中，请耐心等候";
                    ////
                    //var t = new SQLiteHelper().DeleteStudentsWithoutNoImg(this);
                    ////
                    //if (string.IsNullOrEmpty(t))
                    //    MessageBox.Show(this, "操作成功完成，没有学生信息因不存在照片文件而被删除\n\n", "提示");
                    //else
                    //    MessageBox.Show(this, "操作成功完成，以下学生信息因不存在照片文件已被成功删除\n\n" + t, "提示");
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Text = "设置";
                Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Title = "请选择自定义背景图";
                openFile.Filter = "JPEG图像(*.jpg)|*.jpg";
                openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFile.Multiselect = false;
                if (openFile.ShowDialog() == DialogResult.Cancel)
                    return;
                if (!string.IsNullOrEmpty(openFile.FileName))
                {
                    if (File.Exists("Data/bg.jpg"))
                    {
                        File.Delete("Data/bg.jpg");
                    }
                    File.Copy(openFile.FileName, "Data/bg.jpg");
                }
                LoadBGImg();
                MessageBox.Show(this, "操作成功完成", "提示");
            }
            catch(Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("此操作将恢复默认背景图\n\n请确认操作", "提示", messButton);
                if (dr == DialogResult.OK)
                {
                    Text = "恢复中";
                    //
                    if (File.Exists("Data/bg.jpg"))
                    {
                        File.Delete("Data/bg.jpg");
                    }
                    //
                    LoadBGImg();
                    MessageBox.Show(this, "操作成功完成", "提示");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Text = "设置";
                Enabled = true;
            }
        }

        private void LoadBGImg()
        {
            try
            {
                if (File.Exists("Data/bg.jpg"))
                {
                    Image image = Image.FromFile("Data/bg.jpg");
                    Image cloneImage = new Bitmap(image);
                    image.Dispose();
                    pic_bg.Image = cloneImage;
                }
                else
                    pic_bg.Image = Properties.Resources.IMG_BG;
            }
            catch
            {
                pic_bg.Image = Properties.Resources.IMG_BG;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Form_Score frm = new Form_Score();
            frm.ShowDialog();
            Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            var frm = new Form_DataView(this);
            frm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ColorDialog loColorForm = new ColorDialog();
            loColorForm.Color = CurrentTextColor;
            var res = loColorForm.ShowDialog();
            if (res == DialogResult.Yes || res == DialogResult.OK)
            {
                CurrentTextColor = loColorForm.Color;
                radioButton3.Checked = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(LoadMianForm));
            t.Start();
        }

        private void LoadMianForm()
        {
            try
            {
                Invoke((EventHandler)delegate
                {
                    Text = "启动中";
                    Enabled = false;
                });
                List<TeacherInfoStruct> TeacherInfoList = new SQLiteHelper().GetTeacherInfoList();
                Invoke((EventHandler)delegate
                {
                    Form_Main frm = new Form_Main(this, TeacherInfoList, CurrentTextColor, ComboBox_ScreenList.SelectedIndex);
                    frm.Show();
                    Hide();
                });
            }
            catch(Exception e)
            {
                Invoke((EventHandler)delegate
                {
                    MessageBox.Show(this, e.Message, "错误");
                });
            }
            finally
            {
                Invoke((EventHandler)delegate
                {
                    Text = "设置";
                    Enabled = true;
                });
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTextColor = Color.White;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTextColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("此操作将永久性地清空教师表数据库，图片文件将会被保留，请确认操作", "提示", messButton);
                if (dr == DialogResult.OK)
                {
                    Text = "重置中，请耐心等候";
                    new SQLiteHelper().DeleteTableTeacher();
                    MessageBox.Show(this, "操作成功完成", "提示");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Text = "设置";
                Enabled = true;
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Enabled = false;
            var sqlhelper = new SQLiteHelper();
            var teacherList = sqlhelper.GetTeacherInfoList();
            var allMajorList = sqlhelper.GetAllMajor();
            sqlhelper.Close();
            Form_IncludeClass frm = new Form_IncludeClass(teacherList, allMajorList);
            frm.ShowDialog();
            Enabled = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "http://www.yejinmo.com/CounselorCompetition");
        }

        private void Link_Version_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "http://www.yejinmo.com/CounselorCompetition-update");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel文件（*.xls）|*.xls";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                    string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                    ExcelHelper.SaveToFile(ExcelHelper.RenderToExcel(new SQLiteHelper().GetAllTeacherScore().Tables[0]), localFilePath);
                    MessageBox.Show(this, "导出文件保存成功\n\n" + localFilePath, "提示");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "导出失败\n\n" + ee.Message, "错误");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("此操作将永久性地清空比赛成绩，请确认操作", "提示", messButton);
                if (dr == DialogResult.OK)
                {
                    Text = "重置中，请耐心等候";
                    new SQLiteHelper().DeleteAllTeacherScore();
                    MessageBox.Show(this, "操作成功完成", "提示");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, ee.Message + "\n\n如果此错误重复出现，请尝试重新运行程序并重试此操作", "发生了一个不可预知的错误");
            }
            finally
            {
                Text = "设置";
                Enabled = true;
            }

        }

        /// <summary>
        /// 列出当前显示器
        /// </summary>
        private void ListScreen()
        {
            Enabled = false;
            int count = Screen.AllScreens.Count();
            ComboBox_ScreenList.Items.Clear();
            for (int i = 0; i < count; ++i)
            {
                ComboBox_ScreenList.Items.Add(
                    Screen.AllScreens[i].DeviceName +
                    string.Format("[{0}x{1}]", Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height) +
                    string.Format("{0}", Screen.AllScreens[i].Primary ? "[主显示器]" : ""));
                if (Screen.AllScreens[i].Primary)
                    ComboBox_ScreenList.SelectedIndex = i;
            }
            Enabled = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListScreen();
        }
    }
}
