using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Web2023Project.Models
{
	public partial class Chitietgiohang
	{
		public int Id { get; set; }
		public int IdNd { get; set; }

		public int IdSp { get; set; }

		public int Soluong { get; set; }

		public sbyte Trangthai { get; set; }

		public virtual Nguoidung IdNdNavigation { get; set; }

		public virtual Sanpham IdSpNavigation { get; set; }
		public Chitietgiohang()
		{
		}

		public Chitietgiohang(int idNd, int idSp, int soluong, sbyte trangthai)
		{
			IdNd = idNd;
			IdSp = idSp;
			Soluong = soluong;
			Trangthai = trangthai;
		}
		public bool checkContain(List<Chitietgiohang> list)
		{
			foreach (Chitietgiohang ch in list)
			{
				if (IdSp == ch.IdSp)
				{
					return true;
				}
			}
			return false;
		}
	}
}