using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Web;
using Web2023Project.libs;
using MySql.Data.MySqlClient;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;


namespace Web2023Project.DAO
{
    public class LogDao
    {
        public static bool add(Log log)
        {



            return false;

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
          
        }
        public static void FAILEDLOGIN(string message, string address)
        {
           
        }

        public static void INFO(string message, string address)
        {
           
        }

        public static void WARNING(string message, string address)
        {
           
        }
        public static void ALERT(string message, string address)
        {
           
        }
    }
}