using System.Collections.Generic;

namespace Web2023Project.Models
{

    public partial class Nhacungcap
    {
        public int Id { get; set; }

        public string TenNcc { get; set; }

        public string Diachi { get; set; }

        public sbyte? Trangthai { get; set; }

        public virtual ICollection<Sanphams> Sanphams { get; set; } = new List<Sanphams>();
		public string TepHinhAnh { get; set; }
		public string TenTepHinhAnh { get; set; }
		public Nhacungcap()
		{
		}
		//public Nhacungcap(int id, string tenNcc, string diachi, sbyte? trangthai)
		//{
		//	Id = id;
		//	TenNcc = tenNcc;
		//	Diachi = diachi;
		//	Trangthai = trangthai;
		//}
		public Nhacungcap(string tenNcc, string diachi, sbyte? trangthai, string tepHinhAnh, string tenTepHinhAnh)
		{
			TenNcc = tenNcc;
			Diachi = diachi;
			Trangthai = trangthai;
			TepHinhAnh = tepHinhAnh;
			TenTepHinhAnh = tenTepHinhAnh;
		}
	}
}

