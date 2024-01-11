using System;
using System.Collections.Generic;
namespace Web2023Project.Models
{
    public partial class Nguoidung
    {
        public int Id { get; set; }

        public string Ten { get; set; }

        public string Sdt { get; set; }

        public ulong Gioitinh { get; set; }

        public string Email { get; set; }

        public string Matkhau { get; set; }

        public string Anhdaidien { get; set; }

        public sbyte? Quyen { get; set; }

        public string GoogleId { get; set; }

        public string FacebookId { get; set; }

        public DateTime? Ngaytao { get; set; }

        public DateTime? Ngaycapnhat { get; set; }

        public sbyte? Trangthai { get; set; }

        public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

        public virtual ICollection<Chitietgiohang> Chitietgiohangs { get; set; } = new List<Chitietgiohang>();

        public virtual ICollection<Diachi> Diachis { get; set; } = new List<Diachi>();

        public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}