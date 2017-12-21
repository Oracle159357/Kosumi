using System;
using System.Windows.Input;

namespace Kosumi.Presentation
{
    public class TestViewModel
    {
        public TestViewModel(MainViewModel main, Action close, bool additing)
        {
            Test = new PresentationTest();
            MainViewModel = main;
            _close = close;
            IsNew = additing;
            if (main.SelectedItemTest != null)
            {
                Test = main.SelectedItemTest;
            }
        }
        private readonly Action _close;
        public PresentationTest Test { get; set; }
        public MainViewModel MainViewModel { get; set; }
        public bool IsNew { get; set; }

        public SimpleCommand StateAdd => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.AddTest(Test);
                _close();
            },
            CanExecuteDelegate = o => (Test.Time != 0 && !string.IsNullOrEmpty(Test.Title) && Test.QuestionsCount != 0)
        };
        public SimpleCommand StateChange => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.ChangeTest(Test);
                _close();
            },
            CanExecuteDelegate = o => Test.Time != 0 && !string.IsNullOrEmpty(Test.Title) && Test.QuestionsCount != 0
        };

        public ICommand AddTest => IsNew ? StateAdd : StateChange;

        public SimpleCommand SaveAndExit => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.RefreshTest();
                _close();
            }
        };
        public SimpleCommand Exit => new SimpleCommand
        {
            ExecuteDelegate = o => _close()
        };

        public ICommand CancelTest => (IsNew == false) ? SaveAndExit : Exit;

        //public ICommand AddTest => new SimpleCommand
        //{
        //    ExecuteDelegate = o =>
        //    {
        //        MainViewModel.AddTest(Test);
        //        _close();
        //    },
        //    CanExecuteDelegate = o => Test.Time != 0 && !string.IsNullOrEmpty(Test.Title) && Test.QuestionsCount != 0
        //};

        ////public ICommand Change => new SimpleCommand
        ////{
        ////    ExecuteDelegate = o =>
        ////    {
        ////        MainViewModel.ChangeTest(Test);
        ////        _close();
        ////    },
        ////    CanExecuteDelegate = o => Test.Time != 0 && !string.IsNullOrEmpty(Test.Title) && Test.QuestionsCount != 0
        //};
    }
}