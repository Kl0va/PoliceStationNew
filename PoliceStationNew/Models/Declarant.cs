using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Declarant
    {
        public int id { get; set; }
        public string first { get; set; }
        public string second { get; set; }
        public string middle { get; set; }
        public string seria_pas { get; set; }
        public string number_pas { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Declarant(string First,string Second,string Middle,string Seria_pas, string Number_pas,string Number,string Email,string Password)
        {
            first = First;
            second = Second;
            middle = Middle;
            seria_pas = Seria_pas;
            number_pas = Number_pas;
            number = Number;
            email = Email;
            password = Password;
        }
    }
}
