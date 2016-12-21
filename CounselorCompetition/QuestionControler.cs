using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CounselorCompetition.Struct;
using System.Drawing;
using System.IO;
using CounselorCompetition.ImageAlgorithm;
using CounselorCompetition.Database;

namespace CounselorCompetition
{
    public class QuestionControler
    {

        private static TeacherInfoStruct CurrentTeacherInfo;

        public enum QuestionMod
        {
            大海捞针 = 0,
            鱼目混珠 = 1,
            描述定位 = 2
        }

        public static QuestionMod CurrentQuestionMod;

        public static int QuestionMod_0;
        public static int QuestionMod_1;
        public static int QuestionMod_2;

        public static void Reset(TeacherInfoStruct currentTeacherInfo)
        {
            CurrentTeacherInfo = currentTeacherInfo;
            CurrentQuestionMod = 0;
            QuestionMod_0 = 0;
            QuestionMod_1 = 0;
            QuestionMod_2 = 0;
            LoadMod_0StudentInfoStructs();
            LoadMod_1StudentInfoStructs();
            LoadMod_2StudentInfoStructs();
        }

        /// <summary>
        /// 生成指定范围内的不重复随机数
        /// </summary>
        /// <param name="Number">随机数个数</param>
        /// <param name="minNum">随机数下限</param>
        /// <param name="maxNum">随机数上限</param>
        /// <returns></returns>
        public static int[] GetRandomArray(int Number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[Number];
            Random r = new Random();
            for (j = 0; j < Number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }

        #region 大海捞针

        private static void LoadMod_0StudentInfoStructs()
        {
            //生成三个随机数，填充Mod_0StudentInfoStructs;
            var rad_int = GetRandomArray(3, 0, CurrentTeacherInfo.StudentList.Count - 1);
            Mod_0StudentInfoStructs[0] = CurrentTeacherInfo.StudentList[rad_int[0]];
            Mod_0StudentInfoStructs[1] = CurrentTeacherInfo.StudentList[rad_int[1]];
            Mod_0StudentInfoStructs[2] = CurrentTeacherInfo.StudentList[rad_int[2]];
        }

        private static StudentInfoStruct[] Mod_0StudentInfoStructs = new StudentInfoStruct[3];

        public static StudentInfoStruct GetMod_0Question()
        {
            return Mod_0StudentInfoStructs[QuestionMod_0];
        }

        public static bool Next_Mod_0()
        {
            if (CurrentQuestionMod == QuestionMod.大海捞针)
            {
                if (QuestionMod_0 >= 2)
                {
                    CurrentQuestionMod = QuestionMod.鱼目混珠;
                    return false;
                }
                QuestionMod_0++;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 鱼目混珠

        private static void LoadMod_1StudentInfoStructs()
        {
            //生成三个随机数，填充Mod_2StudentInfoStructs;
            var rad_int = GetRandomArray(7, 0, CurrentTeacherInfo.StudentList.Count - 1);
            Mod_1StudentInfoStructs[0] = CurrentTeacherInfo.StudentList[rad_int[3]];
            Mod_1StudentInfoStructs[1] = CurrentTeacherInfo.StudentList[rad_int[4]];
            Mod_1StudentInfoStructs[2] = CurrentTeacherInfo.StudentList[rad_int[5]];
            Mod_1StudentInfoStructs[3] = CurrentTeacherInfo.StudentList[rad_int[6]];
        }

        private static int CurrentIsTeacherStudentNumber;

        private static StudentInfoStruct[] Mod_1StudentInfoStructs = new StudentInfoStruct[4];

        public static int GetCurrentIsTeacherStudentNumber()
        {
            return CurrentIsTeacherStudentNumber;
        }

        public static StudentInfoStruct GetCurrentStudentInfoStructMod1()
        {
            return Mod_1StudentInfoStructs[QuestionMod_1];
        }

        public static string[] Get_1Question()
        {
            var t = new SQLiteHelper().GetOther5StudentsPhotoPath(CurrentTeacherInfo.IncludeClass);
            string[] res = new string[6];

            Random ran = new Random();
            CurrentIsTeacherStudentNumber = ran.Next(0, 5);

            res[CurrentIsTeacherStudentNumber] = Mod_1StudentInfoStructs[QuestionMod_1].PhotoPath;
            int j = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == CurrentIsTeacherStudentNumber)
                    continue;
                res[i] = t[j];
                j++;
            }
            return res;
        }
        #endregion

        #region 描述定位

        private static void LoadMod_2StudentInfoStructs()
        {
            //生成三个随机数，填充Mod_1StudentInfoStructs;
            var rad_int = GetRandomArray(10, 0, CurrentTeacherInfo.StudentList.Count - 1);
            Mod_2StudentInfoStructs[0] = CurrentTeacherInfo.StudentList[rad_int[7]];
            Mod_2StudentInfoStructs[1] = CurrentTeacherInfo.StudentList[rad_int[8]];
            Mod_2StudentInfoStructs[2] = CurrentTeacherInfo.StudentList[rad_int[9]];
        }

        private static StudentInfoStruct[] Mod_2StudentInfoStructs = new StudentInfoStruct[3];
        
        public static StudentInfoStruct GetMod_2Question()
        {
            Mod_2IsShow_Pos_i = 0;
            Mod_2IsShow = GetRandomArray(3, 0, 8);
            return Mod_2StudentInfoStructs[QuestionMod_2];
        }


        private static int Mod_2IsShow_Pos_i;

        private static int[] Mod_2IsShow = new int[3];

        public static bool GetMod_2IsShow()
        {
            if (Mod_2IsShow_Pos_i > 8)
                return false;
            if (Mod_2IsShow_Pos_i == Mod_2IsShow[0] || Mod_2IsShow_Pos_i == Mod_2IsShow[1] || Mod_2IsShow_Pos_i == Mod_2IsShow[2])
            {
                Mod_2IsShow_Pos_i++;
                return true;
            }
            else
                Mod_2IsShow_Pos_i++;
            return false;
        }

        #endregion

    }
}
