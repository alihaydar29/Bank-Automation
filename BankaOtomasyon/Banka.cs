using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    class Banka :BankaOzellikler, IBankaOzellikleri, IRaporOzellikleri
    {
        public static List<Personel> personeller = new List<Personel>();
        public List<Rapor> BankaRaporListesi = new List<Rapor>();
        public decimal toplamPara { get; set; }
        Rapor r;
        public void MusteriEkle(string ad, string soyad, string ID, string sifre, DateTime tarih)
        { 
            Musteri m = new Musteri();
            m.Soyad = soyad;
            m.ID = ID;
            m.Sifre = sifre;
            m.Tarih = tarih;
            Musteri.musteriler.Add(m);
            MessageBox.Show("Müşteri Başarıyla Eklendi.");
        }
        public override void Bilgi()
        {
            MessageBox.Show( ProgramınAcılıs+" "+BankaAdı+" otomasyonunu kullanmaya başlamıştınız");
        }
        public void PersonelEkle(string ad, string soyad, string ID, string sifre)
        {
            Kisi k = new Personel(ad,soyad,ID,sifre);//Polimorphism
           
        }
        public void PersonelSil(string kullaniciAdi)
        {
           foreach (Personel p in personeller.ToList())
            {
                if (kullaniciAdi == p.ID)
                {
                    personeller.Remove(p);
                    MessageBox.Show("Personel Silindi.");
                }
            }
            MessageBox.Show("Personel Silindi.");
        }
        
        public void RaporEkle(string rapor, DateTime tarih)
        {
            r = new Rapor();
            r.rapor = rapor;
            r.tarih = tarih;
            BankaRaporListesi.Add(r);
        }
    }
}
