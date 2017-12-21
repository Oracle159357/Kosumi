using System;
using System.Collections.Generic;
using System.Linq;
using Kosumi.Dao;
using Kosumi.Presentation;

namespace Kosumi.Service
{
    public class LogicaOfApplication
    {
        public TestDao _testDao = new TestDao();
        public QuestionDao _questionDao = new QuestionDao();
        public AnswerDao _answerDao = new AnswerDao();

        
        public PresentationAnswer AddItemAnswer(string value, bool truthful)=>new PresentationAnswer{Value = value,Id= Guid.NewGuid().ToString(),Truthful = truthful};
        public PresentationQuestion AddItemQuestion(string value, string id)=> new PresentationQuestion {Value = value, Id = Guid.NewGuid().ToString() };
        public PresentationTest AddItemTest(string title, int questionsCount,int time,int id)=> new PresentationTest { Title = title, Id = Guid.NewGuid().ToString(), Time = time, QuestionsCount = questionsCount};
        
        public List<PresentationQuestion> AllQuestionsInTest(PresentationTest testQuestins)=>testQuestins.QuestionsList;
        public List<PresentationAnswer> AllAnswersInQuestions(PresentationQuestion questionAnswer) => questionAnswer.AnswersList;
        public PresentationAnswer AddItemAnswerById(string value, bool truthful,string questionId) =>
            new PresentationAnswer { Value = value, Id = Guid.NewGuid().ToString(), Truthful = truthful };

        public List<PresentationTest> GetTests()
        {
            var tests = _testDao.GetAll();
            var questions = _questionDao.GetAll();
            var answers = _answerDao.GetAll();
         
            return tests.Select(test => new PresentationTest
            {
                Title = test.Title,
                Id = test.Id,
                Time = test.Time,
                QuestionsCount = test.QuestionsCount,
                QuestionsList = GetPresentationQuestions(answers, questions, test.Id)
            }).ToList();
        }

        private List<PresentationQuestion> GetPresentationQuestions(List<Answer> answerEntities, List<Question> questionEntities, string testId)
        {
            return questionEntities.FindAll(x => x.Id == testId).Select(question =>
                new PresentationQuestion {
                    Value = question.Value, Id = question.Id,
                    AnswersList = GetPresentationAnswers(answerEntities, question.Id)}
                ).ToList();
        }

        private List<PresentationAnswer> GetPresentationAnswers(List<Answer> answerEntities, string questionId)
        {
            return answerEntities.FindAll(x => x.Id == questionId).Select(answer =>
                new PresentationAnswer
                {
                    Value = answer.Value,
                    Id = answer.Id,
                    Truthful = answer.Truthful
                }).ToList();
        }


        //   Answer answer=new Answer();
        //    new AnswerDao().Add(x);
    }
}