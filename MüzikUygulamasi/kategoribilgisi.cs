using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzikUygulamasi
{
    public class kategoribilgisi
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string GorselUrl { get; set; }
        public int BolumId { get; set; }
    }
}
