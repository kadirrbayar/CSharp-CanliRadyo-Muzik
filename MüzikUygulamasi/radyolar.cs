using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using MySql.Data.MySqlClient;

namespace MuzikUygulamasi
{
    public partial class radyolar : Form
    {
        AnaSayfa anaSayfa;
        MySqlCommand komut = new MySqlCommand();
        MySqlDataReader okuma;
        public radyolar(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa; 
            int i = 1;
            List<radyobilgisi> radyolar = new List<radyobilgisi>() { };
            string sql = $"SELECT * FROM radyolar ORDER BY adi ASC";
            komut.Connection = Connect.baglanti();
            komut.CommandText = sql;
            okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                radyolar.Add(new radyobilgisi { Sira = i++, Id = (int)okuma["id"], Name = (string)okuma["adi"], Url = (string)okuma["url"] });
            }
            okuma.Close();

            komut.CommandText = $"SELECT Count(*) FROM radyolar";
            string maxKayit = komut.ExecuteScalar().ToString();
            label2.Text = "Uygulamada toplam " + maxKayit + " radyo bulunmaktadır.";

            foreach (var radyo in radyolar)
            {
                radyopanel panel = new radyopanel(anaSayfa);
                panel.radyoo = radyo;
                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
