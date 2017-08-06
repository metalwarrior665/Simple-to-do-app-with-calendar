using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appka
{
    [Serializable]
    public class SaveObject
    {
        public Dictionary<DateTime, List<string>> DictionarySave { get; set; }
        public DateTime[] BoldedDatesSave { get; set; }
        public List<string> GlobalQuestsSave { get; set; }
    }
}
