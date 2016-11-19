using System;

namespace MVVMStudentsList.Model
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Index { get; set; }
        public string BirthPlace { get; set; }
        public virtual Group Group { get; set; }
        public DateTime Birthday { get; set; }
    }
}