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
    public partial class calmalistesiekle : Form
    {
        MySqlCommand komut = new MySqlCommand();
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader okuma;

        public calmalistesiekle()
        {
            InitializeComponent();
        }

        void VeriGetir()
        {
            dt = new DataTable();
            komut.Connection = Connect.baglanti();
            adapter = new MySqlDataAdapter("SELECT id,baslik,aciklama,gorselurl FROM bolum", Connect.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                richTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void calmalistesiekle_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || richTextBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldur!");
            }
            else
            {
                string sql3 = $"SELECT gorselurl FROM bolum WHERE gorselurl = '{textBox2.Text}'";
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
                    string sql = "INSERT INTO bolum(baslik,aciklama,gorselurl) VALUES(@ad,@aciklama,@gorselurl)";
                    komut = new MySqlCommand(sql, Connect.baglanti());
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@gorselurl", textBox2.Text);
                    komut.Parameters.AddWithValue("@aciklama", richTextBox1.Text);
                    komut.ExecuteNonQuery();
                    
                    string sql2 = "INSERT INTO kategoriler(isim) VALUES(@isim)";
                    komut = new MySqlCommand(sql2, Connect.baglanti());
                    komut.Parameters.AddWithValue("@isim", textBox1.Text);
                    komut.ExecuteNonQuery();
                    
                    VeriGetir();
                    MessageBox.Show("Çalma listesi başarıyla eklendi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seçtiğin kayıt silinecek onaylıyor musun ?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sql = $"DELETE FROM bolum WHERE id = @id";
                komut = new MySqlCommand(sql, Connect.baglanti());
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();

                string sql2 = $"DELETE FROM kategoriler WHERE id = @id";
                komut = new MySqlCommand(sql2, Connect.baglanti());
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();

                VeriGetir();
                MessageBox.Show("Çalma listesi başarıyla silindi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || richTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldur!");
            }
            else
            {
                string sql = "UPDATE bolum SET baslik=@baslik,aciklama=@aciklama,gorselurl=@gorselurl WHERE id=@id";
                komut = new MySqlCommand(sql, Connect.baglanti());
                komut.Parameters.AddWithValue("@baslik", textBox1.Text);
                komut.Parameters.AddWithValue("@aciklama", richTextBox1.Text);
                komut.Parameters.AddWithValue("@gorselurl", textBox2.Text);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();

                string sql2 = "UPDATE kategoriler SET isim=@isim WHERE id=@id";
                komut = new MySqlCommand(sql2, Connect.baglanti());
                komut.Parameters.AddWithValue("@isim", textBox1.Text);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();

                VeriGetir();
                MessageBox.Show("Kategori başarıyla düzenlendi.");
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "baslik LIKE '%" + textBox4.Text + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
