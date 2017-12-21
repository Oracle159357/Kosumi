using System.Collections.Generic;
using System.Linq;
using Kosumi.Dao;
using Kosumi.Presentation;

namespace Kosumi.Service
{
    public class GetTest
    {
        private readonly TestDao _testDao = new TestDao();
        private readonly QuestionDao _questionDao = new QuestionDao();
        private readonly AnswerDao _answerDao = new AnswerDao();
        
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

        public List<PresentationQuestion> GetPresentationQuestions(string testId)
        {
            return GetPresentationQuestions(_answerDao.GetAll(), _questionDao.GetAll(), testId);
        }
        public List<PresentationAnswer> GetPresentationAnswers(string questionId)
        {
            return GetPresentationAnswers(_answerDao.GetAll(), questionId);
        }

        private List<PresentationQuestion> GetPresentationQuestions(List<Answer> answerEntities, List<Question> questionEntities, string testId)
        {
            return questionEntities.FindAll(x => x.FkTest == testId).Select(question =>
                new PresentationQuestion
                {
                    Value = question.Value,
                    Id = question.Id,
                    AnswersList = GetPresentationAnswers(answerEntities, question.Id)
                }
            ).ToList();
        }

        private List<PresentationAnswer> GetPresentationAnswers(List<Answer> answerEntities, string questionId)
        {
            return answerEntities.FindAll(x => x.FkQuestion == questionId).Select(answer =>
                new PresentationAnswer
                {
                    Value = answer.Value,
                    Id = answer.Id,
                    Truthful = answer.Truthful
                }).ToList();
        }

        public string ResulOfTest(List<PresentationAnswer> presentationAnswers, int total)
        {
            var allWrongQuestions=presentationAnswers.FindAll(answer => (answer.Truthful == false));
            var allTrueAnswers = presentationAnswers.FindAll(answer => (answer.Truthful == true));
            int procentCorrectle = (allTrueAnswers.Count * 100) / total;
            var x = "Result\n" +
                    "There were total " + presentationAnswers.Count + " questions.\n" +
                    "Answered correctly: " + allTrueAnswers.Count + ".\n" +
                    "Answered incorrectly: " + allWrongQuestions.Count + ".\n" +
                    "Not answered : " + (total - presentationAnswers.Count) + ".\n" +
                    "Percentile: " + procentCorrectle + "%";
            return x;
        }
    }
}