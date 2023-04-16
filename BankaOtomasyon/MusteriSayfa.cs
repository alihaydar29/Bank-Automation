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
    partial class MusteriSayfa : Form
    {
        public MusteriSayfa()
        {
            InitializeComponent();
        }
        public MusteriSayfa(Banka banka, Musteri m)//Bu form açıldığında giriş yapan müşteri bireysel ise bu constructor çalışır.
        {
            InitializeComponent();
            this.banka = banka;
            this.musteri = m;
        }
        Banka banka;
        Musteri musteri;
        private void MusteriSayfa_Load(object sender, EventArgs e)
        {
            label1.Text = "Hesap Numarası";
            label2.Text = "Miktar";
            button1.Text = "Çek";
            label3.Text = "Günlük para çekme miktarı 750 TL";
            groupBox1.Text = "Para Çekme İşlemi İçin Boş Alanları Doldurun";
            groupBox2.Text = "Para Yatırma İşlemi İçin Boş Alanları Doldurun";
            groupBox3.Text= "Havale İşlemi İçin Boş Alanları Doldurun";
            label4.Text = "Hesap Numarası";
            label5.Text = "Miktar";
            button2.Text = "Yatır";
            label6.Text = "Kaynak hesap";
            label7.Text = "Hedef hesap";
            label8.Text = "Miktar";
            button3.Text = "Gönder";
            label9.Text = "Hesap Numarası";
            button4.Text = "Listele";
            button5.Text = "Listele";
            button6.Text = "Geri";
            button7.Text = "Geri";
            button8.Text = "Geri";
            button9.Text = "Geri";
            button10.Text = "Geri";
            this.FormBorderStyle = FormBorderStyle.None; //formun tasarımını ayarladık 
        }
        private void button1_Click(object sender, EventArgs e)//Para çekme
        {
            int hesapNo = Convert.ToInt32(textBox1.Text);
            decimal miktar = Convert.ToDecimal(textBox2.Text);

            foreach (Musteri m in Musteri.musteriler)
            {
                foreach (Hesap h in m.hesaplar)
                {
                    if (hesapNo == h.No)
                        m.HesapParaCek(h, miktar);
                }
            }
            string rapor = hesapNo + " nolu Hesaptan "+miktar+" para çekildi.";
            DateTime tarih = DateTime.Today;
            banka.RaporEkle(rapor, tarih);

        }
        private void button2_Click(object sender, EventArgs e)//para Yatırma
        {
            int hesapNo = Convert.ToInt32(textBox3.Text);
            decimal miktar = Convert.ToDecimal(textBox4.Text);
            foreach (Musteri m in Musteri.musteriler)
            {
                foreach (Hesap h in m.hesaplar)
                {
                    if (hesapNo == h.No)
                        m.HesapParaYatir(h, miktar);
                }
            }
            string rapor = hesapNo + " nolu Hesaptan " + miktar + " para yatırıldı.";
            DateTime tarih = DateTime.Today;
            banka.RaporEkle(rapor, tarih);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int kaynakNo = Convert.ToInt32(textBox5.Text);
            int hedefNo = Convert.ToInt32(textBox6.Text);
            int miktar = Convert.ToInt32(textBox7.Text);
            decimal bankaPayi = 0.0m, hedefPayi = 0.0m;
            decimal islemOrani = 0.0m;
            Hesap kaynakHesap = null, hedefHesap = null;
            Musteri kaynakMusteri = null;
            bool kaynakDurum = false, hedefDurum = false, kaynakHesapTuru = false;

            foreach (Musteri m in Musteri.musteriler) //kaynak hesap bireysel müşteri iste
            {
                foreach (Hesap h in m.hesaplar)
                {
                    if (kaynakNo == h.No)
                    {
                        kaynakHesap = h;//Kaynak hesabı buluyoruz
                        kaynakMusteri = m;//Kaynak müşteriyi buluyoruz
                        kaynakDurum = true;
                        kaynakHesapTuru = true;
                    }
                    if (hedefNo == h.No)
                    {
                        hedefHesap = h;//hedef hesabı buluyoruz
                        kaynakMusteri = m;//Kaynak müşteriyi buluyoruz
                        hedefDurum = true;
                    }
                }
            }
            if (kaynakDurum == true && hedefDurum == true)//Kaynak ve Hedef hesap numaraları bulunduysa
            {
                if (kaynakHesap.bakiye >= miktar)
                {
                    if (kaynakHesapTuru == true)
                        islemOrani = 2.0m;
                    kaynakHesap.bakiye -= miktar;
                    bankaPayi = (miktar * islemOrani) / 100;
                    hedefPayi = miktar - bankaPayi;
                    banka.toplamPara += bankaPayi;
                    MessageBox.Show("Hedef hesaba " + hedefPayi + " TL aktarılmıştır. \n Banka işlem ücreti: " + bankaPayi + " TL");
                    string rapor = kaynakNo + " numaralı hesaptan " + hedefNo + " numaralı hesaba " + hedefPayi + " TL aktarılmıştır. \n Banka işlem ücreti: " + bankaPayi + " TL";
                    DateTime tarih = DateTime.Today;
                    banka.RaporEkle(rapor, tarih);
                    rapor = kaynakNo + " numaralı hesabınızdan " + hedefNo + " numaralı hesaba " + hedefPayi + " TL aktarılmıştır. \n Banka işlem ücreti: " + bankaPayi + " TL";
                    kaynakHesap.RaporEkle(rapor, tarih);
                    rapor = kaynakNo + " numaralı hesaptan " + hedefNo + " numaralı hesabınıza " + hedefPayi + " TL aktarılmıştır.";
                    hedefHesap.RaporEkle(rapor, tarih);
                }
            }
            else
                MessageBox.Show("Lütfen Hedef ve Kaynak hesap numaralarını kontrol ediniz.");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int hesapNo = 0;
            if (textBox8.Text != "")//Hesap Özeti Listeleme TextViewi boş bırakılırsa hata vermemesi için
            {
                hesapNo = Convert.ToInt32(textBox8.Text);
                DateTime timeBaslangic = dateTimePicker1.Value;
                DateTime timeBitis = dateTimePicker2.Value;
                int BaslangicGun = timeBaslangic.Day;
                int BitisGun = timeBitis.Day;
                List<Rapor> GosterilecekRapor = new List<Rapor>();
                foreach (Musteri m in Musteri.musteriler)
                {
                    foreach (Hesap h in m.hesaplar)
                    {
                        if (hesapNo == h.No)
                        {
                            foreach (Rapor r in h.RaporListesi)
                            {
                                TimeSpan t = timeBitis - r.tarih; //Seçilen bitiş tarihi ile işlem tarihi arasındaki farkı alıyoruz.
                                int baslangicDegeri = t.Days; //Farkı güne çeviriyoruz.
                                TimeSpan t2 = r.tarih - timeBaslangic;
                                int bitisDegeri = t2.Days;

                                if (baslangicDegeri >= 0 && bitisDegeri >= 0)//Eğer ki farklar - değer değilse yani belirtilen aralık arasındaysa 
                                {
                                    GosterilecekRapor.Add(r);
                                }
                            }
                        }
                    }
                }
                dataGridView1.DataSource = GosterilecekRapor;
            }
            else
                MessageBox.Show("Lütfen hesap numarası giriniz.");
        }
        private void button5_Click(object sender, EventArgs e)
        {
                dataGridView2.DataSource = musteri.hesaplar;
        }
        private void button6_Click(object sender, EventArgs e)
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
