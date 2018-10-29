using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CounselorCompetition.ImageAlgorithm;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using CounselorCompetition.Controls;
using CounselorCompetition.Database;
using CounselorCompetition.Struct;
using System.Threading;
using System.IO;

namespace CounselorCompetition
{
    public partial class Form_Main : Form
    {

        private void TestSub()
        {
        }

        #region 全局

        List<TeacherInfoStruct> TeacherInfoList;
        TeacherInfoStruct CurrentSelectTeacherInfo;
        Form Frm_Parent;
        Size NormalSzie;
        Point NormalLocation;
        GaussianBlur GB;
        Bitmap PreImage_BG;
        Bitmap PreImage_AlphaCover;
        Bitmap PreImage_Header;
        Color Color_PrimaryColor;
        Color CurrentTextColor;
        Color Color_SecondColor;
        //SQLiteHelper sqliteHelper = new SQLiteHelper();
        bool ControlNeedRefresh;
        int Header_Height;
        int currentSocre = 0;

        int CurrentSocre
        {
            get
            {
                return currentSocre;
            }

            set
            {

                currentSocre = value;
                int ScoreLen = value.ToString().Length;
                moUI_Label_Score._Text = "得分：";
                while ((3 - ScoreLen) > 0)
                {
                    moUI_Label_Score._Text += " ";
                    ScoreLen++;
                }
                moUI_Label_Score._Text += value;
            }
        }

        #endregion

        #region UI

        int showOnMonitor = 0;

        public Form_Main(Form frm, List<TeacherInfoStruct> l, Color TextColor, int ShowOnMonitor)
        {
            CurrentTextColor = TextColor;
            Frm_Parent = frm;
            TeacherInfoList = l;
            showOnMonitor = ShowOnMonitor;
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            MakeMaxForm();
            ShowOnMonitor(showOnMonitor);
            NormalSzie = Size;
            ControlNeedRefresh = true;
            NormalLocation = Location;
            Color_PrimaryColor = Color.Black;
            Header_Height = 80;
            GB = new GaussianBlur(10);
            LoadBGImage();
            LoadPanel1();
            LoadPanel2();
            LoadPanel3();
            ScoreListView.Size = new Size(857, 525);
            moUI_Panel1.Visible = false;
            moUI_Panel2.Visible = false;
            moUI_Panel3.Visible = false;
            Form_Main_Resize(null, null);
            LoadOptionButton();
            MoUI_Controler.SetTextColor(Controls, CurrentTextColor);
            MoUI_Controler.SetTextColor(moUI_Panel1.Controls, CurrentTextColor);
            MoUI_Controler.SetTextColor(moUI_Panel2.Controls, CurrentTextColor);
            MoUI_Controler.SetTextColor(moUI_Panel3.Controls, CurrentTextColor);
            MoUI_Controler.SetTextColor(PanelOption.Controls, CurrentTextColor);
            Invalidate();
            TestSub();
        }

        private void ShowOnMonitor(int showOnMonitor)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            if (showOnMonitor >= sc.Length)
            {
                showOnMonitor = 0;
            }


            StartPosition = FormStartPosition.Manual;
            Location = new Point(sc[showOnMonitor].Bounds.Left, sc[showOnMonitor].Bounds.Top);
            Size = new Size(sc[showOnMonitor].Bounds.Width, sc[showOnMonitor].Bounds.Height);
            // If you intend the form to be maximized, change it to normal then maximized.  
        }

        private void LoadBGImage()
        {
            {
                PreImage_AlphaCover = Properties.Resources.IMG_AlphaCover;
            }
            {
                if (File.Exists("Data/bg.jpg"))
                {
                    Image image = Image.FromFile("Data/bg.jpg");
                    Image cloneImage = new Bitmap(image);
                    image.Dispose();
                    PreImage_BG = (Bitmap)cloneImage;
                }
                else
                    PreImage_BG = Properties.Resources.IMG_BG;
                //PreImage_BG = ImageZoom.GetThumbnail(PreImage_BG, PreImage_BG.Height * 1920 / PreImage_BG.Width, 1920);
                PreImage_BG = ImageEx.SmallPic(PreImage_BG, 1920);
                {
                    //Bitmap b = new Bitmap(Width, Height);
                    //Graphics g = Graphics.FromImage();
                }
                //PreImage_BG.Save("0.jpg");
                Color_PrimaryColor = ColorHelper.GetPrimaryColor(PreImage_BG);
                Color_SecondColor = ImageEx.ChangeColor(Color_PrimaryColor, -0.75f);
                PreImage_AlphaCover = (Bitmap)ImageEx.SetImageColorAllWithoutAlpha(PreImage_AlphaCover, Color.Black, Color_SecondColor, 255);
                //PreImage_AlphaCover.Save("2.png");
                PreImage_BG = ImageEx.JoinMImage(PreImage_BG, PreImage_AlphaCover, new Rectangle(0, 0, PreImage_AlphaCover.Width, PreImage_AlphaCover.Height));
                //BackgroundImage = PreImage_BG;
            }
            {
                Graphics g;
                PreImage_Header = new Bitmap(PreImage_BG.Width, Header_Height);
                g = Graphics.FromImage(PreImage_Header);
                Rectangle srcRect = new Rectangle(0, 0, PreImage_Header.Width, PreImage_Header.Height);
                GraphicsUnit units = GraphicsUnit.Pixel;
                g.DrawImage(PreImage_BG, 0, 0, srcRect, units);
                g.Save();
                g.Dispose();
                PreImage_Header = ImageEx.BrightnessP(PreImage_Header, -10);
                PreImage_Header = GB.ProcessImage(PreImage_Header);

                //PreImage_Header.Save("1.jpg");
                //Bitmap b = new Bitmap(PictureBox_Header.Width, PictureBox_Header.Height);
                //g = Graphics.FromImage(b);
                //Pen p = new Pen(Color_PrimaryColor);
                //g.DrawImage(PreImage_Header, new Rectangle(0, 0, PictureBox_Header.Width, PictureBox_Header.Height), 
                //                             new Rectangle((PreImage_Header.Width - Width) / 2, 0, Width, Height), GraphicsUnit.Pixel);
                ////Pen p = new Pen(Color.Red);
                //g.DrawLine(p, new Point(0, 0), new Point(b.Width, 0));
                //g.DrawLine(p, new Point(b.Width - 1, 0), new Point(b.Width - 1, b.Height - 1));
                //g.DrawLine(p, new Point(b.Width - 1, b.Height - 1), new Point(0, b.Height - 1));
                //g.DrawLine(p, new Point(0, b.Height), new Point(0, 0));
                //g.Save();
                //g.Dispose();
            }
            ControlNeedRefresh = true;
        }

        private void LoadOptionButton()
        {
            Size ctlSize = new Size(110, 100);
            int _left = 4;
            int _top = 4;
            int _padding = 6;
            PanelOption._BGColorStyle = true;
            PanelOption.BackColor = Color_SecondColor;
            PanelOption.DrawLine(true, 119, Color_PrimaryColor);
            foreach (var l in TeacherInfoList)
            {
                MoUI_OptionBUtton op = new MoUI_OptionBUtton();
                op.Size = ctlSize;
                op._Text = l.Major;
                op._TextB = l.Name;
                op._BGColorStyle = true;
                op._BackColor = Color_SecondColor;
                PanelOption.Controls.Add(op);
                op.Left = _left;
                op.Top = _top;
                _top = _top + ctlSize.Height + _padding;
                op.teacherInfoStruct = l;
                op.MouseClick += delegate
                {
                    if (op._IsSelected == true)
                        return;
                    foreach (var ctl in op.Parent.Controls)
                    {
                        if (ctl is MoUI_OptionBUtton)
                        {
                            MoUI_OptionBUtton c = (MoUI_OptionBUtton)ctl;
                            c._IsSelected = false;
                        }
                    }
                    moUI_Label3.IsSelected = false;
                    moUI_Label1.IsSelected = true;
                    moUI_Label2.IsSelected = false;
                    op._IsSelected = true;
                    CurrentSelectTeacherInfo = op.teacherInfoStruct;
                    ScoreListView.Visible = false;
                    Thread t = new Thread(new ThreadStart(LoadTeacherAnswerMod));
                    t.Start();
                };
            }
        }

        private void MakeMaxForm()
        {
            if (IsMaximized)
            {
                Size = NormalSzie;
                Location = NormalLocation;
            }
            else
            {
                NormalSzie = Size;
                NormalLocation = Location;
                ShowOnMonitor(showOnMonitor);
                //Size = Screen.PrimaryScreen.Bounds.Size;
                //Location = new Point(0, 0);
            }
            IsMaximized = !IsMaximized;
            Refresh();
        }

        private void SetControlCenter(Control ctl, bool EnableTop = false, bool Offset = false)
        {
            //160, 80
            if (Offset)
            {
                ctl.Left = (ctl.Parent.Width - 160 - ctl.Width) / 2 + 160;
                if (EnableTop)
                    ctl.Top = (ctl.Parent.Height - 80 - ctl.Height) / 2 + 80;
            }
            else
            {
                ctl.Left = (ctl.Parent.Width - ctl.Width) / 2;
                if (EnableTop)
                    ctl.Top = (ctl.Parent.Height - ctl.Height) / 2;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                //Graphics g = e.Graphics;
                Bitmap b = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(b);
                Pen PrimaryColorPen = new Pen(Color.FromArgb(255, Color_PrimaryColor));
                //Pen PrimaryColorPen = new Pen(Color.FromArgb(250, Color.White));
                Pen BlackPen = new Pen(Color.Black);
                //UI重绘
                {
                    //Graphics g = Graphics.FromImage(FormBackGround);
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //Brush BackGroundBush = new SolidBrush(Color.Gray);
                    //g.Clear(Color.LightGray);
                    //for (int i = 100; i < Width; i += 100)
                    //{
                    //    for (int j = 100; j < Height; j += 100)
                    //    {
                    //        g.FillEllipse(BackGroundBush, i, j, 2, 2);
                    //    }
                    //}
                    g.DrawImage(PreImage_BG, new Rectangle(1, 1, Width - 2, Height - 2), new Rectangle((PreImage_BG.Width - Width) / 2 - 1, 1, Width - 2, Height - 2), GraphicsUnit.Pixel);
                    //g.Save();
                    //e.Graphics.DrawImage(FormBackGround, 0, 0);
                }
                //Paint Header
                {
                    g.DrawImage(PreImage_Header, new Rectangle(2, 2, Width - 4, Header_Height),
                        new Rectangle((PreImage_BG.Width - Width) / 2, 2, Width - 4, Header_Height), GraphicsUnit.Pixel);
                }
                //Paint Line
                {
                    g.DrawLine(BlackPen, new Point(0, 0), new Point(Width, 0));
                    g.DrawLine(BlackPen, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                    g.DrawLine(BlackPen, new Point(Width - 1, Height - 1), new Point(0, Height - 1));
                    g.DrawLine(BlackPen, new Point(0, Height), new Point(0, 0));

                    g.DrawLine(PrimaryColorPen, new Point(2, 1), new Point(Width - 2, 1));
                    g.DrawLine(PrimaryColorPen, new Point(Width - 2, 2), new Point(Width - 2, Height - 2));
                    g.DrawLine(PrimaryColorPen, new Point(Width - 3, Height - 2), new Point(1, Height - 2));
                    g.DrawLine(PrimaryColorPen, new Point(1, Height - 3), new Point(1, 1));

                    g.DrawLine(PrimaryColorPen, new Point(121, Header_Height), new Point(121, Height - 2));

                    g.DrawLine(PrimaryColorPen, new Point(2, Header_Height), new Point(Width - 3, Header_Height));
                }
                //Paint Control 
                {
                    //g.DrawImage(ImageEx.ImageFromText("test", new Font("微软雅黑", 30), Color.White, Color.FromArgb(125,Color.Black)), new Point(100, 100));
                }
                //Test Sub
                {
                    //GraphicsPath graphicsPath = new GraphicsPath();
                    //graphicsPath.AddEllipse(new Rectangle(0, 0, 200, 200));
                    //PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
                    //pathGradientBrush.CenterColor = Color.FromArgb(255, 232, 3);
                    //pathGradientBrush.CenterPoint = new PointF(100, 100);
                    //pathGradientBrush.SurroundColors = new Color[] { Color.Transparent };
                    //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //e.Graphics.FillEllipse(pathGradientBrush, new Rectangle(0, 0, 200, 200));
                    //graphicsPath.Dispose();
                    //pathGradientBrush.Dispose();
                }
                g.Save();
                e.Graphics.DrawImage(b, new Point(0, 0));
                if (ControlNeedRefresh && Panel_ResizeButton.Visible)
                {
                    ControlNeedRefresh = false;
                    MoUI_Controler.RefreshMoUIControlBackgroundImage(Controls, b, CurrentTextColor);
                }
                b.Dispose();
                g.Dispose();

            }
            catch //(Exception ex)
            {
                //
            }
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息

                return;

            base.WndProc(ref m);

        }

        private void Panel_ResizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            Panel_Header.SuspendLayout();
            Panel_Header.Visible = false;
            Panel_ResizeButton.Visible = false;
        }

        private void Panel_ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            Panel_Header.ResumeLayout();
            Panel_Header.Visible = true;
            Panel_ResizeButton.Visible = true;
        }

        private void Panel_ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
                Invalidate();
            }
        }

        private void Form_Main_Resize(object sender, EventArgs e)
        {
            SetControlCenter(moUI_Panel1, true, true);
            SetControlCenter(moUI_Panel2, true, true);
            SetControlCenter(moUI_Panel3, true, true);
            ScoreListView.BringToFront();
            SetControlCenter(ScoreListView, true, true);
            ControlNeedRefresh = true;
        }

        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Frm_Parent != null)
                Frm_Parent.Show();
        }

        private void PanelOption_Scroll(object sender, ScrollEventArgs e)
        {
        }

        #endregion

        #region Config

        private void LoadConfig()
        {

        }

        private void SaveConfig()
        {

        }

        #endregion

        #region Header

        bool IsMaximized;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Panel_Header_DoubleClick(object sender, EventArgs e)
        {
            return;
            if (IsMaximized)
            {
                Size = NormalSzie;
                Location = NormalLocation;
            }
            else
            {
                NormalSzie = Size;
                NormalLocation = Location;
                Size = Screen.PrimaryScreen.Bounds.Size;
                Location = new Point(0, 0);
            }
            IsMaximized = !IsMaximized;
            Refresh();
        }

        private void Panel_Header_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

        #endregion

        #region 答题模块

        private void LoadTeacherAnswerMod()
        {
            try
            {
                Invoke((EventHandler)delegate
                {
                    //Enabled = false;
                });
                if (CurrentSelectTeacherInfo.StudentList.Count <= 1)
                    return;
                QuestionControler.Reset(CurrentSelectTeacherInfo);
                Invoke((EventHandler)delegate
                {
                    moUI_Button3.Text = "下一个";
                    CurrentSocre = 0;
                    var temp = QuestionControler.GetMod_0Question();
                    TextCover_1_Name._Text = temp.Name;
                    TextCover_1_Gender._Text = temp.Gender;
                    TextCover_1_Class._Text = temp.Class;
                    TextCover_1_Major._Text = temp.Major;
                    TextCover_1_PoliticalStatus._Text = temp.PoliticalStatus;
                    TextCover_1_Nation._Text = temp.Nation;
                    TextCover_1_Post._Text = temp.Post;
                    TextCover_1_Address._Text = temp.Address;
                    TextCover_1_Dorm._Text = temp.Dorm;
                    TextCover_1_DormMember._Text = temp.DormMember;
                    TextCover_1_Economic._Text = temp.Economic;
                    TextCover_1_BonusAndPenalty._Text = temp.BonusAndPenalty;
                    TextCover_1_Study._Text = temp.Study;
                    TextCover_1_Habby._Text = temp.Habby;
                    TextCover_1_Job._Text = temp.Job;
                    TextCover_1_Number._Text = temp.Number;
                    PictureBox1._Image = GetStudentPhoto(temp.PhotoPath);
                    moUI_Label1.IsSelected = true;
                    moUI_Button1._Text = "下一个";
                    foreach (var ctl in moUI_Panel1.Controls)
                    {
                        if (ctl is MoUI_TextCover)
                        {
                            MoUI_TextCover c = (MoUI_TextCover)ctl;
                            c._IsShow = false;
                            c.Invalidate();
                        }
                        if (ctl is MoUI_Switch)
                        {
                            MoUI_Switch c = (MoUI_Switch)ctl;
                            c.IsSwitched = false;
                            c.Visible = false;
                            c.Invalidate();
                        }
                    }
                    moUI_Panel1.Visible = true;
                    moUI_Panel2.Visible = false;
                    moUI_Panel3.Visible = false;
                    TotalTime = 8 * 60;
                    Timer_TotalTime.Enabled = true;
                });
            }
            catch
            { }
            finally
            {
                Invoke((EventHandler)delegate
                {
                    //Enabled = true;
                });
            }
        }

        private void MakePanelTextCover_IsShow(MoUI_Panel p, bool isShow = false)
        {
            foreach (var ctl in p.Controls)
            {
                if (ctl is MoUI_TextCover)
                {
                    MoUI_TextCover c = (MoUI_TextCover)ctl;
                    c._IsShow = isShow;
                }
            }
        }

        private void MakePanelSwicth_IsSelect(MoUI_Panel p, bool isSwitched = false, bool AndMakeVisable = true, bool visable = false)
        {
            foreach (var ctl in p.Controls)
            {
                if (ctl is MoUI_Switch)
                {
                    MoUI_Switch c = (MoUI_Switch)ctl;
                    c.IsSwitched = isSwitched;
                    if (AndMakeVisable)
                        c.Visible = visable;
                }
            }
        }

        private Bitmap GetStudentPhoto(string photoPath)
        {
            try
            {
                Bitmap res;
                if (File.Exists(photoPath))
                {
                    res = (Bitmap)Image.FromFile(photoPath);
                }
                else if ((File.Exists(photoPath + ".jpg")))
                {
                    res = (Bitmap)Image.FromFile(photoPath + ".jpg");
                }
                else if ((File.Exists(photoPath + ".png")))
                {
                    res = (Bitmap)Image.FromFile(photoPath + ".png");
                }
                else if ((File.Exists(photoPath + ".jpg.jpg")))
                {
                    res = (Bitmap)Image.FromFile(photoPath + ".jpg.jpg");
                }
                else
                    return null;
                //if (res.Height / res.Width > 271.0 / 196.0)
                res = ImageEx.SmallPicH(res, 271);
                if (res.Width < 196)
                    res = ImageEx.SmallPic(res, 196);
                //else
                //    res = ImageEx.SmallPic(res, 196);
                res = ImageEx.CutImage(res, new Rectangle((res.Width - 196) / 2, 0, 196, 271));
                return res;
            }
            catch
            {
                return null;
            }
        }

        private int TotalTime = 480;

        private void Timer_TotalTime_Tick(object sender, EventArgs e)
        {
            TotalTime--;
            int min = TotalTime / 60;
            int sec = TotalTime % 60;
            //剩余时间：0分00秒
            LabelTotalTime._Text = "剩余时间：" + min + "分" + (sec < 10 ? "0" : "") + sec + "秒";
            if (TotalTime <= 0)
            {
                Timer_TotalTime.Enabled = false;
                moUI_Panel1.Visible = false;
                moUI_Panel2.Visible = false;
                moUI_Panel3.Visible = false;
                LoadSocreView();
            }
        }

        #endregion

        #region 大海捞针

        private void LoadPanel1()
        {
            moUI_Label2.IsSelected = false;
            moUI_Label1.IsSelected = true;
            moUI_Label3.IsSelected = false;
            moUI_Panel1.Size = new Size(819, 535 + 96);
            MakePanelTextCover_IsShow(moUI_Panel1, false);
            MakePanelSwicth_IsSelect(moUI_Panel1, false, true, false);
            moUI_Button1._Text = "下一个";
        }

        private void moUI_Button1_Click(object sender, EventArgs e)
        {
            if (QuestionControler.CurrentQuestionMod == QuestionControler.QuestionMod.大海捞针)
            {
                if (QuestionControler.Next_Mod_0())
                {
                    MakePanelTextCover_IsShow(moUI_Panel1, false);
                    MakePanelSwicth_IsSelect(moUI_Panel1, false, true, false);
                    var temp = QuestionControler.GetMod_0Question();
                    TextCover_1_Name._Text = temp.Name;
                    TextCover_1_Gender._Text = temp.Gender;
                    TextCover_1_Class._Text = temp.Class;
                    TextCover_1_Major._Text = temp.Major;
                    TextCover_1_PoliticalStatus._Text = temp.PoliticalStatus;
                    TextCover_1_Nation._Text = temp.Nation;
                    TextCover_1_Post._Text = temp.Post;
                    TextCover_1_Address._Text = temp.Address;
                    TextCover_1_Dorm._Text = temp.Dorm;
                    TextCover_1_DormMember._Text = temp.DormMember;
                    TextCover_1_Economic._Text = temp.Economic;
                    TextCover_1_BonusAndPenalty._Text = temp.BonusAndPenalty;
                    TextCover_1_Study._Text = temp.Study;
                    TextCover_1_Habby._Text = temp.Habby;
                    TextCover_1_Job._Text = temp.Job;
                    TextCover_1_Number._Text = temp.Number;
                    PictureBox1._Image = GetStudentPhoto(temp.PhotoPath);
                    if (QuestionControler.QuestionMod_0 >= 2)
                        moUI_Button1._Text = "下一个比赛项目";
                }
                else
                {
                    moUI_Button1._Text = "下一个";
                    QuestionControler.QuestionMod_1 = -1;
                    LoadQuestion2();
                    moUI_Panel1.Visible = false;
                    moUI_Panel2.Visible = true;
                }
            }
            else
            {
                if (QuestionControler.QuestionMod_1 < 3)
                {
                    moUI_Panel1.Visible = false;
                    LoadQuestion2();
                    moUI_Panel2.Visible = true;
                }
                else
                {
                    moUI_Panel1.Visible = false;
                    moUI_Panel2.Visible = false;
                    LoadQuestion3();
                    moUI_Panel3.Visible = true;
                }
            }
        }

        #region Switch
        private void moUI_Switch2_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch2.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch1_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch1.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch3_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch3.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch4_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch4.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch5_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch5.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch6_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch6.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch7_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch7.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch14_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch14.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch8_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch8.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch9_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch9.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch10_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch10.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch11_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch11.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch12_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch12.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch13_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch13.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch16_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch16.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        private void moUI_Switch17_Click(object sender, EventArgs e)
        {
            int i = (moUI_Switch17.IsSwitched ? CurrentSocre++ : CurrentSocre--);
        }

        #endregion

        #endregion

        #region 鱼目混珠

        private int CurrentIsTeacherStudentNumber;

        private void LoadQuestion2()
        {
            moUI_Label1.IsSelected = false;
            moUI_Label2.IsSelected = true;
            moUI_Label3.IsSelected = false;
            QuestionControler.QuestionMod_1++;
            var t = QuestionControler.Get_1Question();
            CurrentIsTeacherStudentNumber = QuestionControler.GetCurrentIsTeacherStudentNumber();
            PictureBox2_0._Image = GetStudentPhoto(t[0]);
            PictureBox2_1._Image = GetStudentPhoto(t[1]);
            PictureBox2_2._Image = GetStudentPhoto(t[2]);
            PictureBox2_3._Image = GetStudentPhoto(t[3]);
            PictureBox2_4._Image = GetStudentPhoto(t[4]);
            PictureBox2_5._Image = GetStudentPhoto(t[5]);
        }

        private void LoadPanel2()
        {
            moUI_Panel2.Size = new Size(784, 626);
        }

        private void CheckQuestion2(int ans)
        {
            if (ans == CurrentIsTeacherStudentNumber)
            {
                moUI_Panel2.Visible = false;
                var temp = QuestionControler.GetCurrentStudentInfoStructMod1();
                TextCover_1_Name._Text = temp.Name;
                TextCover_1_Gender._Text = temp.Gender;
                TextCover_1_Class._Text = temp.Class;
                TextCover_1_Major._Text = temp.Major;
                TextCover_1_PoliticalStatus._Text = temp.PoliticalStatus;
                TextCover_1_Nation._Text = temp.Nation;
                TextCover_1_Post._Text = temp.Post;
                TextCover_1_Address._Text = temp.Address;
                TextCover_1_Dorm._Text = temp.Dorm;
                TextCover_1_DormMember._Text = temp.DormMember;
                TextCover_1_Economic._Text = temp.Economic;
                TextCover_1_BonusAndPenalty._Text = temp.BonusAndPenalty;
                TextCover_1_Study._Text = temp.Study;
                TextCover_1_Habby._Text = temp.Habby;
                TextCover_1_Number._Text = temp.Number;
                TextCover_1_Job._Text = temp.Job;
                PictureBox1._Image = GetStudentPhoto(temp.PhotoPath);
                MakePanelTextCover_IsShow(moUI_Panel1, false);
                MakePanelSwicth_IsSelect(moUI_Panel1, false, true, false);
                moUI_Panel1.Visible = true;
            }
            else
            {
                if (QuestionControler.QuestionMod_1 < 3)
                    LoadQuestion2();
                else
                {
                    moUI_Panel1.Visible = false;
                    moUI_Panel2.Visible = false;
                    LoadQuestion3();
                    moUI_Panel3.Visible = true;
                }
            }
        }

        #region PictureBoxClick

        private void PictureBox2_0_Click(object sender, EventArgs e)
        {
            CheckQuestion2(0);
        }

        private void PictureBox2_1_Click(object sender, EventArgs e)
        {
            CheckQuestion2(1);
        }

        private void PictureBox2_2_Click(object sender, EventArgs e)
        {
            CheckQuestion2(2);
        }

        private void PictureBox2_3_Click(object sender, EventArgs e)
        {
            CheckQuestion2(3);
        }

        private void PictureBox2_4_Click(object sender, EventArgs e)
        {
            CheckQuestion2(4);
        }

        private void PictureBox2_5_Click(object sender, EventArgs e)
        {
            CheckQuestion2(5);
        }

        #endregion

        #endregion

        #region 描述定位

        private int Sub3Socre = 10;

        private void LoadPanel3()
        {
            moUI_Panel3.Size = new Size(740, 540 + 96);
        }

        private string Question3PhotoPath;

        private void LoadQuestion3()
        {

            moUI_Label1.IsSelected = false;
            moUI_Label3.IsSelected = true;
            moUI_Label2.IsSelected = false;
            moUI_Switch15.IsSwitched = false;
            Sub3Socre = 10;
            MakePanelTextCover_IsShow(moUI_Panel3, false);
            var temp = QuestionControler.GetMod_2Question();
            TextCover_2_Name._Text = temp.Name;
            TextCover_2_Gender._Text = temp.Gender;
            TextCover_2_Class._Text = temp.Class;
            TextCover_2_Major._Text = temp.Major;
            TextCover_2_PoliticalStatus._Text = temp.PoliticalStatus;
            TextCover_2_Nation._Text = temp.Nation;
            TextCover_2_Post._Text = temp.Post;
            TextCover_2_Address._Text = temp.Address;
            TextCover_2_Dorm._Text = temp.Dorm;
            TextCover_2_DormMember._Text = temp.DormMember;
            TextCover_2_Economic._Text = temp.Economic;
            TextCover_2_BonusAndPenalty._Text = temp.BonusAndPenalty;
            TextCover_2_Study._Text = temp.Study;
            TextCover_2_Habby._Text = temp.Habby;
            TextCover_2_Job._Text = temp.Job;
            TextCover_2_Number._Text = temp.Number;
            Question3PhotoPath = temp.PhotoPath;
            moUI_Button3.Enabled = false;
            moUI_PictureBox8._Image = null;
            moUI_Button3._Text = "下一个";
            TextCover_2_Gender._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Class._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Major._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Dorm._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_BonusAndPenalty._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Economic._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Post._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Study._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Habby._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Job._IsShow = QuestionControler.GetMod_2IsShow();
            TextCover_2_Number._IsShow = QuestionControler.GetMod_2IsShow();
        }

        private void moUI_Button3_Click(object sender, EventArgs e)
        {
            if (QuestionControler.QuestionMod_2 < 2)
            {
                QuestionControler.QuestionMod_2++;
                LoadQuestion3();
                if (QuestionControler.QuestionMod_2 == 2)
                    moUI_Button3._Text = "结束答题";
            }
            else
            {
                CurrentSelectTeacherInfo.Socre = CurrentSocre;
                CurrentSelectTeacherInfo.Time = TotalTime;
                Timer_TotalTime.Enabled = false;
                LoadSocreView(true, moUI_Panel3);
            }
        }

        private void moUI_Button2_Click(object sender, EventArgs e)
        {
            moUI_PictureBox8._Image = GetStudentPhoto(Question3PhotoPath);
            moUI_Button3.Enabled = true;
        }

        private void TextCover_2_Name_Click(object sender, EventArgs e)
        {
            moUI_PictureBox8._Image = GetStudentPhoto(Question3PhotoPath);
            moUI_Button3.Enabled = true;
        }

        private void moUI_Switch15_Click(object sender, EventArgs e)
        {
            if (moUI_Switch15.IsSwitched)
                CurrentSocre += Sub3Socre;
            else
                CurrentSocre -= Sub3Socre;
        }

        #region sub3clickscore

        private void TextCover_2_Gender_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Gender._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Class_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Class._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Major_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Major._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Nation_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Nation._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Dorm_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Dorm._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_PoliticalStatus_Click(object sender, EventArgs e)
        {
            if (TextCover_2_PoliticalStatus._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Post_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Post._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Address_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Address._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_DormMember_Click(object sender, EventArgs e)
        {
            if (TextCover_2_DormMember._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_BonusAndPenalty_Click(object sender, EventArgs e)
        {
            if (TextCover_2_BonusAndPenalty._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Study_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Study._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Habby_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Habby._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Job_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Job._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Number_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Number._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        private void TextCover_2_Economic_Click(object sender, EventArgs e)
        {
            if (TextCover_2_Economic._IsShow)
                Sub3Socre--;
            else
                Sub3Socre++;
        }

        #endregion

        #endregion

        #region 成绩模块

        private void LoadSocreView(bool AutoShow = true, Control ctl = null)
        {
            ScoreListView.Items.Clear();
            //ScoreListView.Size = new Size(840, 535);
            //ScoreListView.Location = new Point(129, 93);
            ScoreListView.BeginUpdate();
            Thread thread = new Thread(new ThreadStart(ThreadUpdateScore));
            thread.Start();
            foreach (var t in TeacherInfoList)
            {
                ListViewItem lvi = new ListViewItem(t.Major);
                lvi.SubItems.Add(t.Name);
                if (t.Socre <= 0)
                    lvi.SubItems.Add("——");
                else
                    lvi.SubItems.Add(t.Socre.ToString() + "(" + (t.Socre / (128f + 20f) * 100).ToString("0.00") + "%)");
                if (t.Time <= 0)
                    lvi.SubItems.Add("——");
                else
                {
                    int ttt = 480 - t.Time;
                    int min = ttt / 60;
                    int sec = ttt % 60;
                    lvi.SubItems.Add(min + "分" + (sec < 10 ? "0" : "") + sec + "秒");
                }
                ScoreListView.Items.Add(lvi);
            }
            int i = 20;
            while (i-- > 0)
            {
                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                ScoreListView.Items.Add(lvi);
            }

            if (ctl != null)
                ctl.Visible = false;
            ScoreListView.EndUpdate();
            ScoreListView.Invalidate();
            if (AutoShow)
                ScoreListView.Visible = true;
        }

        private void moUI_Label_Score_Click(object sender, EventArgs e)
        {
            LoadSocreView(false);
            ScoreListView.Visible = !ScoreListView.Visible;
        }

        private void ThreadUpdateScore()
        {
            try
            {
                new SQLiteHelper().InsertOrUpdateTeacherScore(TeacherInfoList);
            }
            catch
            {

            }
        }

        #endregion

    }
}
