using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    partial class GirisSayfa : Form
    {
        Banka banka;
        public GirisSayfa()
        {
            InitializeComponent();
        }
        public GirisSayfa(Banka banka)
        {
            InitializeComponent();
            this.banka = banka;
        }
        private void GirisSayfa_Load(object sender, EventArgs e)
        {
            label1.Text = "Kullanıcı Adı";
            label2.Text = "Şifre";
            groupBox1.Text = "Giriş Bilgilerinizi Girin";
            button1.Text = "Müsteri Girişi";
            button2.Text = "Personel Girişi";
            button3.Text = "Yönetici Girişi";
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string musteriNo = textBox1.Text;
            string sifre = textBox2.Text;
            foreach (Musteri m in Musteri.musteriler)
            {
                if (musteriNo == m.ID && sifre == m.Sifre)
                {
                    AnaSayfa f1 = Application.OpenForms["AnaSayfa"] as AnaSayfa;
                    Panel panel1 = f1.Controls["panel1"] as Panel;
                    panel1.Controls.Clear();
                    MusteriSayfa formMusteri = new MusteriSayfa(banka, m);
                    formMusteri.TopLevel = false;
                    panel1.Controls.Add(formMusteri);
                    formMusteri.Show();
                    formMusteri.Dock = DockStyle.Fill;
                    MessageBox.Show("Hoşgeldiniz Sayın " + m.Ad + " " + m.Soyad);
                }  
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;
            foreach (Personel p in Banka.personeller)
            {
                if (kullaniciAdi == p.ID && sifre == p.Sifre)
                {
                    AnaSayfa f1 = Application.OpenForms["AnaSayfa"] as AnaSayfa;
                    Panel panel1 = f1.Controls["panel1"] as Panel;
                    panel1.Controls.Clear();

                    PersonelSayfa formPersonel = new PersonelSayfa(banka);
                    formPersonel.TopLevel = false;
                    panel1.Controls.Add(formPersonel);
                    formPersonel.Show();
                    formPersonel.Dock = DockStyle.Fill;
                    MessageBox.Show("Hoşgeldiniz. Sayın " + p.Ad + " " + p.Soyad);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnaSayfa f1 = Application.OpenForms["AnaSayfa"] as AnaSayfa;
            Panel panel1 = f1.Controls["panel1"] as Panel;
            panel1.Controls.Clear();
            YoneticiSayfa Yönetici = new YoneticiSayfa(banka);
            Yönetici.TopLevel = false;
            panel1.Controls.Add(Yönetici);
            Yönetici.Show();
            Yönetici.Dock = DockStyle.Fill;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            BankaOzellikler a = new Banka();
            a.Bilgi();
            MessageBox.Show("Hoşgeldiniz");
        }
    }
}
