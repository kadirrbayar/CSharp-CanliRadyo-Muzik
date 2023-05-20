using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Security.Cryptography;

namespace MuzikUygulamasi
{
    public partial class girisyap : Form
    {
        AnaSayfa anaSayfa;
        public girisyap(AnaSayfa gelenSayfa)
        {
            InitializeComponent();
            anaSayfa = gelenSayfa;
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(textBox2.PasswordChar == '*')
                textBox2.PasswordChar = '\0';
            else
                textBox2.PasswordChar = '*';
        }

         private void button1_Click(object sender, EventArgs e)
         {
            string kullanici = textBox1.Text;
            string pass = textBox2.Text;
            anaSayfa.GirisSorgu(kullanici,pass);
         }
    }
}
