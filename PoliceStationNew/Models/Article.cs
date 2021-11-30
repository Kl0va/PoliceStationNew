using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Article
    {
        public int id { get; set; }
        public int number { get; set; }
        public string text { get; set; }
        public string codex_id { get; set; }
        
        public Article(int Number,string Text, string Codex_id)
        {
            number = Number;
            text = Text;
            codex_id = Codex_id;
        }
    }
}
