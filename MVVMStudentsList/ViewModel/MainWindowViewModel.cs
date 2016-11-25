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
        #region private members

        private string groupControl;

        private string firstNameControl;

        private string lastNameControl;

        private string birthDateControl;

        private string birthPlaceControl;

        private string indexControl;

        #endregion private members


        public Storage DB = new Storage();
        public List<Student> Students { get { return DB.GetStudents(); } }

        #region ctor
        public MainWindowViewModel() : base()
        {
            GroupControl = "group";
            FirstNameControl = "fname";
            LastNameControl = "lname";
            BirthDateControl = "biday";
            BirthPlaceControl = "biplace";
            IndexControl = "indx";
            Console.WriteLine(Students.Count());
            //var str = Students.First().Group.Name;

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

        #endregion ctor

        #region properties

        public string GroupControl { get { return groupControl; } set { groupControl = value; base.OnPropertyChanged("GroupControl"); } }
        public string FirstNameControl { get { return firstNameControl; } set { firstNameControl = value; base.OnPropertyChanged("FirstNameControl"); } }
        public string LastNameControl { get { return lastNameControl; } set { lastNameControl = value; base.OnPropertyChanged("LastNameControl"); } }
        public string BirthPlaceControl { get { return birthPlaceControl; } set { birthPlaceControl = value; base.OnPropertyChanged("BirthPlaceControl"); } }
        public string BirthDateControl { get { return birthDateControl; } set { birthDateControl = value; base.OnPropertyChanged("BirthDateControl"); } }
        public string IndexControl { get { return indexControl; } set { indexControl = value; base.OnPropertyChanged("IndexControl"); } }

        #endregion properties
    }
}
