using MVVMStudentsList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStudentsList.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        public Storage DB = new Storage();
        public List<Student> Students { get; set; }

        public MainWindowViewModel() : base()
        {
            Students = DB.GetStudents();
            Console.WriteLine(Students.Count());
            //DBContext.Configuration.AutoDetectChangesEnabled = true;
            //var stud = new Student("Robert", "Kwiatkowicz", "Bialystok");
            //try {
            //    DBContext.Students.Add(stud);
            //    var query = from students in DBContext.Students select students;
            //    Console.WriteLine(query.Count());
            //    foreach (var item in query)
            //    {
            //        Console.WriteLine(item.FirstName);
            //    }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.InnerException.Message);
            //}
        }
    }
}
