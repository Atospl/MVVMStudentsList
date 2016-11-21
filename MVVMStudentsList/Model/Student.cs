using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMStudentsList.Model
{
    public class Student
    {
        [Key]
        public int IDStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IndexNo { get; set; }
        public string BirthPlace { get; set; }
        public int? IDGroup { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Stamp { get; set; }
        [ForeignKey("IDGroup")]
        public virtual Group Group { get; set; }
        public Student(string firstName, string lastName, string birthPlace, int indexNr, int groupID)
        {

        }

        private Student()
        {

        }
    }
}