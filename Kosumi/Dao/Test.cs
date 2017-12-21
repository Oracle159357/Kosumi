using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Kosumi.Dao
{
    [Serializable]
    public class Test
    {
         public string Id { get; set; }
        public int Time { get; set; }

        public int QuestionsCount { get; set; }

        public string Title { get; set; }
       
    }
}