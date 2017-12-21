using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Kosumi.Dao
{

    public class Answer 
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public bool Truthful { get; set; }
        public string FkQuestion{ get; set; }

    }
}