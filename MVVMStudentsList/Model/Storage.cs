using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStudentsList.Model
{
    public class Storage
    { 
        public List<Student> GetStudents()
        {
            using (var db = new StorageContext())
            {
                //db.Groups.Load();
                //db.Students.Include(p => p.Group);
                return db.Students.Include(p => p.Group).ToList();
            }
        }

        public List<Group> GetGroups()
        {
            using (var db = new StorageContext())
                return db.Groups.ToList();
        }
        //void CreateStudent(string firstName, string lastName, string birthPlace, string indexNo, int groupId)
        //{
        //    using (var db = new StorageContext())
        //    {
        //        var group = db.Groups.Find(groupId);
        //        var student = new Student
        //        {
        //            FirstName = firstName,
        //            LastName = lastName,
        //            BirthPlace = birthPlace,
        //            IndexNo = indexNo,
        //            Group = group
        //        };
        //        db.Students.Add(student);
        //        db.SaveChanges();
        //    }
        //}

        public void CreateStudent(Student s)
        {
            using (var db = new StorageContext())
            {
                var group = db.Groups.Find(s.IDGroup);
                db.Students.Add(s);
                db.SaveChanges();
            }
        }
        public void UpdateStudent(Student st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Students.Find(st.IDStudent);
                if (original != null)
                {
                    original.FirstName = st.FirstName;
                    original.LastName = st.LastName;
                    db.SaveChanges();
                }
            }
        }
        public void DeleteStudent(Student st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Students.Find(st.IDStudent);
                if (original != null)
                {
                    db.Students.Remove(original);
                    db.SaveChanges();
                }
            }
        }
    }
}
