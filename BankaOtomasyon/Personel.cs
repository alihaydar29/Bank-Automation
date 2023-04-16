using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    class Personel:Kisi
    {
        public Personel()
        {
            MessageBox.Show("Personel Başarıyla Eklendi.");
        }
        public Personel(string _ad,string _soyad, string _ıd,string _sifre):this()
        {
            this.Ad = _ad;
            this.Soyad = _soyad;
            this.ID = _ıd;
            this.Sifre = _sifre;
            Banka.personeller.Add(this);
        }
    }
}
