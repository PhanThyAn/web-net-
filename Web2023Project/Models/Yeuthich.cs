using System;
using System.Collections.Generic;


namespace Web2023Project.Models
{

    public class Yeuthich
    {
        public int Id { get; set; }

        public int IdNd { get; set; }

        public int IdSp { get; set; }

        public DateTime Ngaytao { get; set; }

        public virtual Nguoidung IdNdNavigation { get; set; }

        public virtual Sanphams IdSpNavigation { get; set; }

    }
}

