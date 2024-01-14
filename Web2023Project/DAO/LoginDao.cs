﻿using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
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
                        Nguoidung n = new Nguoidung();
                        n.Id = Int32.Parse(id);
                        n.Ten = ten;
                        n.Matkhau = pass;
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
            // Dữ liệu đăng nhập
            var registerData = new
            {
                ten = name,
                sdt = phone,
                gioitinh = gender,
                matkhau = password,
                email = emailRegister,
                trangthai = 0
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