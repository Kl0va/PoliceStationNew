using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Statement
    {
        public int id { get; set; }
        public string appeal_id { get; set; }
        public int employee_id { get; set; }
        public int article_id { get; set; }

        public Statement(string appeal, int employee, int article)
        {
            appeal_id = appeal;
            employee_id = employee;
            article_id = article;
        }
    }
}
