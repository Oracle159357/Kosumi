using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Kosumi.Dao
{
    public class Question
    {
        public string Id { get; set; }
        public string FkTest { get; set; }

        public string Value { get; set; }
    }
}