using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using CounselorCompetition.Struct;
using System.Data;
using System.Drawing;
using System.Threading;

namespace CounselorCompetition.Database
{
    public class SQLiteHelper
    {

        private string DatabasePath;
        private string DatabaseRootPath;
        private string ImgRootPath;
        private SQLiteConnection CONN;

        public void Close()
        {
            try
            {
                CONN.Close();
            }
            catch { }
        }

        public SQLiteHelper()
        {

            DatabasePath = "Data Source =" + Environment.CurrentDirectory + "/Data/Data.db";
            DatabaseRootPath = Environment.CurrentDirectory + "/Data";
            ImgRootPath = DatabaseRootPath + "/IMG";
            if (!Directory.Exists(DatabaseRootPath))
            { 
                DirectoryInfo directoryInfo = new DirectoryInfo(DatabaseRootPath);
                directoryInfo.Create();
            }
            if (!Directory.Exists(ImgRootPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(ImgRootPath);
                directoryInfo.Create();
            }

            CONN = new SQLiteConnection(DatabasePath);  
            CONN.Open();
            
            string SQL_CreateStudentTable = @"CREATE TABLE IF NOT EXISTS StudentInfo(
	Name varchar(20), 
	Gender varchar(10),
	Class varchar(10),
	Major varchar(20),
	PoliticalStatus varchar(10),
	Nation varchar(10),
	Post varchar(10),
	Address varchar(100),
	Dorm varchar(10),
	DormMember varchar(100),
	Economic varchar(100),
	BonusAndPenalty varchar(100),
	Study varchar(100),
	Habby varchar(100),
	PhotoPath varchar(100),
	Number varchar(20),
	Job varchar(100),
	ID INTEGER PRIMARY KEY
	);";

            string SQL_CreateTeacherTable = @"CREATE TABLE IF NOT EXISTS TeacherInfo(
	Name varchar(20), 
	Major varchar(20),
	IncludeClass varchar(8000),
	ID integer AUTO_INCREMENT,
	primary key (id)
	);";

            string SQL_CreateConfigTable = @"CREATE TABLE IF NOT EXISTS ConfigTable(
    ConfigKey varchar(20),
    ConfigValue varchar(20),
	ID integer AUTO_INCREMENT,
	primary key (id)
    );";


            string SQL_CreateScoreTable = @"CREATE TABLE IF NOT EXISTS ScoreTable(
    Major varchar(20),
    Name varchar(20),
    Score varchar(20),
    Time varchar(20),
	ID integer AUTO_INCREMENT,
	primary key (id)
    );";

            SQLiteCommand cmd = new SQLiteCommand(SQL_CreateStudentTable, CONN);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand(SQL_CreateTeacherTable, CONN);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand(SQL_CreateConfigTable, CONN);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand(SQL_CreateScoreTable, CONN);
            cmd.ExecuteNonQuery();

            CONN.Close();
        }

        /// <summary>
        /// 插入一条新的学生信息
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="Gender">性别</param>
        /// <param name="Class">班级</param>
        /// <param name="Major">专业</param>
        /// <param name="PoliticalStatus">政治面貌</param>
        /// <param name="Nation">民族</param>
        /// <param name="Post">担任职务</param>
        /// <param name="Address">家庭地址</param>
        /// <param name="Dorm">宿舍</param>
        /// <param name="DormMember">宿舍成员</param>
        /// <param name="Economic">家庭经济情况</param>
        /// <param name="BonusAndPenalty">奖惩情况</param>
        /// <param name="Study">学习状况</param>
        /// <param name="Habby">兴趣爱好</param>
        /// <returns>操作是否成功</returns>
        public bool InsertNewStudent(string Name, string Gender, string Class, string Major, string PoliticalStatus, 
                                            string Nation, string Post, string Address, string Dorm, string DormMember,
                                            string Economic, string BonusAndPenalty, string Study, string Habby, string Job, string Number)
        {
            try
            {
                CONN.Open();

                string SQL_Insert = string.Format(
                    @"INSERT INTO StudentInfo ([Name], [Gender], [Class], [Major], [PoliticalStatus], 
	[Nation], [Post], [Address], [Dorm], [DormMember],
	[Economic], [BonusAndPenalty], [Study], [Habby], [Job], [Number])
VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    Name,
                    Gender,
                    Class,
                    Major,
                    PoliticalStatus,
                    Nation,
                    Post,
                    Address,
                    Dorm,
                    DormMember,
                    Economic,
                    BonusAndPenalty,
                    Study,
                    Habby,
                    Job,
                    Number
                    );

                SQLiteCommand cmd = new SQLiteCommand(SQL_Insert, CONN);
                cmd.ExecuteNonQuery();

                CONN.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 插入一条新的学生信息
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="Gender">性别</param>
        /// <param name="Class">班级</param>
        /// <param name="Major">专业</param>
        /// <param name="PoliticalStatus">政治面貌</param>
        /// <param name="Nation">民族</param>
        /// <param name="Post">担任职务</param>
        /// <param name="Address">家庭地址</param>
        /// <param name="Dorm">宿舍</param>
        /// <param name="DormMember">宿舍成员</param>
        /// <param name="Economic">家庭经济情况</param>
        /// <param name="BonusAndPenalty">奖惩情况</param>
        /// <param name="Study">学习状况</param>
        /// <param name="Habby">兴趣爱好</param>
        /// <param name="PhotoPath">照片地址</param>
        /// <returns>操作是否成功</returns>
        public bool InsertNewStudent(string Name, string Gender, string Class, string Major, string PoliticalStatus,
                                            string Nation, string Post, string Address, string Dorm, string DormMember,
                                            string Economic, string BonusAndPenalty, string Study, string Habby, string PhotoPath, string Job, string Number)
        {
            try
            {
                CONN.Open();

                string SQL_Insert = string.Format(
                    @"INSERT INTO StudentInfo ([Name], [Gender], [Class], [Major], [PoliticalStatus], 
	[Nation], [Post], [Address], [Dorm], [DormMember],
	[Economic], [BonusAndPenalty], [Study], [Habby], [PhotoPath], [Job], [Number])
VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                    Name,
                    Gender,
                    Class,
                    Major,
                    PoliticalStatus,
                    Nation,
                    Post,
                    Address,
                    Dorm,
                    DormMember,
                    Economic,
                    BonusAndPenalty,
                    Study,
                    Habby,
                    PhotoPath,
                    Job,
                    Number
                    );

                SQLiteCommand cmd = new SQLiteCommand(SQL_Insert, CONN);
                cmd.ExecuteNonQuery();

                CONN.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 插入新的学生信息
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>操作是否成功</returns>
        public bool InsertNewStudent(DataTable dt,int current_input_success_total, ref int input_success_total, Form_Config frm = null)
        {
            try
            {
                input_success_total = 0;
                if (dt.Columns.Count != 15)
                    return false;
                CONN.Open();
                var trans = CONN.BeginTransaction();
                for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
                {
                    input_success_total = curRow;
                    string ID = dt.Rows[curRow][0].ToString(),
                           Name = dt.Rows[curRow][1].ToString();
                    if (string.IsNullOrEmpty(Name))
                        break;
                    string Number = dt.Rows[curRow][1].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Gender = dt.Rows[curRow][2].ToString().Trim().Replace("\n","").Replace("\r", ""),
                           Class = dt.Rows[curRow][3].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Major = dt.Rows[curRow][4].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           PoliticalStatus = dt.Rows[curRow][5].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Nation = dt.Rows[curRow][6].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Post = dt.Rows[curRow][7].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Address = dt.Rows[curRow][8].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Dorm = dt.Rows[curRow][9].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           DormMember = dt.Rows[curRow][10].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Economic = dt.Rows[curRow][11].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           BonusAndPenalty = dt.Rows[curRow][12].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Study = dt.Rows[curRow][13].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Habby = dt.Rows[curRow][14].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           Job = dt.Rows[curRow][15].ToString().Trim().Replace("\n", "").Replace("\r", ""),
                           PhotoPath = "Data/IMG/" + Major + "/" + Class + "/" + Name;
                    if (frm != null)
                    {
                        frm.Invoke((EventHandler)delegate
                        {
                            frm.Text = "[" + (current_input_success_total + curRow) + "]" + Major + " - " + Class + " - " + Name;
                        });
                    }
                    //Thread.Sleep(3);
                    string SQL_Insert = string.Format(
    @"INSERT INTO StudentInfo ([Name], [Gender], [Class], [Major], [PoliticalStatus], 
	[Nation], [Post], [Address], [Dorm], [DormMember],
	[Economic], [BonusAndPenalty], [Study], [Habby], [PhotoPath], [Job], [Number])
VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
    Name,
    Gender,
    Class,
    Major,
    PoliticalStatus,
    Nation,
    Post,
    Address,
    Dorm,
    DormMember,
    Economic,
    BonusAndPenalty,
    Study,
    Habby,
    PhotoPath,
    Job,
    Number
    );
                    SQLiteCommand cmd = new SQLiteCommand(SQL_Insert, CONN);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                trans.Commit();
                CONN.Close();
                return true;
            }
            catch
            {
                input_success_total = 0;
                return false;
            }
        }

        public List<StudentInfoStruct> GetStudents(string IncludeClass)
        {
            try
            {
                List<StudentInfoStruct> res = new List<StudentInfoStruct>();
                CONN.Open();
                var StringArray_Class = IncludeClass.Split(',');
                if (StringArray_Class.Length == 0)
                    return null;
                foreach (var ClassString in StringArray_Class)
                {
                    string ClassName;
                    string Major;
                    var temp = ClassString.Split('.');
                    Major = temp[0].Trim();
                    ClassName = temp[1].Trim();

                    string SQL_SELECT_STUDENTS = string.Format(@"SELECT * FROM StudentInfo WHERE [Major] = '{0}' AND [Class] = '{1}'", Major, ClassName);
                    SQLiteCommand cmd = new SQLiteCommand(SQL_SELECT_STUDENTS, CONN);
                    SQLiteDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        StudentInfoStruct si = new StudentInfoStruct();
                        si.Name = sdr["Name"].ToString();
                        si.Gender = sdr["Gender"].ToString();
                        si.Class = sdr["Class"].ToString();
                        si.Major = sdr["Major"].ToString();
                        si.PoliticalStatus = sdr["PoliticalStatus"].ToString();
                        si.Nation = sdr["Nation"].ToString();
                        si.Post = sdr["Post"].ToString();
                        si.Address = sdr["Address"].ToString();
                        si.Dorm = sdr["Dorm"].ToString();
                        si.DormMember = sdr["DormMember"].ToString();
                        si.Economic = sdr["Economic"].ToString();
                        si.BonusAndPenalty = sdr["BonusAndPenalty"].ToString();
                        si.Study = sdr["Study"].ToString();
                        si.Habby = sdr["Habby"].ToString();
                        si.PhotoPath = sdr["PhotoPath"].ToString();
                        si.ID = sdr["ID"].ToString();
                        si.Job = sdr["Job"].ToString();
                        si.Number = sdr["Number"].ToString();
                        if (!File.Exists(si.PhotoPath))
                            continue;
                        res.Add(si);
                    }
                    sdr.Close();
                }
                CONN.Close();
                return res;
            }
            catch
            {
                return new List<StudentInfoStruct>();
            }
        }
        
        /// <summary>
        /// 根据学生ID获取学生信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public StudentInfoStruct GetStudentInfo(string ID)
        {
            try
            {
                StudentInfoStruct res = new StudentInfoStruct();
                CONN.Open();

                string SQL_SELECT = string.Format(@"SELECT * FROM StudentInfo WHERE [ID] = {0}", int.Parse(ID));
                var cmd = new SQLiteCommand(SQL_SELECT, CONN);
                var sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    res.Name = sdr["Name"].ToString();
                    res.Gender = sdr["Gender"].ToString();
                    res.Class = sdr["Class"].ToString();
                    res.Major = sdr["Major"].ToString();
                    res.PoliticalStatus = sdr["PoliticalStatus"].ToString();
                    res.Nation = sdr["Nation"].ToString();
                    res.Post = sdr["Post"].ToString();
                    res.Address = sdr["Address"].ToString();
                    res.Dorm = sdr["Dorm"].ToString();
                    res.DormMember = sdr["DormMember"].ToString();
                    res.Economic = sdr["Economic"].ToString();
                    res.BonusAndPenalty = sdr["BonusAndPenalty"].ToString();
                    res.Study = sdr["Study"].ToString();
                    res.Habby = sdr["Habby"].ToString();
                    res.PhotoPath = sdr["PhotoPath"].ToString();
                    res.ID = sdr["ID"].ToString();
                    res.Job = sdr["Job"].ToString();
                    res.Number = sdr["Number"].ToString();
                }
                sdr.Close();
                CONN.Close();
                return res;
            }
            catch
            {
                return new StudentInfoStruct();
            }
        }

        /// <summary>
        /// 获取教师信息表
        /// </summary>
        /// <returns></returns>
        public List<TeacherInfoStruct> GetTeacherInfoList()
        {
            string TeacherMajor = string.Empty;
            string IncludeClass = string.Empty;
            string TeacherName = string.Empty;
            try
            {
                List<TeacherInfoStruct> resList = new List<TeacherInfoStruct>();
                CONN.Open();
                string SQL_GetAllTeacherInfoList = @"SELECT * FROM TeacherInfo";
                SQLiteCommand cmd = new SQLiteCommand(SQL_GetAllTeacherInfoList, CONN);
                var sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    TeacherInfoStruct TeacherTemp = new TeacherInfoStruct();
                    //读取一个老师的信息
                    TeacherTemp.Name = sdr["Name"].ToString();
                    TeacherName = TeacherTemp.Name;
                    TeacherTemp.Major = sdr["Major"].ToString();
                    TeacherMajor = TeacherTemp.Major;
                    TeacherTemp.IncludeClass = sdr["IncludeClass"].ToString();
                    IncludeClass = TeacherTemp.IncludeClass;
                    //if (string.IsNullOrEmpty(TeacherTemp.IncludeClass))
                    //    continue;
                    #region
                    {
                        string Major = string.Empty;
                        string ClassName = string.Empty;
                        List<StudentInfoStruct> resStudentList = new List<StudentInfoStruct>();
                        var StringArray_Class = TeacherTemp.IncludeClass.Split(',');
                        if (StringArray_Class.Length == 0)
                            return null;
                        foreach (var ClassString in StringArray_Class)
                        {
                            var temp = ClassString.Split('.');
                            Major = temp[0].Trim();
                            ClassName = temp[1].Trim();
                            string SQL_SELECT_STUDENTS = string.Format(@"SELECT * FROM StudentInfo WHERE [Major] = '{0}' AND [Class] = '{1}'", Major, ClassName);
                            SQLiteCommand cmdStudent = new SQLiteCommand(SQL_SELECT_STUDENTS, CONN);
                            SQLiteDataReader sdrStudent = cmdStudent.ExecuteReader();//
                            while (sdrStudent.Read())
                            {
                                StudentInfoStruct si = new StudentInfoStruct();
                                si.Name = sdrStudent["Name"].ToString();
                                si.Gender = sdrStudent["Gender"].ToString();
                                si.Class = sdrStudent["Class"].ToString();
                                si.Major = sdrStudent["Major"].ToString();
                                si.PoliticalStatus = sdrStudent["PoliticalStatus"].ToString();
                                si.Nation = sdrStudent["Nation"].ToString();
                                si.Post = sdrStudent["Post"].ToString();
                                si.Address = sdrStudent["Address"].ToString();
                                si.Dorm = sdrStudent["Dorm"].ToString();
                                si.DormMember = sdrStudent["DormMember"].ToString();
                                si.Economic = sdrStudent["Economic"].ToString();
                                si.BonusAndPenalty = sdrStudent["BonusAndPenalty"].ToString();
                                si.Study = sdrStudent["Study"].ToString();
                                si.Habby = sdrStudent["Habby"].ToString();
                                si.PhotoPath = sdrStudent["PhotoPath"].ToString();
                                si.ID = sdrStudent["ID"].ToString();
                                si.Job = sdrStudent["Job"].ToString();
                                si.Number = sdrStudent["Number"].ToString();
                                if (File.Exists(si.PhotoPath) || File.Exists(si.PhotoPath + ".jpg") || File.Exists(si.PhotoPath + ".png") || File.Exists(si.PhotoPath + ".jpg.jpg"))
                                    resStudentList.Add(si);
                            }
                            sdrStudent.Close();
                        }
                        TeacherTemp.StudentList = resStudentList;
                        resList.Add(TeacherTemp);
                    }
                    #endregion
                }
                sdr.Close();
                CONN.Close();
                return resList;
            }
            catch (Exception e)
            {
                //throw e;
                throw new Exception(TeacherMajor + " - " + TeacherName + "\n\n" + IncludeClass + "\n\n信息格式错误");
            }
        }

        /// <summary>
        /// 插入新老师
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="frm"></param>
        /// <returns></returns>
        public bool InsertNewTeacher(DataTable dt, Form_Config frm = null)
        {
            try
            {
                if (dt.Columns.Count != 3)
                    return false;
                CONN.Open();
                var trans = CONN.BeginTransaction();
                for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
                {
                    string Name = dt.Rows[curRow][0].ToString();
                    string Major = dt.Rows[curRow][1].ToString();
                    string IncludeClass = dt.Rows[curRow][2].ToString();
                    //if (Name == "杨中建")
                    //    ;
                    if (string.IsNullOrEmpty(Name))
                        break;
                    if (frm != null)
                    {
                        frm.Invoke((EventHandler)delegate
                        {
                            frm.Text = "正在导入第 " + curRow + " 项中";
                        });
                    }
                    string SQL_Insert = string.Format(
    @"INSERT INTO TeacherInfo ([Name], [Major], [IncludeClass])
VALUES('{0}', '{1}', '{2}')",
    Name,
    Major,
    IncludeClass
    );
                    SQLiteCommand cmd = new SQLiteCommand(SQL_Insert, CONN);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                trans.Commit();
                CONN.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取其他五位老师的学生照片路径
        /// </summary>
        /// <param name="IncludeClass"></param>
        /// <returns></returns>
        public string[] GetOther5StudentsPhotoPath(string IncludeClass)
        {
            try
            {
                string[] res = new string[5];
                string str_SQLGETSTUDENT;
                str_SQLGETSTUDENT = "SELECT * FROM StudentInfo WHERE ";
                var StringArray_Class = IncludeClass.Split(',');
                string ClassName;
                string Major;
                bool first = true;
                foreach (var ClassString in StringArray_Class)
                {
                    if (!first)
                        str_SQLGETSTUDENT += " AND";
                    first = false;
                    var temp = ClassString.Split('.');
                    Major = temp[0].Trim();
                    ClassName = temp[1].Trim();
                    string t = string.Format(" (Class <> '{0}' AND Major <> '{1}')", ClassName, Major);
                    str_SQLGETSTUDENT += t;
                }
                str_SQLGETSTUDENT += " ORDER BY RANDOM() LIMIT 0, 5";

                CONN.Open();
                var cmd = new SQLiteCommand(str_SQLGETSTUDENT, CONN);
                var sdr = cmd.ExecuteReader();
                int i = 0;
                while (sdr.Read())
                {
                    res[i] = sdr["PhotoPath"].ToString();
                    i++;
                }
                sdr.Close();
                CONN.Close();
                return res;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除不存在照片的学生条目
        /// </summary>
        /// <returns></returns>
        public List<string> DeleteStudentsWithoutNoImg(Form_Config frm = null, bool DeleteConfirm = false)
        {
            try
            {
                CONN.Open();
                var trans = CONN.BeginTransaction();
                List<string> res = new List<string>();
                string SQL_SELECT_STUDENTS = @"SELECT * FROM StudentInfo";
                SQLiteCommand cmd = new SQLiteCommand(SQL_SELECT_STUDENTS, CONN);
                SQLiteDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string Name = sdr["Name"].ToString();
                    string Class = sdr["Class"].ToString();
                    string Major = sdr["Major"].ToString();
                    string PhotoPath = sdr["PhotoPath"].ToString();
                    string ID = sdr["ID"].ToString();
                    if (frm != null)
                        frm.Invoke((EventHandler)delegate
                        {
                            frm.Text = string.Format("正在检测 {0} - {1} - {2} ", Major, Class, Name);
                        });
                    if (File.Exists(PhotoPath) || File.Exists(PhotoPath + ".jpg") || File.Exists(PhotoPath + ".png") || File.Exists(PhotoPath + ".jpg.jpg"))    //存在
                    {
                        if (frm != null)
                            frm.Invoke((EventHandler)delegate
                            {
                                frm.Text += "- 照片存在";
                            });
                    }
                    else
                    {
                        if (frm != null)
                            frm.Invoke((EventHandler)delegate
                            {
                                frm.Text += "- 照片不存在 - 正在删除";
                            });
                        res.Add(string.Format("{0} - {1} - {2}", Major, Class, Name));
                        if (DeleteConfirm)
                        {
                            string SQL_DELETE = string.Format(@"DELETE FROM StudentInfo WHERE ID = '{0}'", ID);
                            SQLiteCommand del_cmd = new SQLiteCommand(SQL_DELETE, CONN);
                            del_cmd.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
                sdr.Close();
                CONN.Close();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 重置学生表
        /// </summary>
        /// <returns></returns>
        public bool DeleteTableStudent()
        {
            try
            {
                string SQL_TRUNCATE_STUDENT = @"DELETE FROM StudentInfo";
                string SQL_TRUNCATE_TEACHER = @"DELETE FROM TeacherInfo";
                CONN.Open();

                SQLiteCommand cmd_student = new SQLiteCommand(SQL_TRUNCATE_STUDENT, CONN);
                cmd_student.ExecuteNonQuery();
                //SQLiteCommand cmd_teacher = new SQLiteCommand(SQL_TRUNCATE_TEACHER, CONN);
                //cmd_teacher.ExecuteNonQuery();
                
                CONN.Close();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 重置教师表
        /// </summary>
        /// <returns></returns>
        public bool DeleteTableTeacher()
        {
            try
            {
                string SQL_TRUNCATE_TEACHER = @"DELETE FROM TeacherInfo";
                CONN.Open();

                SQLiteCommand cmd_student = new SQLiteCommand(SQL_TRUNCATE_TEACHER, CONN);
                cmd_student.ExecuteNonQuery();

                CONN.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 获取全部教师信息的DataSet集合
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllTeacher()
        {
            try
            {
                var res = new DataSet();
                CONN.Open();

                string SQL_GETALLTEACHER = @"SELECT Name, Major, IncludeClass FROM TeacherInfo";
                SQLiteDataAdapter sda = new SQLiteDataAdapter(SQL_GETALLTEACHER, CONN);
                sda.Fill(res, "TeacherInfo");
                sda.Dispose();

                CONN.Close();
                return res;
            }   
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取全部学生信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllStudent()
        {
            try
            {
                DataSet res = new DataSet();
                CONN.Open();

                string str_SQL_GETALLSTUDENT = @"SELECT Name, Gender, Class, Major, PoliticalStatus, Nation, Post, Address, Dorm, DormMember, Economic, BonusAndPenalty, Study, Habby FROM StudentInfo";
                SQLiteDataAdapter sda = new SQLiteDataAdapter(str_SQL_GETALLSTUDENT, CONN);
                sda.Fill(res, "StudentInfo");
                sda.Dispose();

                CONN.Close();
                return res;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="TextColor"></param>
        public void SetConfig(Color TextColor)
        {
            try
            {
                CONN.Open();

                string str_SQL_INSERT_BASE = @"INSERT INTO ConfigTable(ConfigKey, ConfigValue) VALUES('{0}', '{1}')";
                string str_SQL_UPDATE_BASE = @"UPDATE ConfigTable SET ConfigValue = '{0}' WHERE ConfigKey = '{1}'";
                string str_SQL_CHECKEXISTS_BASE = @"SELECT * FROM ConfigTable WHERE ConfigKey = '{0}'";
                if (!new SQLiteCommand(string.Format(str_SQL_CHECKEXISTS_BASE, "TextColor"), CONN).ExecuteReader().HasRows)
                    new SQLiteCommand(string.Format(str_SQL_INSERT_BASE, "TextColor", TextColor.ToArgb().ToString()), CONN).ExecuteNonQuery();
                else
                    new SQLiteCommand(string.Format(str_SQL_UPDATE_BASE, TextColor.ToArgb(), "TextColor"), CONN).ExecuteNonQuery();


                CONN.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取设置_文字颜色
        /// </summary>
        /// <returns></returns>
        public Color GetConfig_TextColor()
        {
            try
            {
                Color res = Color.White;
                CONN.Open();

                string str_SQL_GETTEXTCOLOR = @"SELECT * FROM ConfigTable WHERE ConfigValue = 'TextColor'";
                var sdr = new SQLiteCommand(str_SQL_GETTEXTCOLOR, CONN).ExecuteReader();
                while (sdr.Read())
                {

                }

                CONN.Close();
                return res;
            }
            catch
            {
                return Color.White;
            }
        }

        /// <summary>
        /// 获取全部专业
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMajor()
        {
            try
            {
                List<string> res = new List<string>();
                CONN.Open();
                string SQL_GET_ALL_MAJOR = "SELECT DISTINCT Major From StudentInfo";
                SQLiteCommand cmd = new SQLiteCommand(SQL_GET_ALL_MAJOR, CONN);
                var sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    res.Add(sdr[0].ToString());
                }
                sdr.Close();
                CONN.Close();
                return res;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据专业获取全部班级
        /// </summary>
        /// <param name="Major"></param>
        /// <returns></returns>
        public List<string> GetAllClassByMajor(string Major)
        {
            try
            {
                List<string> res = new List<string>();
                CONN.Open();
                string SQL_GET_ALL_CLASS_BY_MAJOR = "SELECT DISTINCT Class From StudentInfo WHERE Major = '" + Major + "'";
                SQLiteCommand cmd = new SQLiteCommand(SQL_GET_ALL_CLASS_BY_MAJOR, CONN);
                var sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    res.Add(sdr[0].ToString());
                }
                sdr.Close();
                CONN.Close();
                return res;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 更新教师班级信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Major"></param>
        /// <param name="IncludeClass"></param>
        /// <returns></returns>
        public bool SetTeacherIncludeClass(string Name, string Major, string IncludeClass)
        {
            try
            {
                CONN.Open();
                string SQL_UPDATE = string.Format("UPDATE TeacherInfo SET IncludeClass = '{0}' WHERE Name = '{1}' AND Major = '{2}'",
                    IncludeClass, Name, Major);
                SQLiteCommand cmd = new SQLiteCommand(SQL_UPDATE, CONN);
                cmd.ExecuteNonQuery();
                CONN.Clone();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据系别姓名查询教师包含班级
        /// </summary>
        /// <param name="Major"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string GetTeacherIncludeClass(string Major, string Name)
        {
            try
            {
                string res = string.Empty;
                CONN.Open();
                string SQL_GET = string.Format("SELECT IncludeClass FROM TeacherInfo WHERE Major = '{0}' AND Name = '{1}'", Major, Name);
                var sdr = new SQLiteCommand(SQL_GET, CONN).ExecuteReader();
                while (sdr.Read())
                {
                    res = sdr[0].ToString();
                }
                sdr.Close();
                CONN.Close();
                return res;
            }
            catch (Exception e )
            {
                throw e;
            }
        }

        /// <summary>
        /// 插入或更新教师比赛成绩
        /// </summary>
        /// <param name="Major"></param>
        /// <param name="Name"></param>
        /// <param name="Score"></param>
        /// <param name="Time"></param>
        public void InsertOrUpdateTeacherScore(List<TeacherInfoStruct> list)
        {
            try
            {
                CONN.Open();
                var trans = CONN.BeginTransaction();
                foreach (var t in list)
                {
                    string Major = t.Major, Name = t.Name;
                    string Score = t.Socre.ToString(), Time = t.Time.ToString();

                    if (t.Socre <= 0)
                        Score = "——";
                    else
                        Score = (t.Socre.ToString() + "(" + (t.Socre / 128f * 100).ToString("0.00") + "%)");
                    if (t.Time <= 0)
                        Time = ("——");
                    else
                    {
                        int ttt = 480 - t.Time;
                        int min = ttt / 60;
                        int sec = ttt % 60;
                        Time = (min + "分" + (sec < 10 ? "0" : "") + sec + "秒");
                    }

                    string str_SQL_INSERT_BASE = @"INSERT INTO ScoreTable(Major, Name, Score, Time) VALUES('{0}', '{1}', '{2}', '{3}')";
                    string str_SQL_UPDATE_BASE = @"UPDATE ScoreTable SET Score = '{0}', Time = '{1}' WHERE Major = '{2}' AND Name = '{3}'";
                    string str_SQL_CHECKEXISTS_BASE = @"SELECT * FROM ScoreTable WHERE Major = '{0}' AND Name = '{1}'";
                    if (new SQLiteCommand(string.Format(str_SQL_CHECKEXISTS_BASE, Major, Name), CONN).ExecuteReader().HasRows)
                        new SQLiteCommand(string.Format(str_SQL_UPDATE_BASE, Score, Time, Major, Name), CONN).ExecuteNonQuery();
                    else
                        new SQLiteCommand(string.Format(str_SQL_INSERT_BASE, Major, Name, Score, Time), CONN).ExecuteNonQuery();

                }
                trans.Commit();
                CONN.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取全部教师比赛成绩
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllTeacherScore()
        {
            try
            {
                DataSet res = new DataSet();
                CONN.Open();

                string str_SQL_GETALLSCORE = @"SELECT Major, Name, Score, Time FROM ScoreTable";
                SQLiteDataAdapter sda = new SQLiteDataAdapter(str_SQL_GETALLSCORE, CONN);
                sda.Fill(res, "ScoreTable");
                sda.Dispose();

                CONN.Close();
                return res;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 清空比赛成绩
        /// </summary>
        public void DeleteAllTeacherScore()
        {
            try
            {
                string SQL_TRUNCATE_STUDENT = @"DELETE FROM ScoreTable";
                CONN.Open();

                SQLiteCommand cmd_student = new SQLiteCommand(SQL_TRUNCATE_STUDENT, CONN);
                cmd_student.ExecuteNonQuery();

                CONN.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
