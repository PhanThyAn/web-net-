using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Web2023Project.libs;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;
using static System.Net.WebRequestMethods;

namespace Web2023Project.Website.Dao
{
    public class Update_User_DAO
    {
       
            public static bool edit(string taikhoan, string matkhau)
        {
            string sql;
            MySqlCommand cmd = null;
            MySqlConnection conn = null;
            try
            {
                sql = "UPDATE THANHVIEN SET MATKHAU=@matkhau WHERE TAIKHOAN=@taikhoan";
                conn = DBConnection.getConnection();
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@matkhau", MD5.ConvertToMD5(matkhau));
                cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                ReleaseResources.Release(conn, null, cmd);
            }
        }

      
         public static async Task<bool> checkCurrentPass(string current, string inputPass)
        {
            String api = "http://103.77.214.148/api/CheckPassword?pass="+inputPass+"&passhashed="+ current;
            using (HttpClient client = new HttpClient())
            {
                // gọi tới api 
                HttpResponseMessage response = await client.GetAsync(api);
                // nếu trả vè success
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
        public static async Task<bool> AddAddressInfoUser(Diachi diachi,string method)
        {
            String api = "http://103.77.214.148/api/Diachis";
            if (method.Equals("update"))
            {
                api = api +"/"+ diachi.Id;
            }
            var addressData = new
            {
               id= diachi.Id,
              idNd = diachi.IdNd,
              sdt= diachi.Sdt,
              ghichu = diachi.Ghichu,
              ten= diachi.Ten,
              xa= diachi.Xa,
              huyen= diachi.Huyen,
              tinh= diachi.Tinh,
              trangthai= diachi.Trangthai
            };
            using (HttpClient client = new HttpClient())
            {
                // Convert loginData to JSON string
                var jsonAddressData = Newtonsoft.Json.JsonConvert.SerializeObject(addressData);

                // Create StringContent with JSON data
                var content = new StringContent(jsonAddressData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                // Send POST request to the login API
                if (method.Equals("update"))
                {
                    response = await client.PutAsync(api, content);
                }
                else
                {
                    response = await client.PostAsync(api, content);
                }
                    

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
        public static async Task<bool> DeleteAddress(string id)
        {
            String api = "http://103.77.214.148/api/Diachis/"+id;
          
           
            using (HttpClient client = new HttpClient())
            {
                // Convert loginData to JSON string
               
                // Create StringContent with JSON data            
                HttpResponseMessage response = null;
                    response = await client.DeleteAsync(api);

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
        public static async Task<bool> updateInfoUser(Nguoidung member)
        {
            String api = "http://103.77.214.148/api/Nguoidungs/" + member.Id;
            // Dữ liệu đăng nhập
            var registerData = new
            {
                id = member.Id,
                ten = member.Ten,
                sdt = member.Sdt,
                gioitinh = member.Gioitinh,
                matkhau = member.Matkhau,
                email = member.Email,
                anhdaidien = member.Anhdaidien,
                quyen = member.Quyen,
                googleId = member.GoogleId,
                facebookId = member.FacebookId,
                ngaytao = member.Ngaytao,
                ngaycapnhat = member.Ngaycapnhat,
                trangthai = member.Trangthai
            };
            using (HttpClient client = new HttpClient())
            {
                // Convert loginData to JSON string
                var jsonLoginData = Newtonsoft.Json.JsonConvert.SerializeObject(registerData);

                // Create StringContent with JSON data
                var content = new StringContent(jsonLoginData, Encoding.UTF8, "application/json");
                // Send POST request to the login API
                HttpResponseMessage response = await client.PutAsync(api, content);

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