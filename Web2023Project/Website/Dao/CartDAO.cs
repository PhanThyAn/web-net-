using System;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Net.Http;
using MySql.Data.MySqlClient;
using Web2023Project.libs;
using Web2023Project.Website.Model;
using Newtonsoft.Json;
using System.Text;

namespace Web2023Project.Website.Dao
{
    public class CartDAO
    {
        private readonly HttpClient httpClient;
        private readonly string api;

        public CartDAO()
        {
            this.httpClient = new HttpClient();
            this.api = "http://103.77.214.148/api/Dathang";
        }

        public bool InsertCart(Cart cart)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string cartJson = JsonConvert.SerializeObject(cart);

                HttpResponseMessage response = httpClient.PostAsync(api, new StringContent(cartJson, Encoding.UTF8, "application/json")).Result;

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
    }
}