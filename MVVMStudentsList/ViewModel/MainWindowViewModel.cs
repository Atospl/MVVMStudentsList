using log4net;
using log4net.Config;
using MVVMStudentsList.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVMStudentsList.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        #region private members

        private Group groupSelected;

        private string placeSelected;

        public Student student;

        private Group groupControl;

        //private string firstNameControl;

        //private string lastNameControl;

        private string birthDateControl;

        private SolidColorBrush borderColorDate;
        private SolidColorBrush borderColorIndex;


        //private string birthPlaceControl;

        private string indexControl;

        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindowViewModel));

        private bool isClearEnabled;

        private bool isSaveEnabled;

        private bool isNewEnabled;

        private bool isRemoveEnabled;

        #endregion private members


        public Storage DB = new Storage();
        public List<Student> Students { get { return DB.GetStudents(); } }
        public ObservableCollection<Group> Groups { get {
                var groups = DB.GetGroups();
                var oc = new ObservableCollection<Group>();
                foreach (var group in groups)
                    oc.Add(group);
                return oc;
            }
        }

        #region ctor
        public MainWindowViewModel() : base()
        {

            XmlConfigurator.Configure(new System.IO.FileInfo("LogConfig.xml"));
            log.Info("MainWindowViewModel constructor");
            placeSelected = "";
            //GroupSelected = Groups.First();
            student = new Student("aa", "bb", "cc", 0, 0);
            birthDateControl = "1/1/2016";


            //Bools initialization
            IsClearEnabled = false;
            IsSaveEnabled = false;
            IsNewEnabled = true;
            IsRemoveEnabled = false;


            //binding commands to methods
            FilterCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                FilterCommandMethod(obj as string);
            }));
            ClearCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                ClearCommandMethod(obj as string);
            }));
            SelectCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                SelectCommandMethod(obj as Student);
            }));
            NewStudentCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                NewStudentCommandMethod(obj as string);
            }));
            SaveStudentCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                SaveStudentCommandMethod(obj as string);
            }));
            RemoveStudentCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                RemoveStudentCommandMethod(obj as string);
            }));
            TextChangedCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                TextChangedCommandMethod(obj as string);
            }));
            GroupSelectionCommand = new RelayCommand(new Action<object>(delegate (object obj) {
                GroupSelectionMethod(obj as string);
            }));
            //FirstNameControl = "fname";
            //LastNameControl = "lname";
            //BirthDateControl = "biday";
            //BirthPlaceControl = "biplace";
            //IndexControl = "indx";
            Console.WriteLine(Students.Count());
        }

        #endregion ctor

        #region properties
        public Group GroupSelected {
            get {
                return this.groupSelected;
            }
            set {
                groupSelected = value;
                if (placeSelected.Equals("") && value.Name.Equals(""))
                    IsClearEnabled = false;
                else
                    IsClearEnabled = true;
                base.OnPropertyChanged("GroupSelected");
            }
        }
        public string PlaceSelected {
            get {
                return placeSelected;
            }

            set {
                if (value.Equals(""))
                {
                    if (GroupSelected.Name.Equals(""))
                        IsClearEnabled = false;
                }
                else
                    IsClearEnabled = true;
                placeSelected = value;
                base.OnPropertyChanged("PlaceSelected");
            }
        }
        public Group GroupControl {
            get {
                return groupControl;
                    }
            set {
                groupControl = value;
                base.OnPropertyChanged("GroupControl");
            }
        }
        //public string FirstNameControl { get { return firstNameControl; } set { firstNameControl = value; base.OnPropertyChanged("FirstNameControl"); } }
        //public string LastNameControl { get { return lastNameControl; } set { lastNameControl = value; base.OnPropertyChanged("LastNameControl"); } }
        //public string BirthPlaceControl { get { return birthPlaceControl; } set { birthPlaceControl = value; base.OnPropertyChanged("BirthPlaceControl"); } }
        public SolidColorBrush BorderColorDate { get { return borderColorDate; } set { borderColorDate = value; base.OnPropertyChanged("BorderColorDate"); } }
        public SolidColorBrush BorderColorIndex { get { return borderColorIndex; } set { borderColorIndex = value; base.OnPropertyChanged("BorderColorIndex"); } }

        public string BirthDateControl {
            get {
                return birthDateControl;
            }
            set {
                if (value.Equals(""))
                    birthDateControl = "1/1/2016";
                try
                {
                    var date = DateTime.ParseExact(value, "d/M/yyyy", CultureInfo.CurrentCulture);
                    birthDateControl = value;
                    Student.BirthDate = date;
                    BorderColorDate = SystemColors.WindowBrush;
                }
                catch (FormatException ex)
                {
                    BorderColorDate = new SolidColorBrush(Colors.Red);
                    birthDateControl = value;
                }
                finally
                {
                    base.OnPropertyChanged("BirthDateControl");
                }
            }
        }
        public string IndexControl {
            get {
                return indexControl;
            }
            set {
                int val;
                if (!int.TryParse(value as string, out val))
                {
                    BorderColorIndex = new SolidColorBrush(Colors.Red);
                    return;
                }
                indexControl = value;
                BorderColorIndex = SystemColors.WindowBrush;
                base.OnPropertyChanged("IndexControl");
            }
        }
        public Student Student {
            get {
                return student;
            }
            set {
                student = value;
                base.OnPropertyChanged("Student");
            }
        }

        public bool IsClearEnabled { get { return isClearEnabled; }
            set { isClearEnabled = value; base.OnPropertyChanged("IsClearEnabled"); } }
        public bool IsSaveEnabled { get { return isSaveEnabled; } set { isSaveEnabled = value; base.OnPropertyChanged("IsSaveEnabled"); } }
        public bool IsNewEnabled { get { return isNewEnabled; } set { isNewEnabled = value; base.OnPropertyChanged("IsNewEnabled"); } }
        public bool IsRemoveEnabled { get { return isRemoveEnabled; } set { isRemoveEnabled = value; base.OnPropertyChanged("IsRemoveEnabled"); } }

        #region commands
        public ICommand FilterCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand SelectCommand { get; private set; }
        public ICommand NewStudentCommand { get; private set; }
        public ICommand SaveStudentCommand { get; private set; }
        public ICommand RemoveStudentCommand { get; private set; }
        public ICommand TextChangedCommand { get; set; }
        public ICommand GroupSelectionCommand { get; set; }
        #endregion commands

        #endregion properties

        #region private methods

        private void FilterCommandMethod(string s)
        {
            log.Info("FilterCmdLog");
            Console.WriteLine(Student.BirthDate);
            Console.WriteLine(Student.FirstName);
            Console.WriteLine(Student.LastName);
        }

        private void ClearCommandMethod(string s)
        {
            log.Info("ClearCmdLog");
        }

        private void SelectCommandMethod(Student s)
        {
            log.Info("SelectCmdLog");
            Console.WriteLine(s.IndexNo);
            //Console.WriteLine(GroupControl.Name);
            Console.WriteLine(PlaceSelected);
            Student = s;
            GroupControl = Groups.Where(group => group.IDGroup == s.IDGroup).First();
            
            BirthDateControl = s.BirthDate.ToString("d/M/yyyy");
        }

        private void NewStudentCommandMethod(string s)
        {
            log.Info("NewStudentCmdLog");
        }

        private void SaveStudentCommandMethod(string s)
        {
            log.Info("SaveStudentCmdLog");
        }

        private void RemoveStudentCommandMethod(string s)
        {
            log.Info("RemoveStudentCmdLog");
        }

        private void TextChangedCommandMethod(string s)
        {
            Console.WriteLine("TextChanged");
        }

        private void GroupSelectionMethod(string s)
        {
            Console.WriteLine(s);
        }
        #endregion private methods

    }
}
