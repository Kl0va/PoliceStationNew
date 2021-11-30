using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Models
{
    class Employee
    {
        public int id { get; set; }
        public string first { get; set; }
        public string second { get; set; }
        public string middle { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string date_rec { get; set; }
        public string date_dis { get; set; }
        public int position_id { get; set; }
        public string role_id { get; set; }
        public int formation_id { get; set; }

        [JsonConstructor]
        public Employee(string First,string Second,string Middle,string Series,string Number,string Email,string Password,string Date_rec,int Position_id,string Role_id,int Formation_id)
        {
            first = First;
            second = Second;
            middle = Middle;
            series = Series;
            number = Number;
            email = Email;
            password = Password;
            date_rec = Date_rec;
            position_id = Position_id;
            role_id = Role_id;
            formation_id = Formation_id;
        }

        public Employee(string First, string Second,string Middle, string Series, string Number, string Email,string Password, string Date_rec, string Date_dis, int Position_id, string Role_id, int Formation_id)
        {
            first = First;
            second = Second;
            middle = Middle;
            series = Series;
            number = Number;
            email = Email;
            password = Password;
            date_rec = Date_rec;
            date_dis = Date_dis;
            position_id = Position_id;
            role_id = Role_id;
            formation_id = Formation_id;
        }
        public Employee(int Id,string First, string Second,string Middle, string Series, string Number, string Email,string Password, string Date_rec, string Date_dis, int Position_id, string Role_id, int Formation_id)
        {
            id = Id;
            first = First;
            second = Second;
            middle = Middle;
            series = Series;
            number = Number;
            email = Email;
            password = Password;
            date_rec = Date_rec;
            date_dis = Date_dis;
            position_id = Position_id;
            role_id = Role_id;
            formation_id = Formation_id;
        }
    }
}
