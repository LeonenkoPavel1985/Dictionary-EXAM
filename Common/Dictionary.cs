using System;
using System.Collections.Generic;

namespace Common
{
    public class Dictionary
    {
        public int id { get; set; }
        public string type { get; set; }
        public List<Word> words { get; set; }
    }
    public class Word
    {
        public string initial { get; set; }
        public List<string> translation { get; set; }
    }
}
