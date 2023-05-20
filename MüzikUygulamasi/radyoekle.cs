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
    public partial class radyoekle : Form
    {
        MySqlCommand komut = new MySqlCommand();
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader okuma;

        public radyoekle()
        {
            InitializeComponent();
        }
        void VeriGetir()
        {
            dt = new DataTable();
            komut.Connection = Connect.baglanti();
            adapter = new MySqlDataAdapter("SELECT * FROM radyolar", Connect.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void radyoekle_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "adi LIKE '%" + textBox4.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldur!");
            }
            else
            {
                string sql3 = $"SELECT url FROM radyolar WHERE url = '{textBox3.Text}'";
                komut.Connection = Connect.baglanti();
                komut.CommandText = sql3;
                okuma = komut.ExecuteReader();
                if (okuma.Read())
                {
                    MessageBox.Show("Zaten böyle bir kayıt mevcut!");
                    okuma.Close();
                }
                else
                {
                    okuma.Close();
                    string sql = "INSERT INTO radyolar(adi,url) VALUES(@ad,@url)";
                    komut = new MySqlCommand(sql, Connect.baglanti());
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@url", textBox3.Text);
                    komut.ExecuteNonQuery();
                    VeriGetir();
                    MessageBox.Show("Radyo başarıyla eklendi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seçtiğin kayıt silinecek onaylıyor musun ?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sql = $"DELETE FROM radyolar WHERE id = @id";
                komut = new MySqlCommand(sql, Connect.baglanti());
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();
                VeriGetir();
                MessageBox.Show("Radyo başarıyla silindi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldur!");
            }
            else
            {
                string sql = "UPDATE radyolar SET adi=@ad,url=@url WHERE id=@id";
                komut = new MySqlCommand(sql, Connect.baglanti());
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@url", textBox3.Text);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();
                VeriGetir();
                MessageBox.Show("Müzik başarıyla düzenlendi.");
            }
        }
    }
}
