using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Kosumi.Dao
{
    public class TestDao
    {
        private void InContext(Action<List<Test>> work)
        {
            var listOfTest = Load();
            work(listOfTest);
            Save(listOfTest);
        }

        public void Add(Test test) => InContext(listOfQuestion => listOfQuestion.Add(test));
        public void Delete(Test test) => InContext(listOfQuestion => listOfQuestion.RemoveAll(x => x.Id == test.Id));
        public void Change(Test test) => InContext(listOfTest =>
            listOfTest[listOfTest.FindIndex(x => x.Id == test.Id)] = test);
        public List<Test> GetAll() => Load();

        private void Save(List<Test> test)
        {
            string json = JsonConvert.SerializeObject(test);
            File.WriteAllText("Test.json", json);
        }

        private List<Test> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Test>>(File.ReadAllText("Test.json"));
            }
            catch (Exception e)
            {
                return new List<Test>();
            }
        }
    }
}