using System;
using NUnit;
using Rhino;
using NUnit.Framework;
using Rhino.Mocks;
using MVVMStudentsList;

namespace MVVMStudentsListTests
{
    [TestFixture]
    public class StudListTests
    {
        [Test]
        public void TestSetNull()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            mockVM.SetUnselected();
            Assert.AreEqual(false, mockVM.IsClearEnabled);
            Assert.AreEqual(true, mockVM.IsNewEnabled);
            Assert.AreEqual(false, mockVM.IsRemoveEnabled);
            Assert.AreEqual(false, mockVM.IsSaveEnabled);
            Assert.AreEqual(false, mockVM.IsSelected);
            Assert.IsNotNull(mockVM.BirthDateControl);
        }

        [Test]
        public void TestFieldsOK1()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            mockVM.GroupSelected = mockVM.Groups[0];
            Assert.IsFalse(mockVM.FieldsOK("save"));
        }

        [Test]
        public void TestFieldsOK2()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            mockVM.GroupSelected = new MVVMStudentsList.Model.Group("A5N1");
            mockVM.FirstNameControl = "alla";
            mockVM.LastNameControl = "halla";
            mockVM.PlaceSelected = "galla";
            mockVM.BirthDateControl = "21/11/1995";
            mockVM.IndexControl = "";
            Assert.IsFalse(mockVM.FieldsOK("save"));
        }

        [Test]
        public void TestFieldsOK3()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            mockVM.Groups.Add(new MVVMStudentsList.Model.Group("A5N1"));
            mockVM.GroupControl = mockVM.Groups.Find(gr => gr.Name.Equals("A5N1"));
            mockVM.FirstNameControl = "alla";
            mockVM.LastNameControl = "halla";
            mockVM.PlaceSelected = "galla";
            mockVM.BirthDateControl = "21/11/2065";
            mockVM.IndexControl = "216613"; 
            Assert.IsFalse(mockVM.FieldsOK("save"));
        }

        [Test]
        public void TestCtor()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            Assert.NotNull(mockVM.Student);
            Assert.NotNull(mockVM.Groups);
        }

        [Test]
        public void TestClear()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            mockVM.ClearCommandMethod("");
            Assert.AreEqual("", mockVM.PlaceSelected);
        }

        [Test]
        public void TestCtor2()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            Assert.AreEqual("1/1/2016", mockVM.BirthDateControl);
        }

        [Test]
        public void TestStudentsCtor()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            Assert.AreEqual("cc", mockVM.Student.BirthPlace);
            Assert.AreEqual("1/1/2016", mockVM.BirthDateControl);
        }

        [Test]
        public void TestSetUnselected()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            mockVM.Expect(vm => vm.SetNullGroup());
            mockVM.SetUnselected();
            mockVM.AssertWasNotCalled(vm => vm.SetNullGroup());
        }

        [Test]
        public void TestCommands()
        {
            MockRepository mocks = new MockRepository();
            var mockVM = MockRepository.GenerateMock<MVVMStudentsList.ViewModel.MainWindowViewModel>();
            Assert.IsNotNull(mockVM.ClearCommand);
            Assert.IsNotNull(mockVM.FilterCommand);
            Assert.IsNotNull(mockVM.GroupSelectionCommand);
            Assert.IsNotNull(mockVM.NewStudentCommand);
        }
    }
}
