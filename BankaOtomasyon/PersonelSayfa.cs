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
    partial class PersonelSayfa : Form
    {
        public PersonelSayfa()
        {
            InitializeComponent();
        }
        Banka banka;
        public PersonelSayfa(Banka banka)
        {
            InitializeComponent();
            this.banka = banka;
        }
        private void PersonelSayfa_Load(object sender, EventArgs e)
        {
            groupBox1.Text = "Müşteri Bilgisi";
            label1.Text = "Ad";
            label2.Text = "Soyad";
            label3.Text = "Tarih";
            label4.Text = "Müşteri Numarası";
            label5.Text = "Şifre";
            button1.Text = "Kaydet";
            button2.Text = "Hesap Aç";
            button3.Text = "Hesap Sil";
            button4.Text = "Geri";
            groupBox2.Text = "Müşteri Bilgisi";
            label7.Text = "Müşteri Numarası";
            label8.Text = "Ek Bakiye Miktari";
            button5.Text = "Geri";
            groupBox3.Text = "Müşteri Bilgisi";
            label9.Text = "Hesap Numarası";
            button6.Text = "Geri";
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void button1_Click(object sender, EventArgs e)//Müşteri ekle
        {
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string kullaniciAdi = textBox3.Text;
            string sifre = textBox4.Text;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            DateTime tarih = dateTimePicker1.Value;
            banka.MusteriEkle(ad, soyad, kullaniciAdi, sifre, tarih);
            string rapor = kullaniciAdi + " kullanıcı adına sahip " + ad + " " + soyad + " kişisi Müşteri olarak bankaya eklendi.";
            banka.RaporEkle(rapor, tarih);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string musteriNo = textBox5.Text;
            int ekBakiye = Convert.ToInt32(textBox6.Text);

            foreach (Musteri m in Musteri.musteriler)// müşteri kontrol ediyoruz
            {
                if (musteriNo == m.ID)
                {
                    m.HesapAc(ekBakiye);

                    string rapor = m.ID + " kullanıcı adına sahip müşteri için yeni hesap açıldı";
                    DateTime tarih = DateTime.Today;
                    banka.RaporEkle(rapor, tarih);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int hesapNo = Convert.ToInt32(textBox7.Text);

            foreach (Musteri m in Musteri.musteriler)// müşteri mi kontrol ediyoruz
            {
                foreach (Hesap h in m.hesaplar.ToList())//Her bir müşterinin hesaplar listesini tarayarak girilen hesap numarasına ait müşteriyi buluyoruz.
                {
                    if (hesapNo == h.No)
                    {
                        m.HesapSil(hesapNo);//Müşterinin HesapSil metodunu çalıştırıyoruz.

                        string rapor = m.ID + " kullanıcı adına sahip müşterinin " + hesapNo + " numaralı hesabı silindi.";
                        DateTime tarih = DateTime.Today;
                        banka.RaporEkle(rapor, tarih);
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa f1 = Application.OpenForms["AnaSayfa"] as AnaSayfa;
            Panel panel1 = f1.Controls["panel1"] as Panel;
            panel1.Controls.Clear();
            GirisSayfa f3 = new GirisSayfa(banka);
            f3.TopLevel = false;
            panel1.Controls.Add(f3);
            f3.Show();
            f3.Dock = DockStyle.Fill;
        }
    }
}
