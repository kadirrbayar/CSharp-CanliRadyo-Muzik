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
    public partial class kayitol : Form
    {
        AnaSayfa anaSayfa;
        public kayitol(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (kayitpass.PasswordChar == '*' || kayitrepass.PasswordChar == '*')
            {
                kayitpass.PasswordChar = '\0';
                kayitrepass.PasswordChar = '\0';
            }
            else
            {
                kayitpass.PasswordChar = '*';
                kayitrepass.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = kayitname.Text;
            string username = kayitusername.Text;
            string pass = kayitpass.Text;
            string repass = kayitrepass.Text;    
            anaSayfa.KayitSorgu(name, username, pass, repass);
        }
    }
}
