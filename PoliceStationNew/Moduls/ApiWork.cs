using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using PoliceStationNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceStationNew.Moduls
{
    class ApiWork
    {
        private static string baseUrl = "https://police-api-russia.herokuapp.com";

        public static async Task<List<Statement>> GetAllStatements()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/statement").GetStringAsync();

            List<Statement> statements = JsonConvert.DeserializeObject<List<Statement>>(response);
            return statements;
        }
        public static async Task<List<Appeal>> GetAllAppeals()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/appeal").GetStringAsync();

            List<Appeal> appeals = JsonConvert.DeserializeObject<List<Appeal>>(response);
            return appeals;
        }

        public static async Task<List<Declarant>> GetAllDeclarants()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/declarant").GetStringAsync();

            List<Declarant> declarants = JsonConvert.DeserializeObject<List<Declarant>>(response);
            return declarants;
        }
        public static async Task<List<Employee>> GetAllEmployees()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/employee").GetStringAsync();

            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(response);
            return employees;
        }
        public static async Task<List<Prize>> GetAllPrizes()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/prize").GetStringAsync();

            List<Prize> prizes = JsonConvert.DeserializeObject<List<Prize>>(response);
            return prizes;
        }
        public static async Task<List<Position>> GetAllPositions()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/position").GetStringAsync();

            List<Position> positions = JsonConvert.DeserializeObject<List<Position>>(response);
            return positions;
        }
        public static async Task<List<Vacation>> GetAllVacations()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/vacation").GetStringAsync();

            List<Vacation> prizes = JsonConvert.DeserializeObject<List<Vacation>>(response);
            return prizes;
        } 
        public static async Task<List<Role>> GetAllRoles()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/role").GetStringAsync();

            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(response);
            return roles;
        }
        public static async Task<List<Formation>> GetAllFormations()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/formation").GetStringAsync();

            List<Formation> formations = JsonConvert.DeserializeObject<List<Formation>>(response);
            return formations;
        }
        public static async Task<List<Article>> GetAllArticles()
        {
            var response = await $@"{baseUrl}".AppendPathSegment("/article").GetStringAsync();

            List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(response);
            return articles;
        }
    }
}
