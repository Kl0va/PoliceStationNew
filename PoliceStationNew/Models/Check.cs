using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Check
    {
        public static string emcheck { get; set; }
        public static string email { get; set; }
        public static int ID { get; set; }
        public static bool reg { get; set; }
        public static string roleEmp {get;set;}
        public static Prize editPrize { get; set; }
        public static Vacation editVac { get; set; }
        public static Employee editEmp { get; set; }
        public static Position editPos { get; set; }
        public static Appeal confAppeal { get; set; }
    }
}
