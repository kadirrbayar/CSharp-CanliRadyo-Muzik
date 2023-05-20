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
    public partial class arama : Form
    {
        AnaSayfa anaSayfa;
        MySqlCommand komut = new MySqlCommand();
        MySqlDataReader okuma;

        public arama(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
            if (anaSayfa.aramakutusuyazi == "")
            {
                label2.Text = "Bir şeyler yazarak aramayı deneyin :)";
            }
            else
            {
                List<aramakutusu> aramakutum = new List<aramakutusu>() { };
                int i = 1;
                string sql = $"SELECT * FROM muzikler WHERE adi LIKE '%{anaSayfa.aramakutusuyazi}%' OR sanatci LIKE '%{anaSayfa.aramakutusuyazi}%' ";
                komut.Connection = Connect.baglanti();
                komut.CommandText = sql;
                okuma = komut.ExecuteReader();
                while (okuma.Read())
                {
                    aramakutum.Add(new aramakutusu { Sira = i++, MuzikName = (string)okuma["adi"], MuzikSanatci = (string)okuma["sanatci"], MuzikUrl = (string)okuma["url"], RadyoMu = false });
                }
                okuma.Close();
                string sql2 = $"SELECT * FROM radyolar WHERE adi LIKE '%{anaSayfa.aramakutusuyazi}%'";
                komut.Connection = Connect.baglanti();
                komut.CommandText = sql2;
                okuma = komut.ExecuteReader();
                while (okuma.Read())
                {
                    aramakutum.Add(new aramakutusu { Sira = i++, MuzikName = (string)okuma["adi"], MuzikUrl = (string)okuma["url"], RadyoMu = true });
                }
                okuma.Close();

                foreach (var yolla in aramakutum)
                {
                    aramapanel panel = new aramapanel(anaSayfa);
                    panel.araman = yolla;
                    flowLayoutPanel1.Controls.Add(panel);
                }
                label2.Text = "Toplam " + aramakutum.Count().ToString() + " sonuç bulundu.";
            }
        }
    }
}
