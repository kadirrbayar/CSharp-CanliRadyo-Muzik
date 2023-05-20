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
    public partial class muzikekle : Form
    {
        MySqlCommand komut = new MySqlCommand();
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader okuma;

        public muzikekle()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }
        void VeriGetir()
        {
            dt = new DataTable();
            komut.Connection = Connect.baglanti();
            adapter = new MySqlDataAdapter("SELECT muzikler.id,muzikler.adi,muzikler.sanatci,muzikler.url,kategoriler.isim FROM muzikler INNER JOIN kategoriler on muzikler.kategori = kategoriler.id", Connect.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void muzikekle_Load(object sender, EventArgs e)
        {
            VeriGetir();

            string sql = $"SELECT * FROM kategoriler";
            komut.Connection = Connect.baglanti();
            komut.CommandText = sql;
            okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                comboBox1.Items.Add(okuma["isim"]);
                comboBox2.Items.Add(okuma["isim"]);
            }
            okuma.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldur!");
            }
            else
            {
                string sql2 = "SELECT * FROM kategoriler WHERE isim = '" + comboBox1.Text + "'";
                komut.Connection = Connect.baglanti();
                komut.CommandText = sql2;
                okuma = komut.ExecuteReader();
                okuma.Read();
                string id = okuma["id"].ToString();
                okuma.Close();

                string sql3 = $"SELECT url FROM muzikler WHERE url = '{textBox3.Text}'";
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
                    string sql = "INSERT INTO muzikler(adi,sanatci,url,kategori) VALUES(@ad,@sanatci,@url,@kategori)";
                    komut = new MySqlCommand(sql, Connect.baglanti());
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@sanatci", textBox2.Text);
                    komut.Parameters.AddWithValue("@url", textBox3.Text);
                    komut.Parameters.AddWithValue("@kategori", id);
                    komut.ExecuteNonQuery();
                    VeriGetir();
                    MessageBox.Show("Müzik başarıyla eklendi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seçtiğin kayıt silinecek onaylıyor musun ?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sql = $"DELETE FROM muzikler WHERE id = @id";
                komut = new MySqlCommand(sql, Connect.baglanti());
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();
                VeriGetir();
                MessageBox.Show("Müzik başarıyla silindi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldur!");
            }
            else
            {
                string sql2 = $"SELECT id FROM kategoriler WHERE isim = '{comboBox1.Text}'";
                komut.Connection = Connect.baglanti();
                komut.CommandText = sql2;
                okuma = komut.ExecuteReader();
                okuma.Read();
                string kategoriid = okuma["id"].ToString();
                okuma.Close();

                string sql = "UPDATE muzikler SET adi=@ad,sanatci=@sanatci,url=@url,kategori=@kategori WHERE id=@id";
                komut = new MySqlCommand(sql, Connect.baglanti());
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@sanatci", textBox2.Text);
                komut.Parameters.AddWithValue("@url", textBox3.Text);
                komut.Parameters.AddWithValue("@kategori", kategoriid);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();
                VeriGetir();
                MessageBox.Show("Müzik başarıyla düzenlendi.");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "adi LIKE '%" + textBox4.Text + "%' OR sanatci LIKE '%" + textBox4.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "isim LIKE '" + comboBox2.SelectedItem + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
