using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web2023Project.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int IdSp { get; set; }
        public string Url { get; set; }

        public Image(int id, int idSp, string url)
        {
            Id = id;
            IdSp = idSp;
            Url = url;
        }
    }
}
