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
    public partial class aramapanel : UserControl
    {
        public aramakutusu araman { get; set; }
        AnaSayfa anaSayfa;

        public aramapanel(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
        }

        private void aramapanel_Load(object sender, EventArgs e)
        {
            if(araman.RadyoMu == false)
            {
                ıconButton1.Text = $"{araman.MuzikSanatci} - {araman.MuzikName}";
                label1.Text = $"{araman.Sira}.";
            }
            else
            {
                ıconButton1.Text = $"{araman.MuzikName}";
                label1.Text = $"{araman.Sira}.";
            }
        }

        private void ıconButton1_Click_1(object sender, EventArgs e)
        {
            if (araman.RadyoMu == false)
            {
                anaSayfa.calanSarki = $"{araman.MuzikSanatci} - {araman.MuzikName}";
                string url = $"https://docs.google.com/uc?export=open&id=" + araman.MuzikUrl;
                anaSayfa.PlayURLFile(url);
            }
            else
            {
                anaSayfa.calanSarki = $"{araman.MuzikName}";
                string url = araman.MuzikUrl;
                anaSayfa.PlayURLFile(url);
            }
        }
    }
}
