using FontAwesome.Sharp;
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
    public partial class muzikpanel : UserControl
    {
        public muzikbilgisi muzikk { get; set; }
        AnaSayfa anaSayfa;
        public muzikpanel(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
        }

        private void muzikpanel_Load(object sender, EventArgs e)
        {
            ıconButton1.Text = $"{muzikk.Sanatci} - {muzikk.Adi}";
            label1.Text = $"{muzikk.Sira}.";
        }

        private void ıconButton1_Click_1(object sender, EventArgs e)
        {
            anaSayfa.calanSarki = $"{muzikk.Sanatci} - {muzikk.Adi}";
            string url = $"https://docs.google.com/uc?export=open&id=" + muzikk.Url;
            anaSayfa.PlayURLFile(url);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
