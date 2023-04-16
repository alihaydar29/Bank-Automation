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
    partial class YoneticiSayfa : Form
    {
        public YoneticiSayfa()
        {
            InitializeComponent();
        }
        public YoneticiSayfa(Banka banka)
        {
            InitializeComponent();
            this.banka = banka;
        }
        Banka banka;
        private void YoneticiSayfa_Load(object sender, EventArgs e)
        {
            label1.Text = "Adı";
            label2.Text = "Soyadı";
            label3.Text = "Kullanıcı Adı";
            label4.Text = "Şifre";
            groupBox1.Text = "Personal Bilgileri";
            button1.Text = "Ekle";
            groupBox2.Text = "Personel Sil";
            label5.Text = "Kullanıcı Adı";
            button2.Text = "Onayla";
            label6.Text = "Banka Toplam Para: -TL";
            button3.Text = "Banka İşlemlerini Listele";
            button4.Text = "Geri";
            button5.Text = "Geri";
            button6.Text = "Geri";
            button7.Text = "Geri";
            this.FormBorderStyle = FormBorderStyle.None; //formun sağ usteki kapatma simgesi gibi simgeleri çıkardık
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.DataSource = Banka.personeller.ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Ad = textBox1.Text; //Personel Bilgilerini textBox'dan okuyup yeni nesneye ekliyoruz.
            string Soyad = textBox2.Text;
            string ID = textBox3.Text;
            string Sifre = textBox4.Text;
            textBox1.Clear(); //Personel eklendikten sonra textBoxları temizliyoruz.
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            banka.PersonelEkle(Ad, Soyad, ID, Sifre); //Personel bilgilerini Banka sınıfındaki PersonelEkle metoduna gönderiyoruz.        
            string rapor = ID + " kullanıcı adına sahip kişi bankaya personel olarak eklendi.";
            DateTime tarih = DateTime.Today;
            banka.RaporEkle(rapor, tarih);
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.DataSource = Banka.personeller; //gridView öğesine personeller listesini yazdırıyoruz.
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox5.Text;
            textBox5.Clear();
            banka.PersonelSil(kullaniciAdi);
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.DataSource = Banka.personeller;
            string rapor = kullaniciAdi + " kullanici adına sahip banka personeli silindi.";
            DateTime tarih = DateTime.Today;
            banka.RaporEkle(rapor, tarih);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = banka.BankaRaporListesi;
            label6.Text = "Banka toplam para:" + banka.toplamPara + " TL";
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
