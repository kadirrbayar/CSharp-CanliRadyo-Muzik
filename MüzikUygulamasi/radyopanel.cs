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
    public partial class radyopanel : UserControl
    {
        public radyobilgisi radyoo { get; set; } // Product Property
        AnaSayfa anaSayfa;
        public radyopanel(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
        }

        private void bilgiler_Load(object sender, EventArgs e)
        {
            ıconButton1.Text = radyoo.Name;
            label1.Text = $"{radyoo.Sira}.";
        }

        private void ıconButton1_Click_1(object sender, EventArgs e)
        {
            anaSayfa.calanSarki = radyoo.Name;
            string url = radyoo.Url;
            anaSayfa.PlayURLFile(url);
        }
    }
}
