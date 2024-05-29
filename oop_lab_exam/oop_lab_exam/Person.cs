using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab_exam
{
    internal class Person : DBAccess

    {
        DBAccess objDBAccess = new DBAccess();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

    }

    class Student : Person
    {
        public int Id { get; set; }
        public int MathCours { get; set; }
        public int OopCourse { get; set; }
        public int StatisticsCourse { get; set; }
        public int JavaCourse { get; set; }


    }
   
}
