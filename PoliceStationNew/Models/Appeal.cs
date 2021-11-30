using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Appeal
    {
        public string status { get; set; }
        public string appeal_name { get; set; }
        public string text { get; set; }
        public int declarant_id { get; set; }
        
        [JsonConstructor]
        public Appeal(string Status,string Text,string Appeal_name, int Declarant)
        {
            status = Status;
            appeal_name = Appeal_name;
            text = Text;
            declarant_id = Declarant;
        }
        public Appeal(string Status,string Appeal_name)
        {
            status = Status;
            appeal_name = Appeal_name;
        }
    }
}
