using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Vacation
    {
        public int id { get; set; }
        public String start_otp { get; set; }
        public String end_otp { get; set; }
        public decimal coef { get; set; }
        public string type_otp { get; set; }
        public int employee_id { get; set; }

        [JsonConstructor]
        public Vacation(String startVac,String endVac,decimal Coef,string typeVac,int employee)
        {
            start_otp = startVac;
            end_otp = endVac;
            coef = Coef;
            type_otp = typeVac;
            employee_id = employee;
        }
        public Vacation(String startVac,String endVac,string typeVac,int employee)
        {
            start_otp = startVac;
            end_otp = endVac;
            type_otp = typeVac;
            employee_id = employee;
        }
        public Vacation(int ID,String startVac,String endVac,string typeVac)
        {
            start_otp = startVac;
            end_otp = endVac;
            type_otp = typeVac;
            id = ID;
        }
        public Vacation(int ID,String startVac, decimal Coef, String endVac,string typeVac)
        {
            start_otp = startVac;
            end_otp = endVac;
            type_otp = typeVac;
            coef = Coef;
            id = ID;
        }
    }
}
