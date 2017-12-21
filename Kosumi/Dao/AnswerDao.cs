using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Kosumi.Dao
{
    public class AnswerDao
    {
        private void InContext(Func<List<Answer>, List<Answer>> work)
        {
            var listOfAnswer = Load();
            listOfAnswer = work(listOfAnswer);
            Save(listOfAnswer);
        }

        private void InContext(Action<List<Answer>> work)
        {
            var listOfAnswer = Load();
            work(listOfAnswer);
            Save(listOfAnswer);
        }

        public void Add(Answer answer) => InContext(listOfAnswer => listOfAnswer.Add(answer));
        public void Delete(Answer answer) => InContext(listOfAnswer => listOfAnswer.RemoveAll(x=>x.Id==answer.Id));
        
        public void Change(Answer answer) => InContext(listOfAnswer =>
            listOfAnswer[listOfAnswer.FindIndex(x => x.Id == answer.Id)] = answer);

        public List<Answer> GetAll() => Load();

        private void Save(List<Answer> answer)
        {
            string json = JsonConvert.SerializeObject(answer);
            File.WriteAllText("Answer.json", json);
        }

        private List<Answer> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Answer>>(File.ReadAllText("Answer.json"));
            }
            catch (Exception e)
            {
                return new List<Answer>();
            }
        }
    }
}