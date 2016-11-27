using log4net;
using log4net.Config;
using MVVMStudentsList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMStudentsList.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        #region private members

        private Group groupSelected;

        private string placeSelected;

        private Group groupControl;

        private string firstNameControl;

        private string lastNameControl;

        private string birthDateControl;

        private string birthPlaceControl;

        private string indexControl;

        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindowViewModel));

        private bool isClearEnabled;

        private bool isSaveEnabled;

        private bool isNewEnabled;

        private bool isRemoveEnabled;

        #endregion private members


        public Storage DB = new Storage();
        public List<Student> Students { get { return DB.GetStudents(); } }
        public List<Group> Groups { get { return DB.GetGroups(); } }

        #region ctor
        public MainWindowViewModel() : base()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("LogConfig.xml"));
            log.Info("MainWindowViewModel constructor");
            placeSelected = "";
            GroupSelected = new Group("");

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

            FirstNameControl = "fname";
            LastNameControl = "lname";
            BirthDateControl = "biday";
            BirthPlaceControl = "biplace";
            IndexControl = "indx";
            Console.WriteLine(Students.Count());
        }

        #endregion ctor

        #region properties
        public Group GroupSelected {
            get {
                return this.groupSelected;
            }
            set {
                groupSelected = value; if (placeSelected.Equals("") && value.Name.Equals("")) IsClearEnabled = false; else IsClearEnabled = true;
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
        public Group GroupControl { get { return groupControl; } set { groupControl = value; base.OnPropertyChanged("GroupControl"); } }
        public string FirstNameControl { get { return firstNameControl; } set { firstNameControl = value; base.OnPropertyChanged("FirstNameControl"); } }
        public string LastNameControl { get { return lastNameControl; } set { lastNameControl = value; base.OnPropertyChanged("LastNameControl"); } }
        public string BirthPlaceControl { get { return birthPlaceControl; } set { birthPlaceControl = value; base.OnPropertyChanged("BirthPlaceControl"); } }
        public string BirthDateControl { get { return birthDateControl; } set { birthDateControl = value; base.OnPropertyChanged("BirthDateControl"); } }
        public string IndexControl { get { return indexControl; } set { indexControl = value; base.OnPropertyChanged("IndexControl"); } }

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
        #endregion commands

        #endregion properties

        #region private methods

        private void FilterCommandMethod(string s)
        {
            log.Info("FilterCmdLog");

        }

        private void ClearCommandMethod(string s)
        {
            log.Info("ClearCmdLog");
        }

        private void SelectCommandMethod(Student s)
        {
            log.Info("SelectCmdLog");
            Console.WriteLine(s.IndexNo);
            Console.WriteLine(GroupSelected.Name);
            Console.WriteLine(PlaceSelected);

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
        #endregion private methods

    }
}
