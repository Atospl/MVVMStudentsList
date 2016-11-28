using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMStudentsList.Model
{
    public class Student
    {
        [Key]
        public int IDStudent { get; set; }

        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(64)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(10)]
        [Required]
        [Index(IsUnique = true)]
        public string IndexNo { get; set; }

        [MaxLength(64)]
        public string BirthPlace { get; set; }

        [Required]
        public int? IDGroup { get; set; }
        public DateTime BirthDate { get; set; }

        [Timestamp]
        public byte[] Stamp { get; set; }

        [ForeignKey("IDGroup")]
        public virtual Group Group { get; set; }
        public Student(string firstName, string lastName, string birthPlace, int indexNr, int groupID)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthPlace = birthPlace;
            IndexNo = indexNr.ToString();
            IDGroup = groupID;
            BirthDate = new DateTime();
        }

        private Student()
        {

        }
    }
}