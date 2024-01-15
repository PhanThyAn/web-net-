using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Web2023Project.libs;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;
using Binhluan = Web2023Project.Models.Binhluan;

namespace Web2023Project.Website.Dao
{
    public class CommentDAO
    {
        private readonly HttpClient httpClient;
        private readonly string api;

        public CommentDAO()
        {
            this.httpClient = new HttpClient();
            this.api = "http://103.77.214.148/api/Binhluans";
        }

        public async Task<List<Binhluan>> LoadComment()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                List<Binhluan> comments = JsonConvert.DeserializeObject<List<Binhluan>>(jsonResponse);

                return comments;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> InsertCMT(Binhluan comment)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(api);

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string commentJson = JsonConvert.SerializeObject(comment);

                    HttpResponseMessage response = await client.PostAsync(api, new StringContent(commentJson, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        [HttpGet]
        public async Task<List<Binhluan>> LoadCMT(int productID)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}/{productID}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<Binhluan> comments = JsonConvert.DeserializeObject<List<Binhluan>>(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                return comments;
            }
            else
            {
                return null;
            }
        }
    }
}