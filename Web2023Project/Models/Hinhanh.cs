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

    }
}