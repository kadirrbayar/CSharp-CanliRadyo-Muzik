using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuzikUygulamasi
{
    public partial class kategoriler : UserControl
    {
        public kategoribilgisi kategori { get; set; } 
        AnaSayfa anaSayfa;
        public kategoriler(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
        }

        private void kategoriler_Load(object sender, EventArgs e)
        {
            pictureBox1.Load(kategori.GorselUrl);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            anaSayfa.kategoriid = kategori.Id;
            anaSayfa.kategoriaciklama = kategori.Aciklama;
            anaSayfa.kategoribaslik = kategori.Baslik;
            anaSayfa.kategoriurl = kategori.GorselUrl;
            anaSayfa.Kategoriler();
        }
    }
}
