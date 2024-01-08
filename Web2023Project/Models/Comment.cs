﻿using System;
using MySql.Data.MySqlClient;

namespace Web2023Project.Model
{
    public class Comment
    {
        private int id;
        private String name;
        private String content;
        private int productID;
        private String product;
        private String commentDate;

        public Comment()
        {
        }

        public Comment(int id, string name, string content, int productId, string product, string commentDate)
        {
            this.id = id;
            this.name = name;
            this.content = content;
            productID = productId;
            this.product = product;
            this.commentDate = commentDate;
        }
        public int Id
        {
            get => id;
            set => id = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Content
        {
            get => content;
            set => content = value;
        }

        public int ProductId
        {
            get => productID;
            set => productID = value;
        }

        public string Product
        {
            get => product;
            set => product = value;
        }

        public string CommentDate
        {
            get => commentDate;
            set => commentDate = value;
        }

        public Comment GetComment(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("hoten");
            Content = reader.GetString("noidung");
            Product = reader.GetString("sanpham");
            CommentDate = reader.GetString("ngaybinhluan");
            return this;
        }
    }
}