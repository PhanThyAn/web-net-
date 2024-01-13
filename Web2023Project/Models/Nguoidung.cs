using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web2023Project.Models;

namespace Web2023Project.Models
{
    public class Nguoidung
    {
        public int Id { get; set; }

        public string Ten { get; set; }

        public string Sdt { get; set; }

        public int Gioitinh { get; set; }

        public string Email { get; set; }

        public string Matkhau { get; set; }

        public string Anhdaidien { get; set; }

        public int Quyen { get; set; }

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

        public Nguoidung()
        {
            // Khởi tạo giá trị mặc định nếu cần
        }

        
    }
}
