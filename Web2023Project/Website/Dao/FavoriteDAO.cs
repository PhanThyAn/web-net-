using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Security.Permissions;
using System.Text;
using MySql.Data.MySqlClient;
using Web2023Project.libs;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;

namespace Web2023Project.Website.Dao
{
    public class FavouriteDAO
    {
        private string connectionString = "Data Source=D:\\LT .NET_NLU\\web-net-\\Web2023Project\\spyeuthich.db;Version=3;";

        public void AddFavoriteProduct(Yeuthich favoriteProduct)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Yeuthich (IdNd, IdSp, Ngaytao) VALUES (@IdNd, @IdSp, @Ngaytao)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdNd", favoriteProduct.IdNd);
                    command.Parameters.AddWithValue("@IdSp", favoriteProduct.IdSp);
                    command.Parameters.AddWithValue("@Ngaytao", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddProduct(Sanphams product)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Sanpham (Id, TenSp, GiaGoc, GiaDagiam) VALUES (@Id, @TenSp, @GiaGoc, @GiaDagiam)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@TenSp", product.TenSp);
                    command.Parameters.AddWithValue("@GiaGoc", product.GiaGoc);
                    command.Parameters.AddWithValue("@GiaDagiam", product.GiaDagiam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Sanphams> GetFavoriteProducts(int userId)
        {
            List<Sanphams> favoriteProducts = new List<Sanphams>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT sp.* FROM Sanpham sp " +
                                          "INNER JOIN Yeuthich yt ON sp.Id = yt.IdSp " +
                                          "WHERE yt.IdNd = @IdNd";
                    command.Parameters.AddWithValue("@IdNd", userId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sanphams product = new Sanphams
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                TenSp = reader["TenSp"].ToString(),
                                GiaGoc = Convert.ToInt32(reader["GiaGoc"]),
                                GiaDagiam = Convert.ToInt32(reader["GiaDagiam"]),
                            };
                            favoriteProducts.Add(product);
                        }
                    }
                }
            }

            return favoriteProducts;
        }
    }
}