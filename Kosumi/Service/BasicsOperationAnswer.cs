using System;
using Kosumi.Dao;
using Kosumi.Presentation;

namespace Kosumi.Service
{
    public class BasicsOperationAnswer
    {
        private readonly AnswerDao _answerDao = new AnswerDao();
        public void AddAnswer(PresentationAnswer answer, string questionId) => _answerDao.Add(new Answer { FkQuestion = questionId, Truthful = answer.Truthful, Id = Guid.NewGuid().ToString(), Value = answer.Value });
        public void DeleteAnswer(PresentationAnswer answer, string questionId) => _answerDao.Delete(new Answer { FkQuestion = questionId, Truthful = answer.Truthful, Id = answer.Id, Value = answer.Value });
        public void ChangeAnswer(PresentationAnswer answer, string questionId) => _answerDao.Change(new Answer { FkQuestion = questionId, Truthful = answer.Truthful, Id = answer.Id, Value = answer.Value });
       
    }
}