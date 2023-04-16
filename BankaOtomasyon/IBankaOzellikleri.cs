using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaOtomasyon
{
    public interface IBankaOzellikleri
    {
        void MusteriEkle(string ad, string soyad, string ID, string sifre, DateTime tarih);
        void PersonelEkle(string ad, string soyad, string ID, string sifre);
        void PersonelSil(string kullaniciAdi);
    }
}
