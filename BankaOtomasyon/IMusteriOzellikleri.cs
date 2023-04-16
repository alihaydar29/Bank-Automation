using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaOtomasyon
{
    interface IMusteriOzellikleri
    {
        void HesapAc(int ekBakiye);
        void HesapSil(int hesapNo);
        void HesapParaYatir(Hesap h, decimal miktar);
        void HesapParaCek(Hesap h, decimal miktar);
    }
}
