using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Position
    {
        public int id { get; set; }
        public string name { get; set; }
        public float salary { get; set; }

        [JsonConstructor]
        public Position(string Name, float Salary)
        {
            name = Name;
            salary = Salary;
        }
        public Position(int Id,string Name, float Salary)
        {
            id = Id;
            name = Name;
            salary = Salary;
        }
    }
}
