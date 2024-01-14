using System;
using Web2023Project.Models;

namespace Web2023Project.Model
{
    public class Binhluan
    {
        public int Id { get; set; }
        public Nguoidung IdNd { get; set; }
        public int IdSp { get; set; }
        public string Noidung { get; set; }
        public int Danhgia { get; set; }
        public string Ngaybinhluan { get; set; }
        public sbyte Trangthai { get; set; }

        public Binhluan()
        {
        }

        public Binhluan(int id, Nguoidung idNd, int idSp, string noidung, int danhgia, string ngaybinhluan, sbyte trangthai)
        {
            Id = id;
            IdNd = idNd;
            IdSp = idSp;
            Noidung = noidung;
            Danhgia = danhgia;
            Ngaybinhluan = ngaybinhluan;
            Trangthai = trangthai;
        }
    }
}