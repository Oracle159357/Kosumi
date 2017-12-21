using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Kosumi.Service;

namespace Kosumi.Presentation
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            _getTest = new GetTest();
            _basicsOperationTest=new BasicsOperationTest();
            _basicsOperationQuestion=new BasicsOperationQuestion();
            _basicsOperationAnswer=new BasicsOperationAnswer();
            RefreshTest();
        }

        public PresentationTest SelectedItemTest
        {
            get { return _selectedItemTest; }
            set
            {
                _selectedItemTest = value;
                RefreshQuestions();
            }
        }

        public PresentationQuestion SelectedItemQuestion
        {
            get { return _selectedItemQuestion; }
            set
            {
                _selectedItemQuestion = value;
                RefreshAnswer();
            }
        }

        public PresentationAnswer SelectedItemAnswer { get; set; }
        private readonly BasicsOperationTest _basicsOperationTest;
        private readonly BasicsOperationQuestion _basicsOperationQuestion;
        private readonly BasicsOperationAnswer _basicsOperationAnswer;
        private readonly GetTest _getTest;
        private PresentationTest _selectedItemTest;
        private PresentationQuestion _selectedItemQuestion;

        public ObservableCollection<PresentationTest> Tests { get; set; } =
        new ObservableCollection<PresentationTest>();
        public ObservableCollection<PresentationQuestion> Questions { get; set; } =
            new ObservableCollection<PresentationQuestion>();
        public ObservableCollection<PresentationAnswer>  Answers { get; set; } =
            new ObservableCollection<PresentationAnswer>();
        public void RefreshTest()
        {
            Tests = new ObservableCollection<PresentationTest>(_getTest.GetTests());
        }
        public void RefreshQuestions()
        {
            if (SelectedItemTest != null)
            {
                SelectedItemTest.QuestionsList = _getTest.GetPresentationQuestions(SelectedItemTest.Id);

                Questions = new ObservableCollection<PresentationQuestion>(SelectedItemTest.QuestionsList);

            }
            else
            {
                Questions = null;
            }
          }
        public void RefreshAnswer()
        {
            if (SelectedItemQuestion != null)
            {
                SelectedItemQuestion.AnswersList = _getTest.GetPresentationAnswers(SelectedItemQuestion.Id);
                Answers = new ObservableCollection<PresentationAnswer>(SelectedItemQuestion.AnswersList);
            }
            else
            {
                Answers = null;
            }
        }
        public ICommand AddTestCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var testWindow=new TestWindow(this,true);
                testWindow.ShowDialog();
            }
        };
        public ICommand ChangeTestCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var testWindow = new TestWindow(this,false);
                testWindow.ShowDialog();
            },
            CanExecuteDelegate = o => SelectedItemTest != null
        };
        public ICommand DeleteTestCommand => new SimpleCommand
        {
            ExecuteDelegate = o => DeleteTest(SelectedItemTest),
            CanExecuteDelegate = o => SelectedItemTest != null
        };
        public void AddTest(PresentationTest test)
        {
            _basicsOperationTest.AddTest(test);
            RefreshTest();
        }
     
        public void ChangeTest(PresentationTest test)
        {
            _basicsOperationTest.ChangeTest(test);
            RefreshTest();
        }
        public void DeleteTest(PresentationTest test)
        {
            _basicsOperationTest.DeleteTest(test);
            RefreshTest();
        }
       
        public ICommand AddQuestionCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var questionWindow = new QuestionWindow(this, true);
                questionWindow.ShowDialog();
            },CanExecuteDelegate = o => SelectedItemTest!=null&& (SelectedItemTest.QuestionsCount >SelectedItemTest.QuestionsList.Count)
        };
        public ICommand ChangeQuestionCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var questionWindow = new QuestionWindow(this,false);
                questionWindow.ShowDialog();
            },
            CanExecuteDelegate = o => SelectedItemQuestion != null && SelectedItemTest != null
        };
        public ICommand DeleteQuestionCommand => new SimpleCommand
        {
            ExecuteDelegate = o => DeleteQuestions(SelectedItemQuestion),
            CanExecuteDelegate = o => SelectedItemQuestion != null && SelectedItemTest != null
        };
        public void AddQuestion(PresentationQuestion question)
        {
            _basicsOperationQuestion.AddQuestion(question,SelectedItemTest.Id);
            RefreshQuestions();
           
        }
        public void ChangeQuestion(PresentationQuestion question)
        {
            _basicsOperationQuestion.ChangeQuestion(question,SelectedItemTest.Id);
            RefreshQuestions();
        }

        public void DeleteQuestions(PresentationQuestion question)
        {
            _basicsOperationQuestion.DeleteQuestion(question,SelectedItemTest.Id);
            RefreshQuestions();
        }
        public ICommand AddAnswerCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var answerWindow = new AnswerWindow(this, true);
                answerWindow.ShowDialog();
            },
            CanExecuteDelegate = o => SelectedItemQuestion != null 
        };
        public ICommand ChangeAnswerCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var answerWindow = new AnswerWindow(this, false);
                answerWindow.ShowDialog();
            },
            CanExecuteDelegate = o => SelectedItemQuestion != null && SelectedItemAnswer!=null
        };
        public ICommand DeleteAnswerCommand => new SimpleCommand
        {
            ExecuteDelegate = o => DeleteAnswer(SelectedItemAnswer),
            CanExecuteDelegate = o => SelectedItemQuestion != null && SelectedItemTest != null && SelectedItemAnswer!=null
        };
        public void AddAnswer(PresentationAnswer answer)
        {
            _basicsOperationAnswer.AddAnswer(answer,SelectedItemQuestion.Id);
            RefreshQuestions();

        }
        public void ChangeAnswer(PresentationAnswer answer)
        {
            _basicsOperationAnswer.ChangeAnswer(answer,SelectedItemQuestion.Id);
            RefreshQuestions();
        }

        public void DeleteAnswer(PresentationAnswer answer)
        {
            _basicsOperationAnswer.DeleteAnswer(answer,SelectedItemQuestion.Id);
            RefreshAnswer();
        }
        public ICommand StartTestCommand => new SimpleCommand
        {
            ExecuteDelegate = o =>
            {
                var questionWindow = new PassTestWindow(this);
                questionWindow.ShowDialog();
            },
            CanExecuteDelegate = o => SelectedItemTest?.QuestionsList?.All(x => (x.AnswersList?.Count ?? 0) > 0) ?? false
        };
    }
}