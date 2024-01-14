using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web2023Project.Models
{
    public class Sanphams
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
        public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

        public virtual ICollection<Chitietgiohang> Chitietgiohangs { get; set; } = new List<Chitietgiohang>();

        public virtual ICollection<Hinhanh> Hinhanhs { get; set; } = new List<Hinhanh>();

        public virtual Nhacungcap IdNccNavigation { get; set; }

        public Sanphams()
        {
            // Khởi tạo giá trị mặc định nếu cần
        }

    }
}
