using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CounselorCompetition.Struct
{
    public class StudentInfoStruct
    {
        /*
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
	ID integer AUTO_INCREMENT,
             */

        public StudentInfoStruct()
        {

        }

        public string Name;
        public string Gender;
        public string Class;
        public string Major;
        public string PoliticalStatus;
        public string Nation;
        public string Post;
        public string Address;
        public string Dorm;
        public string DormMember;
        public string Economic;
        public string BonusAndPenalty;
        public string Study;
        public string Habby;
        public string PhotoPath;
        public string ID;
        public string Number;
        public string Job;
        public Bitmap Photo;
    }
}
