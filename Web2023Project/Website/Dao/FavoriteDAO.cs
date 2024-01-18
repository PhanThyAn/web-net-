using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
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
        private List<Yeuthich> favoriteProducts;

        public FavouriteDAO()
        {
            favoriteProducts = new List<Yeuthich>();
        }

        public void AddFavoriteProduct(Yeuthich favoriteProduct)
        {
            // Kiểm tra xem sản phẩm đã được thêm vào danh sách yêu thích chưa
            if (!favoriteProducts.Any(fp => fp.IdSp == favoriteProduct.IdSp && fp.IdNd == favoriteProduct.IdNd))
            {
                favoriteProduct.Ngaytao = DateTime.Now;
                favoriteProducts.Add(favoriteProduct);
            }
        }

        public void RemoveFavoriteProduct(int userId, int productId)
        {
            favoriteProducts.RemoveAll(fp => fp.IdSp == productId && fp.IdNd == userId);
        }

        public List<Yeuthich> GetFavoriteProducts()
        {
            return favoriteProducts;
        }
    }
}