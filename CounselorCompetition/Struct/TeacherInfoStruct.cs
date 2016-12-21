using System;
using System.Collections.Generic;
using System.Text;

namespace CounselorCompetition.Struct
{
    public class TeacherInfoStruct
    {

        /*
         	Name varchar(20), 
	Major varchar(20),
	IncludeClass varchar(8000),
	ID integer AUTO_INCREMENT,
	primary key (id)
*/

        public string Name;
        public string Major;
        public string IncludeClass;
        public List<StudentInfoStruct> StudentList = new List<StudentInfoStruct>();

        public int Socre = 0;
        public int Time = 0;

    }
}
