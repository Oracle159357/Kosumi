using System;
using System.Windows.Input;

namespace Kosumi.Presentation
{
    public class QuestionViewModel
    {
        public QuestionViewModel(MainViewModel main, Action close,bool isNew)
        {
            Question = new PresentationQuestion();
            MainViewModel = main;
            _close = close;
            IsNew = isNew;
            if (main.SelectedItemQuestion != null)
            {
                Question = main.SelectedItemQuestion;
            }
        }
        private readonly Action _close;
        public PresentationQuestion Question { get; set; }
        public MainViewModel MainViewModel { get; set; }
        public bool IsNew { get; set; }

        public SimpleCommand StateAdd => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
               MainViewModel.AddQuestion(Question);
                _close();
            },
            CanExecuteDelegate = o => ( !string.IsNullOrEmpty(Question.Value))
        };
        public SimpleCommand StateChange => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.ChangeQuestion(Question);
                _close();
            },
            CanExecuteDelegate = o => (!string.IsNullOrEmpty(Question.Value))
        };

        public ICommand AddQuestion => IsNew ? StateAdd : StateChange;

        public SimpleCommand SaveAndExit => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.RefreshQuestions();
                _close();
            }
        };
        public SimpleCommand Exit => new SimpleCommand
        {
            ExecuteDelegate = o => _close()
        };

        public ICommand CancelQuestion=> (IsNew == false) ? SaveAndExit : Exit;
    }
}