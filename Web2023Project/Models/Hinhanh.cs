using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Hinhanh
{
    public int Id { get; set; }

    public int? IdSp { get; set; }

    public string? Url { get; set; }

    public virtual Sanpham? IdSpNavigation { get; set; }
}
