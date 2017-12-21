using System;
using Kosumi.Dao;
using Kosumi.Presentation;

namespace Kosumi.Service
{
    public class BasicsOperationQuestion
    {
        private readonly QuestionDao _questionDao = new QuestionDao();
        public void AddQuestion(PresentationQuestion question, string testId) => _questionDao.Add(new Question { FkTest = testId, Value = question.Value, Id = Guid.NewGuid().ToString() });
        public void DeleteQuestion(PresentationQuestion question, string testId) => _questionDao.Delete(new Question { FkTest = testId, Value = question.Value, Id = question.Id });
        public void ChangeQuestion(PresentationQuestion question, string testId) => _questionDao.Change(new Question { FkTest = testId, Value = question.Value, Id = question.Id });
    }
}