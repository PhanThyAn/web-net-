using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web2023Project.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string TenSp { get; set; }
        public int IdNcc { get; set; }
        public string ThuongHieu { get; set; }
        public Double GiaDagiam { get; set; }
        public Double GiaGoc { get; set; }
        public int? SoLuong { get; set; }
        public string MauSanPham { get; set; }
        public string ManHinh { get; set; }
        public string HeDieuHanh { get; set; }
        public string Camera { get; set; }
        public string Chip { get; set; }
        public string Ram { get; set; }
        public string DungLuong { get; set; }
        public string Pin { get; set; }
        public string MoTa { get; set; }
        public string TenVietTat { get; set; }
        public string TrangThai { get; set; }

        public Products()
        {
            // Khởi tạo giá trị mặc định nếu cần
        }

        // Constructor với tham số
        public Products(int id, string tenSp, int idNcc, string thuongHieu, Double giaDagiam, Double giaGoc, int? soLuong, string mauSanPham, string manHinh, string heDieuHanh, string camera, string chip, string ram, string dungLuong, string pin, string moTa, string tenVietTat, string trangThai)
        {
            this.Id = id;
            this.TenSp = tenSp;
            this.IdNcc = idNcc;
            this.ThuongHieu = thuongHieu;
            this.GiaDagiam = giaDagiam;
            this.GiaGoc = giaGoc;
            this.SoLuong = soLuong;
            this.MauSanPham = mauSanPham;
            this.ManHinh = manHinh;
            this.HeDieuHanh = heDieuHanh;
            this.Camera = camera;
            this.Chip = chip;
            this.Ram = ram;
            this.DungLuong = dungLuong;
            this.Pin = pin;
            this.MoTa = moTa;
            this.TenVietTat = tenVietTat;
            this.TrangThai = trangThai;
        }
    }
}
