using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web2023Project.Models;

namespace Web2023Project.Website.Dao
{
    public class ProductDetailDAO
    {
        private readonly HttpClient httpClient;
        private readonly string api;

        public ProductDetailDAO()
        {
            this.httpClient = new HttpClient();
            this.api = "http://103.77.214.148/api/ShowSanpham";
        }

        public async Task<ProductShow> GetProductByShortenWord(string tenviettat)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}/{tenviettat}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                ProductShow product = JsonConvert.DeserializeObject<ProductShow>(jsonResponse);

                return product;
            }
            else
            {
                return null;
            }
        }

        public async Task<Image> GetProductImage(string tenviettat)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}/{tenviettat}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                Image image = JsonConvert.DeserializeObject<Image>(jsonResponse);

                return image;
            }
            else
            {
                return null;
            }
        }
    }
}