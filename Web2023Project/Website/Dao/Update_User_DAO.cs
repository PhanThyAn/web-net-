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

        public static bool checkCurrentPass(string current, string inputPass)
        {
            return current.Equals(MD5.ConvertToMD5(inputPass));
        }

        public static bool updateInfoUser(Member member)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            String sql;
            try {
            sql = "UPDATE THANHVIEN SET HOTEN= @hoten, GIOITINH=@gioitinh, EMAIL=@email, SODIENTHOAI=@sodienthoai, DIACHI=@diachi WHERE TAIKHOAN=@taikhoan";

            conn = DBConnection.getConnection();
            conn.Open();
            cmd= new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@hoten", member.Name);
            cmd.Parameters.AddWithValue("@gioitinh", member.Gender);
            cmd.Parameters.AddWithValue("@email", member.Email);
            cmd.Parameters.AddWithValue("@sodienthoai", member.Phone);
            cmd.Parameters.AddWithValue("@diachi", member.Address);
            cmd.Parameters.AddWithValue("@taikhoan", member.UserName);
            cmd.ExecuteNonQuery();
            
            return true;
        } catch (Exception e) {
           Console.WriteLine(e.Message);
            return false;
        } finally {
                ReleaseResources.Release(conn, null, cmd);
        }
    }
}

}