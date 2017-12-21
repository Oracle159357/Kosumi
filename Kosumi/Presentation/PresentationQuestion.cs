using System.Collections.Generic;

namespace Kosumi.Presentation
{
    public class PresentationQuestion
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public List<PresentationAnswer> AnswersList { get; set; }
    }
}