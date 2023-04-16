using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaOtomasyon
{
    class Hesap
    {
        public int No { get; set; }
        public decimal bakiye { get; set; }
        public decimal gunlukLimit { get; set; }
        public decimal limit { get; set; }
        public decimal ekBakiye { get; set; }
        public List<Rapor> RaporListesi;
        Rapor r;

        public Hesap()
        {
            RaporListesi = new List<Rapor>();
        }
        public void RaporEkle(string rapor, DateTime tarih)
        {
            r = new Rapor();
            this.r.rapor = rapor;
            this.r.tarih = tarih;
            RaporListesi.Add(r);
        }
    }
}
