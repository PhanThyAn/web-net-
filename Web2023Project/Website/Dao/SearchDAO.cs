using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web2023Project.libs;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;

namespace Web2023Project.Website.Dao
{
    public class SearchDAO
    {
        public static List<Product> Search(String input)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            List<Product> products = new List<Product>();
            try
            {
                string sql = "SELECT * FROM SANPHAM WHERE TRANGTHAI>0 AND TENSANPHAM LIKE " + "'%" + input +
                             "%' LIMIT 6";
                connection = DBConnection.getConnection();
                connection.Open();
                cmd = new MySqlCommand(sql, connection);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        products.Add(new Product().GetProduct(reader));
                    }
                }

                return products.Count != 0 ? products : null;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                ReleaseResources.Release(connection, reader, cmd);
            }
        }

        public static List<ProductDetail> SearchKey(String key)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            List<ProductDetail> productDetails = new List<ProductDetail>();
            try
            {
                string sql =
                    "SELECT * FROM SANPHAM AS SP JOIN CHITIETSANPHAM AS CT WHERE SP.MASANPHAM=CT.MASANPHAM AND TRANGTHAI>0 AND SP.TENSANPHAM LIKE " +
                    "'%" + key + "%'";
                connection = DBConnection.getConnection();
                connection.Open();
                cmd = new MySqlCommand(sql, connection);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productDetails.Add(new ProductDetail().GetProductDetail(reader));
                    }
                }

                return productDetails.Count != 0 ? productDetails : null;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                ReleaseResources.Release(connection, reader, cmd);
            }
        }

        public static async Task<List<Sanphams>> SeachTest(string input)
        {
            String api = "http://103.77.214.148/api/Sanphams";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(api);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    JArray jsonArray = JArray.Parse(jsonResponse);
                    List<Sanphams> resultList = new List<Sanphams>();
                    foreach (JObject jsonObject in jsonArray)
                    {
                       
                        // Lấy giá trị của thuộc tính TenSp
                        string tenSanPham = jsonObject["tenSp"]?.ToString();
                        if (tenSanPham != null && tenSanPham.ToLower().Contains(input.ToLower()))
                        {
                            string gia = jsonObject["giagoc"]?.ToString();
                            string giaGiam = jsonObject["giadagiam"]?.ToString();
                            string id = jsonObject["id"]?.ToString();
                            string thuongHieu = jsonObject["thuonghieu"]?.ToString();
                            string soLuong = jsonObject["soluong"]?.ToString();
                            string mausanpham = jsonObject["mausanpham"]?.ToString();
                            string manhinh = jsonObject["manhinh"]?.ToString();
                            string hedieuhanh = jsonObject["hedieuhanh"]?.ToString();
                            string camera = jsonObject["camera"]?.ToString();
                            string chip = jsonObject["chip"]?.ToString();
                            string ram = jsonObject["ram"]?.ToString();
                            string dungluong = jsonObject["dungluong"]?.ToString();
                            string pin = jsonObject["pin"]?.ToString();
                            string mota = jsonObject["mota"]?.ToString();
                            string tenviettat = jsonObject["tenviettat"]?.ToString();
                            string trangthai = jsonObject["trangthai"]?.ToString();
                            Console.WriteLine("i" + id);

                            // In ra màn hình
                            Sanphams p = new Sanphams();
                            p.TenSp = tenSanPham;
                            p.Id = Int32.Parse(id);
                            p.GiaGoc = Double.Parse(gia);
                            p.GiaDagiam = Double.Parse(giaGiam);
                            p.ThuongHieu = thuongHieu;
                            try
                            {
                                p.SoLuong = Int32.Parse(soLuong);
                            }
                            catch (Exception ex)
                            {
                                p.SoLuong = 0;
                            }

                            p.MauSanPham = mausanpham;
                            p.ManHinh = manhinh;
                            p.HeDieuHanh = hedieuhanh;
                            p.Camera = camera;
                            p.Chip = chip;
                            p.Ram = ram;
                            p.DungLuong = dungluong;
                            p.Pin = pin;
                            p.MoTa = mota;
                            p.TenVietTat = tenviettat;
                            p.TrangThai = trangthai;
                            resultList.Add(p);
                        }
                        // In các thuộc tính khác tương tự

                    }
                    return resultList;

                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            return null;
        }

    }

}