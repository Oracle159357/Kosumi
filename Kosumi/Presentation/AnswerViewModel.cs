using System;
using System.Windows.Input;

namespace Kosumi.Presentation
{
    public class AnswerViewModel
    {
        public AnswerViewModel(MainViewModel main, Action close, bool isNew)
        {
            Answer = new PresentationAnswer();
            MainViewModel = main;
            _close = close;
            IsNew = isNew;
            if (main.SelectedItemAnswer != null)
            {
                Answer = main.SelectedItemAnswer;
            }
        }

        private readonly Action _close;
        public PresentationAnswer Answer { get; set; }
        public MainViewModel MainViewModel { get; set; }
        public bool IsNew { get; set; }

        public SimpleCommand StateAdd => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.AddAnswer(Answer);
                _close();
            },
            CanExecuteDelegate = o => (!string.IsNullOrEmpty(Answer.Value))
        };

        public SimpleCommand StateChange => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.ChangeAnswer(Answer);
                _close();
            },
            CanExecuteDelegate = o => (!string.IsNullOrEmpty(Answer.Value))
        };

        public ICommand AddAnswer=> IsNew ? StateAdd : StateChange;

        public SimpleCommand SaveAndExit => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                MainViewModel.RefreshAnswer();
                _close();
            }
        };

        public SimpleCommand Exit => new SimpleCommand
        {
            ExecuteDelegate = o => _close()
        };

        public ICommand CancelAnswer => (IsNew == false) ? SaveAndExit : Exit;
    }
}

