using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web2023Project.Models;

namespace Web2023Project.Website.Dao
{
    public class OrderDAO
    {
        private readonly HttpClient httpClient;
        private readonly string api;

        public OrderDAO()
        {
            this.httpClient = new HttpClient();
            this.api = "http://103.77.214.148/api/Donhangs";
        }

        public async Task<List<Donhang>> GetAllOrders()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Donhang> orders = JsonConvert.DeserializeObject<List<Donhang>>(jsonResponse);

                return orders;
            }
            else
            {
                return null;
            }
        }

        public async Task<Donhang> GetOrderById(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Donhang order = JsonConvert.DeserializeObject<Donhang>(jsonResponse);

                return order;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Donhang>> GetOrderByUserId(int userId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}/Nguoidung/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Donhang> orders = JsonConvert.DeserializeObject<List<Donhang>>(jsonResponse);

                return orders;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Donhang>> GetOrderDetailById(int userId, int orderId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{api}/Nguoidung/{userId}?orderId={orderId}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Donhang> orders = JsonConvert.DeserializeObject<List<Donhang>> (jsonResponse);

                return orders;
            }
            else
            {
                return null;
            }
        }
    }
}