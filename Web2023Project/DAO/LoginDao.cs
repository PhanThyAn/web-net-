using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Web2023Project.libs;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;

namespace Web2023Project.Dao
{
    public class LoginDao
    {
        public static async Task<Nguoidung> getInforFromChangePass(string email,string sdt)
        {
            String api = "http://103.77.214.148/api/CheckMailSdt?email="+ email +"&sdt="+ sdt;
            using (HttpClient client = new HttpClient())
            {
                // gọi tới api 
                HttpResponseMessage response = await client.GetAsync(api);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    JObject jsonObject = JObject.Parse(jsonResponse);
                    string id = jsonObject["id"]?.ToString();
                    string ten = jsonObject["ten"]?.ToString();                
                    string gioiTinh = jsonObject["gioitinh"]?.ToString();                  
                    string pass = jsonObject["matkhau"]?.ToString();
                    string quyen = jsonObject["quyen"]?.ToString();
                    string anhdaidien = jsonObject["anhdaidien"]?.ToString();
                    string googleId = jsonObject["googleId"]?.ToString();
                    string facebookId = jsonObject["facebookId"]?.ToString();
                    string ngaytao = jsonObject["ngaytao"]?.ToString();
                    DateTime ngayCapNhat = DateTime.Now;
                    Task<List<Diachi>> list = getDiaChi(id);
                    List<Diachi> listDC = await list;
                    Nguoidung n = new Nguoidung();
                    n.Id = Int32.Parse(id);
                    n.Ten = ten;
                    n.Matkhau = pass;
                    n.Sdt = sdt;
                    n.Gioitinh = (ulong)int.Parse(gioiTinh);
                    n.Diachis = listDC;
                    n.Email = email;
                    n.Quyen = 1;
                    n.Anhdaidien = anhdaidien;
                    n.GoogleId = googleId;
                    n.FacebookId = facebookId;                 
                    n.Ngaycapnhat = ngayCapNhat;
                    return n;
                }
                else
                {
                    return null;
                }
               
             
            }
            return null;
        }
        public static async Task<List<Diachi>> getDiaChi(string id)
        {
            String api = "http://103.77.214.148/api/Diachis/Nguoidung/" + id;
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
                    List<Diachi> resultList = new List<Diachi>();
                    // duyệt qua mảng obj
                    foreach (JObject jsonObject in jsonArray)
                    {
                            string idDC = jsonObject["id"]?.ToString();
                            string idNd = jsonObject["idNd"]?.ToString();
                            string sdt = jsonObject["sdt"]?.ToString();                    
                            string ten = jsonObject["ten"]?.ToString();
                            string ghichu = jsonObject["ghichu"]?.ToString();
                            string xa = jsonObject["xa"]?.ToString();
                            string huyen = jsonObject["huyen"]?.ToString();
                            string tinh = jsonObject["tinh"]?.ToString();
                            string trangthai = jsonObject["trangthai"]?.ToString();
                          
                        
                            Diachi p = new Diachi();
                            p.Id = Int32.Parse(idDC);
                            p.IdNd = Int32.Parse(idNd);
                            p.Sdt = sdt;
                            p.Ten = ten;
                            p.Ghichu = ghichu;                      
                            p.Xa = xa;
                            p.Huyen = huyen;
                            p.Tinh = tinh;
                            p.Trangthai = sbyte.Parse(trangthai);

                            resultList.Add(p);
                        }
                    // In các thuộc tính khác tương tự
                    return resultList;
                }                     
            }
            return null;
        }
        public static async Task<List<Nhacungcap>> getNhaCungCap()
        {
            String api = "http://103.77.214.148/api/Nhacungcaps";
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
                    List<Nhacungcap> resultList = new List<Nhacungcap>();
                    // duyệt qua mảng obj
                    foreach (JObject jsonObject in jsonArray)
                    {
                        string id = jsonObject["id"]?.ToString();
                        string tenNcc = jsonObject["tenNcc"]?.ToString();
                        string diachi = jsonObject["diachi"]?.ToString();
                        string trangthai = jsonObject["trangthai"]?.ToString();
                       

                        Nhacungcap p = new Nhacungcap();
                        p.Id = Int32.Parse(id);
                        p.TenNcc = tenNcc;
                        p.Diachi = diachi;
                        sbyte status = 0;
                        if(trangthai != null) { 
                        p.Trangthai =  sbyte.Parse(trangthai);
                        }
                        else
                        {
                           p.Trangthai = status;
                        }
                        resultList.Add(p);
                    }
                    // In các thuộc tính khác tương tự
                    return resultList;
                }
            }
            return null;
        }
        public static async Task<Nguoidung> login(string taikhoan, string matkhau)
            {
                String api = "http://103.77.214.148/api/Login";
                // Dữ liệu đăng nhập
                var loginData = new
                {
                    sdt = taikhoan,
                    matkhau = matkhau
                };
                using (HttpClient client = new HttpClient())
                {
                    // Convert loginData to JSON string
                    var jsonLoginData = Newtonsoft.Json.JsonConvert.SerializeObject(loginData);

                    // Create StringContent with JSON data
                    var content = new StringContent(jsonLoginData, Encoding.UTF8, "application/json");
                    // Send POST request to the login API
                    HttpResponseMessage response = await client.PostAsync(api, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        JObject jsonObject = JObject.Parse(jsonResponse);
                        string id = jsonObject["id"]?.ToString();
                        string ten = jsonObject["ten"]?.ToString();
                        string sdt = jsonObject["sdt"]?.ToString();
                        string gioiTinh = jsonObject["gioitinh"]?.ToString();
                        string email = jsonObject["email"]?.ToString();
                        string pass = jsonObject["matkhau"]?.ToString();
                        string quyen = jsonObject["quyen"]?.ToString();

                        Task<List<Diachi>> list = getDiaChi(id);
                        List<Diachi> listDC = await list;
                        Nguoidung n = new Nguoidung();
                        n.Id = Int32.Parse(id);
                        n.Ten = ten;
                        n.Matkhau = pass;
                        n.Sdt = sdt;
                        n.Gioitinh = (ulong)int.Parse(gioiTinh);
                        n.Diachis = listDC;
                        n.Email = email;
                    n.Quyen = 1;
                         return n;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
        public static Member checkLogin(string userName, string password)
        {
            MySqlDataReader reader = null;
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            string sql = "SELECT * FROM THANHVIEN WHERE TAIKHOAN= @taikhoan AND MATKHAU= @matkhau";
            try
            {
                connection = DBConnection.getConnection();
                connection.Open();
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@taikhoan", userName);
                cmd.Parameters.AddWithValue("@matkhau", MD5.ConvertToMD5(password));
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        return new Member().GetMember(reader);
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                ReleaseResources.Release(connection, reader, cmd);
            }
        }
        public static async Task<Nguoidung> register( string password, string name, int gender, string emailRegister, string phone)
        {
            String api = "http://103.77.214.148/api/Register";
            DateTime create = DateTime.Now;
            String ngay = create.ToString();
            // Dữ liệu đăng nhập
            var registerData = new
            {
                ten = name,
                sdt = phone,
                gioitinh = gender,
                matkhau = password,
                email = emailRegister,
                trangthai = 0,
                ngaytao = ngay
            };
            using (HttpClient client = new HttpClient())
            {
                // Convert loginData to JSON string
                var jsonLoginData = Newtonsoft.Json.JsonConvert.SerializeObject(registerData);

                // Create StringContent with JSON data
                var content = new StringContent(jsonLoginData, Encoding.UTF8, "application/json");
                // Send POST request to the login API
                HttpResponseMessage response = await client.PostAsync(api, content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    JObject jsonObject = JObject.Parse(jsonResponse);
                    string id = jsonObject["id"]?.ToString();
                    string ten = jsonObject["ten"]?.ToString();
                    string sdt = jsonObject["sdt"]?.ToString();
                    string gioiTinh = jsonObject["gioitinh"]?.ToString();
                    string email = jsonObject["email"]?.ToString();
                    string matKhau = jsonObject["matkhau"]?.ToString();
                    string quyen = jsonObject["quyen"]?.ToString();
                    Nguoidung n = new Nguoidung();
                    n.Id = Int32.Parse(id);
                    n.Ten = ten;
                    n.Matkhau = matKhau;
                    n.Sdt = sdt;
                    n.Gioitinh = (ulong)int.Parse(gioiTinh);
                    n.Email = email;
                    n.Quyen = 1;
                    return n;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}