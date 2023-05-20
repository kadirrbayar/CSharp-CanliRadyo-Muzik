using MySql.Data.MySqlClient;
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
    public partial class muzikler : Form
    {
        AnaSayfa anaSayfa;
        MySqlCommand komut = new MySqlCommand();
        MySqlDataReader okuma;

        public muzikler(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
            int i = 1;
            List<muzikbilgisi> muzikler = new List<muzikbilgisi>() { };
            string sql = $"SELECT * FROM muzikler ORDER BY sanatci ASC";
            komut.Connection = Connect.baglanti();
            komut.CommandText = sql;
            okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                muzikler.Add(new muzikbilgisi { Sira = i++, Id = (int)okuma["id"], Adi = (string)okuma["adi"], Sanatci = (string)okuma["sanatci"], Kategori = (int)okuma["kategori"], Url = (string)okuma["url"] });
            }
            okuma.Close();

            komut.CommandText = $"SELECT Count(*) FROM muzikler";
            string maxKayit = komut.ExecuteScalar().ToString();
            label2.Text = "Uygulamada toplam " + maxKayit + " şarkı bulunmaktadır.";

            foreach (var muzik in muzikler)
            {
                muzikpanel panel = new muzikpanel(anaSayfa);
                panel.muzikk = muzik;
                flowLayoutPanel1.Controls.Add(panel);
            }
        }

    }
}
