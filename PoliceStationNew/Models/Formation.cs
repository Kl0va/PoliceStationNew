using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Formation
    {
        public int id { get; set; }
        public string formation_name { get; set; }
        public string education_name { get; set; }

        public Formation(string Formation_name, string Education_name)
        {
            formation_name = Formation_name;
            education_name = Education_name;
        }
    }
}
