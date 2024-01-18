using System;
using System.Collections.Generic;

namespace Web2023Project.Models
{

    public partial class Hinhanh
    {
        public int Id { get; set; }

        public int? IdSp { get; set; }

        public string Url { get; set; }

        public virtual Sanphams IdSpNavigation { get; set; }

		public Hinhanh()
		{
		}

		public Hinhanh(int idSp, string url)
		{
			IdSp = idSp;
			Url = url;
		}
		public Hinhanh(int id, int? idSp, string url)
		{
			Id = id;
			IdSp = idSp;
			Url = url;
		}
	}
}