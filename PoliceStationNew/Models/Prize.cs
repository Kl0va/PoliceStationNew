using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Prize
    {
        public int id { get; set; }
        public string coef_prem { get; set; }
        public int employee_id { get; set; }

        [JsonConstructor]
        public Prize(string Coef_prem,int employee)
        {
            coef_prem = Coef_prem;
            employee_id = employee;
        }
        public Prize(int Id,string Coef_prem)
        {
            coef_prem = Coef_prem;
            id = Id;
        }
    }
}
