using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web2023Project.Models;

namespace Web2023Project.Website.Dao
{
    public class ProductDAO
    {
        private readonly HttpClient httpClient;
        private readonly string api;

        public ProductDAO()
        {
            this.httpClient = new HttpClient();
            this.api = "http://103.77.214.148/api/Sanphams";
        }

        public async Task<List<Products>> GetAllProducts()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                List<Products> products = JsonConvert.DeserializeObject<List<Products>>(jsonResponse);

                return products;

            }
            else
            {
                return null;
            }
        }

        public Products GetProductById(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync($"{api}/{id}").Result;

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            Products product = JsonConvert.DeserializeObject<Products>(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                return product;

            }
            else
            {
                return null;
            }
        }

        public List<Products> GetNewProducts(int count = 8)
        {
            HttpResponseMessage response = httpClient.GetAsync($"{api}?count={count}").Result;

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<Products> newProducts = JsonConvert.DeserializeObject<List<Products>>(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                return newProducts;

            }
            else
            {
                return null;
            }
        }

        public List<Products> GetHotProducts(int count = 8)
        {
            HttpResponseMessage response = httpClient.GetAsync($"{api}?count={count}").Result;

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<Products> hotProducts = JsonConvert.DeserializeObject<List<Products>>(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                return hotProducts;

            }
            else
            {
                return null;
            }
        }

        public List<Products> GetSaleProducts(int count = 8)
        {
            HttpResponseMessage response = httpClient.GetAsync($"{api}?count={count}").Result;

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<Products> saleProducts = JsonConvert.DeserializeObject<List<Products>>(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                return saleProducts;

            }
            else
            {
                return null;
            }
        }
    }
}