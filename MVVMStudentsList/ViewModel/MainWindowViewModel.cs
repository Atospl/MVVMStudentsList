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

        private string firstNameControl;

        private string lastNameControl;

        private string birthDateControl;

        private SolidColorBrush borderColorDate;
        private SolidColorBrush borderColorIndex;
        private string errorLabel; 

        private List<Student> students;

        private string birthPlaceControl;

        private string indexControl;

        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindowViewModel));

        private bool isClearEnabled;

        private bool isSaveEnabled;

        private bool isNewEnabled;

        private bool isRemoveEnabled;

        #endregion private members


        public Storage DB = new Storage();

        public List<Student> Students { get { return students; } private set { students = value; base.OnPropertyChanged("Students"); } }
        public ObservableCollection<Group> Groups {
            get {
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
            Students = DB.GetStudents();
            XmlConfigurator.Configure(new System.IO.FileInfo("LogConfig.xml"));
            log.Info("MainWindowViewModel constructor");
            placeSelected = "";
            SetNullGroup();
            student = new Student("aa", "bb", "cc", "0", DateTime.Now, 0);
            birthDateControl = "1/1/2016";
            GroupControl = Groups.Where(group => group.Name.Equals("")).First();
            errorLabel = "NONONO";
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
                CheckStudentDiffs();
            }
        }
        public string FirstNameControl {
            get {
                return firstNameControl;
            }
            set {
                if (value.Equals(""))
                    BorderColorFirstName = new SolidColorBrush(Colors.Red);
                else
                    BorderColorFirstName = SystemColors.WindowBrush;
                firstNameControl = value;
                base.OnPropertyChanged("FirstNameControl");
                CheckStudentDiffs();
            }
        }
        public string LastNameControl {
            get {
                return lastNameControl;
            }
            set {
                if (value.Equals(""))
                    BorderColorLastName = new SolidColorBrush(Colors.Red);
                else
                    BorderColorLastName = SystemColors.WindowBrush;
                lastNameControl = value;
                base.OnPropertyChanged("LastNameControl");
                CheckStudentDiffs();
            }
        }
        public string BirthPlaceControl { get { return birthPlaceControl; } set { birthPlaceControl = value; base.OnPropertyChanged("BirthPlaceControl"); CheckStudentDiffs(); } }
        public SolidColorBrush BorderColorDate { get { return borderColorDate; } set { borderColorDate = value; base.OnPropertyChanged("BorderColorDate"); } }
        public SolidColorBrush BorderColorIndex { get { return borderColorIndex; } set { borderColorIndex = value; base.OnPropertyChanged("BorderColorIndex"); } }
        public SolidColorBrush BorderColorFirstName { get { return borderColorDate; } set { borderColorDate = value; base.OnPropertyChanged("BorderColorFirstName"); } }
        public SolidColorBrush BorderColorLastName { get { return borderColorIndex; } set { borderColorIndex = value; base.OnPropertyChanged("BorderColorLastName"); } }
        public string ErrorLabel { get { return errorLabel; } set { errorLabel = value; base.OnPropertyChanged("ErrorLabel"); } }
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
                    CheckStudentDiffs();
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
                    indexControl = value;
                    //IndexControl = "0";
                    base.OnPropertyChanged("IndexControl");
                    CheckStudentDiffs();
                    return;
                }
                indexControl = value;
                BorderColorIndex = SystemColors.WindowBrush;
                base.OnPropertyChanged("IndexControl");
                CheckStudentDiffs();
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

        public bool IsClearEnabled {
            get { return isClearEnabled; }
            set { isClearEnabled = value; base.OnPropertyChanged("IsClearEnabled"); }
        }
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
        public bool IsSelected { get; private set; }
        #endregion commands

        #endregion properties

        #region private methods

        private void FilterCommandMethod(string s)
        {
            log.Info("FilterCmdLog");
            FilterStudents(this.GroupSelected, this.PlaceSelected);
        }

        private void ClearCommandMethod(string s)
        {
            log.Info("ClearCmdLog");
            this.PlaceSelected = "";
            SetNullGroup();
            FilterStudents(GroupSelected, PlaceSelected);
        }

        private void SelectCommandMethod(Student s)
        {
            log.Info("SelectCmdLog");
            Console.WriteLine(s.IndexNo);
            //Console.WriteLine(GroupControl.Name);
            Console.WriteLine(PlaceSelected);
            FirstNameControl = s.FirstName;
            LastNameControl = s.LastName;
            BirthPlaceControl = s.BirthPlace;
            GroupControl = Groups.Where(group => group.IDGroup == s.IDGroup).First();
            IndexControl = s.IndexNo;
            Student = s;
            Console.WriteLine(GroupControl.Name);
            BirthDateControl = s.BirthDate.ToString("d/M/yyyy");
            IsRemoveEnabled = true;
            IsNewEnabled = false;
            IsSelected = true;

        }

        private void NewStudentCommandMethod(string s)
        {
            log.Info("NewStudentCmdLog");
            //check values
            if (GroupControl.Name.Equals("") || FirstNameControl.Equals("") || LastNameControl.Equals(""))
            {
                log.Error("Not all values inserted!");
                //TODO add popup
                return;
            }
            //check index value
            else
            {
                try
                {
                    int val = int.Parse(IndexControl);
                    //check if index exists in db
                    var students = DB.GetStudents();
                    var indexes = from student in students select student.IndexNo;
                    if (indexes.Contains(IndexControl))
                    {
                        log.Error("Index already exists!");
                        //TODO add popup

                        return;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Invalid index value");
                    return;
                }
            }
            //check group
            if (GroupControl.Name.Equals(""))
            {
                log.Error("Invalid group");
                return;
            }
            //check date
            try
            {
                DateTime BirthDate = DateTime.ParseExact(BirthDateControl, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (BirthDate > DateTime.Now)
                {
                    log.Error("Invalid date");
                    return;
                }
                Student = new Student(FirstNameControl, LastNameControl, BirthPlaceControl, IndexControl, BirthDate, GroupControl.IDGroup);
                try
                {
                    DB.CreateStudent(Student);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                SetUnselected();
                UpdateStudentsList();
            }
            catch (Exception ex)
            {
                log.Error("Invalid date");
                return;
            }
        }

        private void SaveStudentCommandMethod(string s)
        {
            log.Info("SaveStudentCmdLog");
            //check for external modifications
            Student dbStudent = DB.GetStudents().Where(stud => stud.IDStudent == Student.IDStudent).First();
            if(!dbStudent.Stamp.Equals(Student.Stamp))
            {
                string errorMsg = "Student has been externally modified";
                log.Error(errorMsg);
                ErrorLabel = errorMsg;
                UpdateStudentsList();
                SetUnselected();
                return;
            }
            else
            {

            }
            SetUnselected();
        }

        private void RemoveStudentCommandMethod(string s)
        {
            log.Info("RemoveStudentCmdLog");

            SetUnselected();
        }

        private void TextChangedCommandMethod(string s)
        {
            Console.WriteLine("TextChanged");
        }

        private void GroupSelectionMethod(string s)
        {
            Console.WriteLine(s);
        }

        private void CheckSaveRemoveMethod()
        {
            if (IndexControl.Equals("") || Student.FirstName.Equals("") || Student.LastName.Equals("") || BirthDateControl.Equals("") || Student.BirthPlace.Equals(""))
            {
                IsSaveEnabled = false;
                IsRemoveEnabled = false;
            }
            else
            {
                IsSaveEnabled = true;
                IsRemoveEnabled = true;
            }
        }

        private void FilterStudents(Group group, string place)
        {
            if (group.Name.Equals(""))
            {
                if (PlaceSelected.Equals(""))
                {
                    Students = DB.GetStudents();
                    return;
                }
                Students = DB.GetStudents().Where(stud => stud.BirthPlace.ToUpper().Contains(place.ToUpper())).ToList();
                return;
            }
            Students = DB.GetStudents().Where(stud => stud.BirthPlace.ToUpper().Contains(place.ToUpper()) && stud.IDGroup == group.IDGroup).ToList();
        }

        private void SetNullGroup()
        {
            try
            {
                GroupSelected = Groups.Where(group => group.Name.Equals("")).First();
            }
            catch (Exception ex)
            {
                log.Error("No null group!");
            }
        }

        private void SetUnselected()
        {
            FirstNameControl = "";
            LastNameControl = "";
            BirthPlaceControl = "";
            BirthDateControl = DateTime.Now.ToString("d/M/yyyy");
            IndexControl = "";
            try
            {
                GroupControl = Groups.Where(group => group.Name.Equals("")).First();
            }
            catch (Exception ex)
            {
                log.Error("No null group!");
            }
            IsSelected = false;
            IsRemoveEnabled = false;
            IsSaveEnabled = false;
            IsNewEnabled = true;
        }

        private void UpdateStudentsList()
        {
            Students = DB.GetStudents();
        }

        private void CheckStudentDiffs()
        {
            if (IsSelected)
                try
                {
                    var res1 = !Student.IndexNo.Equals(IndexControl);
                    var res2 = !Student.FirstName.Equals(FirstNameControl);
                    var res3 = !Student.LastName.Equals(LastNameControl);
                    var res4 = !Student.BirthDate.Equals(DateTime.ParseExact(BirthDateControl, "d/M/yyyy", CultureInfo.CurrentCulture));
                    var res5 = !Student.BirthPlace.Equals(BirthPlaceControl);
                    var res6 = !Student.IndexNo.Equals(IndexControl);
                    var res7 = !(Student.IDGroup == GroupControl.IDGroup);
                    if (res1 || res2 || res3 || res4 || res5 || res6 || res7)
                    {
                        IsNewEnabled = true;
                        IsSaveEnabled = true;
                    }
                    else
                    {
                        IsNewEnabled = false;
                        IsSaveEnabled = false;
                    }
                }
                catch(Exception ex)
                {
                    IsNewEnabled = true;
                }
        }
        #endregion private methods


    }
}
