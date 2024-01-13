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
            String api = "http://103.77.214.148/api/ShowSanpham";
            using (HttpClient client = new HttpClient())
            {
                // gọi tới api 
                HttpResponseMessage response = await client.GetAsync(api);
                // nếu trả vè success
                if (response.IsSuccessStatusCode)
                {
                    // đọc kết quả trả về
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    // chuyển dữ liệu thành 1 mảng obj
                    JArray jsonArray = JArray.Parse(jsonResponse);
                    List<Sanphams> resultList = new List<Sanphams>();
                    // duyệt qua mảng obj
                    foreach (JObject jsonObject in jsonArray)
                    {
                       
                        // Lấy giá trị của thuộc tính TenSp
                        string tenSanPham = jsonObject["thongtin"]?["tenSp"]?.ToString();
                        if (tenSanPham != null && tenSanPham.ToLower().Contains(input.ToLower()))
                        {
                            string gia = jsonObject["thongtin"]?["giagoc"]?.ToString();
                            string giaGiam = jsonObject["thongtin"]?["giadagiam"]?.ToString();
                            string id = jsonObject["thongtin"]?["id"]?.ToString();
                            string thuongHieu = jsonObject["thongtin"]?["thuonghieu"]?.ToString();
                            string soLuong = jsonObject["thongtin"]?["soluong"]?.ToString();
                            string mausanpham = jsonObject["thongtin"]?["mausanpham"]?.ToString();
                            string manhinh = jsonObject["thongtin"]?["manhinh"]?.ToString();
                            string hedieuhanh = jsonObject["thongtin"]?["hedieuhanh"]?.ToString();
                            string camera = jsonObject["thongtin"]?["camera"]?.ToString();
                            string chip = jsonObject["thongtin"]?["chip"]?.ToString();
                            string ram = jsonObject["thongtin"]?["ram"]?.ToString();
                            string dungluong = jsonObject["thongtin"]?["dungluong"]?.ToString();
                            string pin = jsonObject["thongtin"]?["pin"]?.ToString();
                            string mota = jsonObject["thongtin"]?["mota"]?.ToString();
                            string tenviettat = jsonObject["tenviettat"]?.ToString();
                            string trangthai = jsonObject["thongtin"]?["trangthai"]?.ToString();
                            string imageUrl = jsonObject["thongtin"]?["hinhanhs"]?[0]?["url"]?.ToString();
                            string imageId = jsonObject["thongtin"]?["hinhanhs"]?[0]?["id"]?.ToString();
                            string imageIdSp = jsonObject["thongtin"]?["hinhanhs"]?[0]?["idSp"]?.ToString();
                            Hinhanh h = new Hinhanh();
                            h.Id = Int32.Parse(imageId);
                            h.Url = imageUrl;
                            h.IdSp = Int32.Parse(imageIdSp);
                            List<Hinhanh> listImage = new List<Hinhanh>();
                            listImage.Add(h);
                            Console.WriteLine("i" + id);

                            // tạo ra sản phẩm và set dữ liệu vào sp đó
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
                            p.Hinhanhs = listImage;
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