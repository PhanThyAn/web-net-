using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Web;
using Web2023Project.libs;
using MySql.Data.MySqlClient;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;
using Web2023Project.Website.Model;

namespace Web2023Project.DAO
{
    public class LogDao
    {
        public static bool add(Log log)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            try
            {
                string sql = "INSERT INTO LOG(thongbao, capdo, taikhoan, diachi, ip, ngaythuchien) VALUES (@thongbao, @capdo, @taikhoan, @diachi, @ip, now())";
                connection = DBConnection.getConnection();
                connection.Open();
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@thongbao", log.Noidung);
                cmd.Parameters.AddWithValue("@capdo", log.Capdo);
                cmd.Parameters.AddWithValue("@taikhoan", log.IdNd);
                cmd.Parameters.AddWithValue("@diachi", log.Nguon);
                cmd.Parameters.AddWithValue("@ip", log.Ip);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

            }
            finally
            {
                ReleaseResources.Release(connection, null, cmd);
            }

        }
        public static string GetIPAddress()
        {
           string ipaddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }

        public static List<Role> loadRolesByUserName(string username)
        { 
            MySqlConnection connection = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            List<Role> roles= new List<Role>();
            string sql = "SELECT control,action FROM resource,userrole  WHERE  userrole.username=@username and userrole.idResource = resource.id and resource.active =1";
            try
            {
                connection = DBConnection.getConnection();
                connection.Open();
                cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role(reader.GetString("control"), reader.GetString("action")));
                    }
                }
                return roles;
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
        public static void LogBase(Log log, string message, string address)
        {
            log.Noidung = message;
            log.Nguon = address;
            //log.IdNd = (int?)(HttpContext.Current.Session["memberLogin"] as Nguoidung);
            log.Ip = GetIPAddress();
            add(log);
        }
        public static void FAILEDLOGIN(string message, string address)
        {
            Log log =new Log();
            log.Noidung = message;
            //log.IdNd= (int?)new Nguoidung();
            log.Capdo = Level.INFOR.ToString();
            log.Nguon = address;
            log.Ip = GetIPAddress();
            add(log);
        }

        public static void INFO(string message, string address)
        {
            Log log= new Log();
            log.Capdo = Level.INFOR.ToString();
            LogBase(log, message, address);
        }

        public static void WARNING(string message, string address)
        {
            Log log= new Log();
            log.Capdo = Level.WARNING.ToString();
            LogBase(log, message, address);
        }
        public static void ALERT(string message, string address)
        {
            Log log= new Log();
            log.Capdo = Level.ALERT.ToString();
            LogBase(log, message, address);
        }
    }
}