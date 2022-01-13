using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolADO.Net.Models
{
    public class Teacher
    {
        public string FirstName;
        public string LastName;
        public int Wage;
        public string BirthDate;

        public Teacher ( string _firstname ,string _lastname , int _wage , string _birthdate ) {
        this.FirstName = _firstname;    
        this.LastName = _lastname;
        this.Wage = _wage;
        this.BirthDate = _birthdate;
        }

        public Teacher () { }
    }
}