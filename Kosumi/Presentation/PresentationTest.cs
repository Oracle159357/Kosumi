using System;
using System.Collections.Generic;

namespace Kosumi.Presentation
{
    public class PresentationTest
    {
        public string Id { get; set; }
        public int Time { get; set; }

        public int QuestionsCount { get; set; }

        public string Title { get; set; }

        public List<PresentationQuestion> QuestionsList { get; set; }
    }
}