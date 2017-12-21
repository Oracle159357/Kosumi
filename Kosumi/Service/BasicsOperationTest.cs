using System;
using Kosumi.Dao;
using Kosumi.Presentation;

namespace Kosumi.Service
{
    public class BasicsOperationTest
    {
        private readonly TestDao _testDao = new TestDao();
        public void AddTest(PresentationTest test) => _testDao.Add(new Test { Title = test.Title, QuestionsCount = test.QuestionsCount, Time = test.Time, Id = Guid.NewGuid().ToString() });
        public void DeleteTest(PresentationTest test) => _testDao.Delete(new Test { Title = test.Title, QuestionsCount = test.QuestionsCount, Time = test.Time, Id = test.Id });
        public void ChangeTest(PresentationTest test) => _testDao.Change(new Test { Title = test.Title, QuestionsCount = test.QuestionsCount, Time = test.Time, Id = test.Id });
    }
}