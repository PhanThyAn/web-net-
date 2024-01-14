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