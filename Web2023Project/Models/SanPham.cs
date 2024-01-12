using System;
using System.Collections.Generic;

namespace Web2023Project.Models
{
	public partial class Sanpham
	{
		public int Id { get; set; }

		public string TenSp { get; set; }

		public int? IdNcc { get; set; }

		public string Thuonghieu { get; set; }

		public double? Giadagiam { get; set; }

		public double? Giagoc { get; set; }

		public int? Soluong { get; set; }

		public string Mausanpham { get; set; }

		public string Manhinh { get; set; }

		public string Hedieuhanh { get; set; }

		public string Camera { get; set; }

		public string Chip { get; set; }

		public string Ram { get; set; }

		public string Dungluong { get; set; }

		public string Pin { get; set; }

		public string Mota { get; set; }

		public string Tenviettat { get; set; }

		public sbyte? Trangthai { get; set; }

		public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

		public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

		public virtual ICollection<Chitietgiohang> Chitietgiohangs { get; set; } = new List<Chitietgiohang>();

		public virtual ICollection<Hinhanh> Hinhanhs { get; set; } = new List<Hinhanh>();

		public virtual Nhacungcap IdNccNavigation { get; set; }
		public Sanpham(int id, string tenSp, int? idNcc, string thuonghieu, double? giadagiam,
			   double? giagoc, int? soluong, string mausanpham, string manhinh, string hedieuhanh,
			   string camera, string chip, string ram, string dungluong, string pin, string mota,
			   string tenviettat, sbyte? trangthai)
		{
			Id = id;
			TenSp = tenSp;
			IdNcc = idNcc;
			Thuonghieu = thuonghieu;
			Giadagiam = giadagiam;
			Giagoc = giagoc;
			Soluong = soluong;
			Mausanpham = mausanpham;
			Manhinh = manhinh;
			Hedieuhanh = hedieuhanh;
			Camera = camera;
			Chip = chip;
			Ram = ram;
			Dungluong = dungluong;
			Pin = pin;
			Mota = mota;
			Tenviettat = tenviettat;
			Trangthai = trangthai;
		}
	}
}