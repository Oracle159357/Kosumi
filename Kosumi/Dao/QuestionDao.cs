using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Kosumi.Dao
{
    public class QuestionDao
    {
        private void InContext(Action<List<Question>> work)
        {
            var listOfQuestion = Load();
            work(listOfQuestion);
            Save(listOfQuestion);
        }

        public void Add(Question question) => InContext(listOfQuestion => listOfQuestion.Add(question));
        public void Delete(Question question) => InContext(listOfQuestion => listOfQuestion.RemoveAll(x => x.Id == question.Id));

        public void Change(Question question) => InContext(listOfQuestion =>
            listOfQuestion[listOfQuestion.FindIndex(x => x.Id == question.Id)] = question);

        public List<Question> GetAll() => Load();

        private void Save(List<Question> question)
        {
            string json = JsonConvert.SerializeObject(question);
            File.WriteAllText("Question.json", json);
        }

        private List<Question> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("Question.json"));
            }
            catch (Exception e)
            {
                return new List<Question>();
            }
        }
    }
}