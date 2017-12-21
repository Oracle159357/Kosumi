using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Kosumi.Service;

namespace Kosumi.Presentation
{
    public class PassTestViewModel : BaseViewModel
    {
        public PassTestViewModel(MainViewModel main, Action close, Dispatcher dispatcher)
        {
            MainViewModel = main;
            _close = close;

            if (main.SelectedItemQuestion != null)
            {
                SelectedQuestion = main.SelectedItemQuestion;
            }

            _getTest = new GetTest();

            //  DispatcherTimer setup
            _dispatcherTimer = new DispatcherTimer();
            TimeLeft = MainViewModel.SelectedItemTest.Time;
            _dispatcherTimer.Tick += (sender, args) =>
            {
                TimeLeft--;
                if (TimeLeft <= 0)
                {
                    var result = _getTest.ResulOfTest(_choosedAnsewers.Values.ToList(), QuestionsList.Count);
                    MessageBox.Show("Time is over\n" + result);
                    close();
                    _dispatcherTimer.Stop();
                }
            };
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        private readonly GetTest _getTest;

        private readonly Action _close;
        public MainViewModel MainViewModel { get; set; }

        public int TimeLeft { get; set; }

        public PresentationQuestion SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                if (_choosedAnsewers.ContainsKey(value))
                {
                    SelectedAnswer = _choosedAnsewers[value];
                }
                else if(value.AnswersList.Count == 1)
                {
                    SelectedAnswer = value.AnswersList[0];
                }
            }
        }

        public PresentationAnswer SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                _selectedAnswer = value;
                if (value != null)
                {
                    _choosedAnsewers[SelectedQuestion] = SelectedAnswer;
                }
            }
        }

        public ObservableCollection<PresentationQuestion> QuestionsList =>
            new ObservableCollection<PresentationQuestion>(MainViewModel.SelectedItemTest.QuestionsList);

        public ObservableCollection<PresentationAnswer> AnswersList =>
            new ObservableCollection<PresentationAnswer>(
                SelectedQuestion?.AnswersList ?? new List<PresentationAnswer>());

        private readonly Dictionary<PresentationQuestion, PresentationAnswer> _choosedAnsewers =
            new Dictionary<PresentationQuestion, PresentationAnswer>();

        private PresentationAnswer _selectedAnswer;
        private PresentationQuestion _selectedQuestion;
        private readonly DispatcherTimer _dispatcherTimer;

        public ICommand FinishTestCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var result = _getTest.ResulOfTest(_choosedAnsewers.Values.ToList(), QuestionsList.Count);
                MessageBox.Show(result);
                _close();
                _dispatcherTimer.Stop();
            },
            CanExecuteDelegate = o => QuestionsList.All(q => _choosedAnsewers.ContainsKey(q) && q.AnswersList.Any(a =>  _choosedAnsewers[q] == a))
        };
    }
}