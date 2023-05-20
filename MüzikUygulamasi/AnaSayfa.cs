using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Security.Cryptography;
using LibVLCSharp.Shared;

namespace MuzikUygulamasi
{
    public partial class AnaSayfa : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        MySqlCommand komut = new MySqlCommand();
        MySqlDataReader okuma;
        public bool GirisYapildiMi = false;
        public string KullaniciBilgisi;
        public LibVLC _libVLC;
        public MediaPlayer _mp;
        public Media media;
        public bool isPlaying = false;
        public string calanSarki = "";
        public int kategoriid;
        public string kategoriaciklama;
        public string kategoribaslik;
        public string kategoriurl;
        public string aramakutusuyazi = "";
        public bool isMute = false;

        public AnaSayfa()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            Core.Initialize();
            _libVLC = new LibVLC();
            _mp = new MediaPlayer(_libVLC);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                ıconCurrentForm.IconChar = currentBtn.IconChar;
                ıconCurrentForm.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void OpenChildForm(Form childForm)
        {
            
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            ıconCurrentForm.IconChar = IconChar.Home;
            ıconCurrentForm.IconColor = Color.White;
            lblFormTitle.Text = "Ana Sayfa";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void ıconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new hakkimizda());
            lblFormTitle.Text = "Hakkımızda";
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void ıconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new muzikler(this));
            lblFormTitle.Text = "Müzikler";
        }

        private void ıconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new radyolar(this));
            lblFormTitle.Text = "Radyolar";
        }

        private void ıconButton5_Click(object sender, EventArgs e)
        {

        }

        private void ıconButton6_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new girisyap(this));
            lblFormTitle.Text = "Giriş Yap";
        }

        private void ıconButton1_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        private void AnaSayfa_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Connect.kapat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            try
            {
                Connect.baglanti();
            }
            catch (Exception err)
            {
                MessageBox.Show("Hata! " + err.Message, "Hata Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ıconButton7.Visible = false;
            ıconWelcome.Visible = false;
            labelWelcome.Visible = false;
            ıconButton13.Visible = false;
            ıconButton14.Visible = false;
            ıconButton5.Visible = false;
            List<kategoribilgisi> kategoriler = new List<kategoribilgisi>() { };
            string sql = $"SELECT * FROM bolum";
            komut.Connection = Connect.baglanti();
            komut.CommandText = sql;
            okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                kategoriler.Add(new kategoribilgisi { Id = (int)okuma["id"], Baslik = (string)okuma["baslik"], Aciklama = (string)okuma["aciklama"], GorselUrl = (string)okuma["gorselurl"] });
            }
            okuma.Close();

            foreach (var kategori in kategoriler)
            {
                kategoriler panel = new kategoriler(this);
                panel.kategori = kategori;
                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        public void PlayURLFile(string file)
        {
            _mp.Play(new Media(_libVLC, new Uri(file)));
            isPlaying = true;
            ıconButton9.IconChar = IconChar.PauseCircle;
            altbartext.Text = calanSarki;
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }
        public static string MD5Encryption(string encryptionText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] array = Encoding.UTF8.GetBytes(encryptionText);
            array = md5.ComputeHash(array);
            StringBuilder sb = new StringBuilder();

            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        public void KayitSorgu(string name, string kullaniciadi, string pass, string repass)
        {
            if(name == "" || kullaniciadi == "" || pass == "" || repass == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
            else
            {
                string sql = $"SELECT * FROM users WHERE user_name='{kullaniciadi}'";
                komut.Connection = Connect.baglanti();
                komut.CommandText = sql;
                okuma = komut.ExecuteReader();
                if (okuma.Read())
                {
                    MessageBox.Show("Bu kullanıcı adı kullanımda. Lütfen yeni bir kullanıcı adı ile yeniden deneyin.");
                }
                else
                {
                    okuma.Close();
                    if (pass != repass)
                    {
                        MessageBox.Show("Şifreler eşleşmiyor. Lütfen yeniden deneyin.");
                    }
                    else
                    {
                        komut.CommandText = $"INSERT INTO users(user_name, password, name) VALUES(@kullaniciadi, @pass, @name)";                   
                        komut.Parameters.AddWithValue("@kullaniciadi", kullaniciadi);
                        komut.Parameters.AddWithValue("@pass", MD5Encryption(pass));
                        komut.Parameters.AddWithValue("@name", name);
                        komut.ExecuteNonQuery();

                        OpenChildForm(new girisyap(this));
                        lblFormTitle.Text = "Giriş Yap";

                        ıconButton8.Visible = false;
                        MessageBox.Show("Başarıyla kayıt oldun. Giriş sayfasına yönlendiriliyorsun.");
                    }
                }
                okuma.Close();
            }
        }

        public void GirisSorgu(string kullaniciadi, string pass)
        {
            string sql = $"SELECT * FROM users WHERE user_name='{kullaniciadi}' AND password='{MD5Encryption(pass)}'";
            komut.Connection = Connect.baglanti();
            komut.CommandText = sql;
            okuma = komut.ExecuteReader();
            if (okuma.Read())
            {    
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                }
                Reset();

                KullaniciBilgisi = $"{okuma["id"]}";
                GirisYapildiMi = true;
                labelWelcome.Text = $"Merhaba {okuma["name"]}";
                ıconWelcome.Visible = true;
                labelWelcome.Visible = true;
                ıconButton7.Visible = true;
                ıconButton6.Visible = false;
                ıconButton8.Visible = false;
                if (okuma["id"].ToString() == "16" && okuma["user_name"].ToString() == "admin")
                {
                    ıconButton13.Visible = true;
                    ıconButton14.Visible = true;
                    ıconButton5.Visible = true;
                }
                MessageBox.Show("Başarıyla giriş yapıldı.");
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifreniz hatalı.");
            }
            okuma.Close();
        }

        private void ıconButton8_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new kayitol(this));
            lblFormTitle.Text = "Kayıt Ol";
        }

        private void ıconButton7_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
            ıconButton7.Visible = false;
            ıconWelcome.Visible = false;
            labelWelcome.Visible = false;
            ıconButton6.Visible = true;
            ıconButton8.Visible = true;
            GirisYapildiMi = false;
            ıconButton13.Visible = false;
            ıconButton14.Visible = false;
            ıconButton5.Visible = false;
            MessageBox.Show("Başarıyla çıkış yapıldı.");
        }

        private void ıconButton9_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                _mp.Pause();
                ıconButton9.IconChar = IconChar.PlayCircle;
                isPlaying = false;
            }
            else
            {
                _mp.Play();
                isPlaying = true;
                ıconButton9.IconChar = IconChar.PauseCircle;
            }

        }

        private void ıconButton13_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new muzikekle());
            lblFormTitle.Text = "Müzik Ekle";
        }

        private void ıconButton14_Click(object sender, EventArgs e)
        {
              ActivateButton(sender, RGBColors.color2);
              OpenChildForm(new radyoekle());
              lblFormTitle.Text = "Radyo Ekle";
        }

        public void Kategoriler()
        {
            OpenChildForm(new calmalistesi(this));
        }

        private void aramakutusu_Click(object sender, EventArgs e)
        {
            aramakutusu.Text = "";
        }

        private void aramakutusu_TextChanged(object sender, EventArgs e)
        {
            aramakutusuyazi = aramakutusu.Text;
            OpenChildForm(new arama(this));
            lblFormTitle.Text = "Arama Sonuçları";
        }

        private void ıconButton5_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new calmalistesiekle());
            lblFormTitle.Text = "Kategori Ekle";
        }
        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            _mp.Volume = trackBar1.Value;
        }

        private void ıconButton12_Click(object sender, EventArgs e)
        {
            if (!isMute)
            {
                _mp.Mute = true;
                ıconButton12.IconChar = IconChar.VolumeMute;
                isMute = true;
            }
            else
            {
                _mp.Mute = false;
                ıconButton12.IconChar = IconChar.VolumeUp;
                isMute = false;
            }
        }
    }
}
