using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    class Musteri : Kisi, IMusteriOzellikleri
    {
        public DateTime Tarih { get; set; }
        public string musteriTipi { get; set; }
        public List<Hesap> hesaplar;
        Hesap h;
        string rapor;
        DateTime tarih;

        public Musteri()
        {
            hesaplar = new List<Hesap>();
        }
        public static List<Musteri> musteriler = new List<Musteri>();
        public void HesapAc(int ekBakiye)
        {
            Random r = new Random(); //Hesap numarası için rastgele 3 haneli sayı üretiyoruz.
            int sayi = r.Next(100, 999);
            h = new Hesap();
            h.No = sayi;
            h.bakiye = 0;
            h.ekBakiye = ekBakiye;
            h.limit = ekBakiye;
            h.gunlukLimit = 750;
            hesaplar.Add(h);
            MessageBox.Show("'" + ID + "' Numaralı müşteri için '" + sayi + "' numaralı hesap '" + ekBakiye + "' ek bakiyesi ile açılmıştır.");
        }
        public void HesapSil(int hesapNo)
        {
            foreach (Hesap h in hesaplar.ToList())
            {
                if (hesapNo == h.No)
                {
                    if (h.bakiye == 0 && h.ekBakiye == h.limit) //Hesapta para yoksa ve ekBakiye borcu yoksa
                    {
                        hesaplar.Remove(h);
                        MessageBox.Show("'" + hesapNo + "' numaralı hesap başarıyla silindi.");
                    }
                    else
                        MessageBox.Show("'" + hesapNo + "' numaralı hesap silinebilmesi için bakiyesi 0 TL ve ek bakiye borcu olmamalıdır. \n Mevcut Bakiye: '" + h.bakiye + "' TL \n Güncel borç: '" + (h.limit - h.ekBakiye + "' TL"));
                }
            }
        }
        public void HesapParaYatir(Hesap h, decimal miktar)
        {
            decimal odenenBorc = 0;
            h.bakiye += miktar;
            MessageBox.Show("Hesabınıza '" + miktar + "' TL eklenmiştir. \n Mevcut bakiye '" + h.bakiye + "' TL");
            rapor = ("'" + miktar + "' TL yatırıldı.");
            tarih = DateTime.Today;
            h.RaporEkle(rapor, tarih);//Hesap Özetine yapılan işlem ve tarihi gönderiliyor

            if (h.ekBakiye < h.limit)//Ekbakiye borcu varsa
            {
                decimal borc = h.limit - h.ekBakiye;
                for (decimal i = h.ekBakiye; h.ekBakiye < h.limit; i += 0.1m)//Ekbakiye borcu varsa para yatırırken otomatik olarak borç ödenir
                {
                    if (h.bakiye == 0.0m) { break; }
                    else
                    {
                        h.bakiye -= 0.1m;
                        odenenBorc += 0.1m;
                        h.ekBakiye += 0.1m;
                    }
                }
                MessageBox.Show("'" + borc + "' TL olan ek bakiye borcunuzun '" + odenenBorc + "' TL kadarı bakiyenizden tahsil edilmiştir.");
                rapor = "'" + borc + "' TL olan ek bakiye borcunuzun '" + odenenBorc + "' TL kadarı bakiyenizden tahsil edilmiştir.";
                tarih = DateTime.Today;
                h.RaporEkle(rapor, tarih);
            }
        }
        public void HesapParaCek(Hesap h, decimal miktar)
        {
            if (h.ekBakiye != 0.0m && h.ekBakiye >= 0)
            {
                if (h.gunlukLimit != 0.0m && h.gunlukLimit >= 0.0m)//Günlük para çekme limiti dolmamışsa
                {
                    if (h.gunlukLimit - miktar != 0.0m && h.gunlukLimit - miktar >= 0.0m) // Çekilmek istenen para çekilince günlük para çekme limiti 0'a eşit olmuyorsa ve - değere düşmüyorsa
                    {
                        if (h.bakiye >= miktar)//Çekilmek istenen paranın tamamı bakiyede varsa
                        {
                            h.bakiye -= miktar;
                            h.gunlukLimit -= miktar;//Çekilen parayı günlük limitten düşüyoruz

                            MessageBox.Show("Hesabınızdan '" + miktar + "' TL çekilmiştir.");
                            rapor = ("'" + miktar + "' TL bakiyenizden çekilmiştir.");
                            tarih = DateTime.Today;
                            h.RaporEkle(rapor, tarih); //Hesap Özetine yapılan işlem ve tarihi gönderiliyor
                        }
                        else if (h.bakiye > 0)//Bakiye 0 dan büyükse (Örneğin bakiye 30tl müşteri 80 tl kalan 50tlyi ekbakiyeden çekeceği durumda burası çalışır.
                        {
                            if ((miktar - h.bakiye) <= h.ekBakiye) //Çekilmek istenen bir kısmı bakiyede var ve diğer kısmı ek bakiyeden çekilecekse ek bakiye limiti yeterliyse
                            {
                                decimal bakiyedenCekilen, ekBakiyedenCekilen;

                                bakiyedenCekilen = h.bakiye; //Bakiyedeki miktar alınıyor
                                h.bakiye = 0; //Bakiye sıfırlanıyor
                                ekBakiyedenCekilen = miktar - bakiyedenCekilen; //ekBakiyeden çekilecek para hesaplanıyor
                                h.ekBakiye -= ekBakiyedenCekilen; //Çekilen para ekBakiyeden eksiltiliyor.
                                h.gunlukLimit -= bakiyedenCekilen + ekBakiyedenCekilen; //Çekilen parayı günlük limitten düşüyoruz
                                MessageBox.Show("'" + bakiyedenCekilen + "' TL bakiyeden, '" + ekBakiyedenCekilen + "' TL Ek Bakiyeden çekilmiştir.");
                                rapor = ("'" + bakiyedenCekilen + "' TL bakiyeden, '" + ekBakiyedenCekilen + "' Tl Ek Bakiyeden çekilmiştir.");
                                tarih = DateTime.Today;
                                h.RaporEkle(rapor, tarih); //Hesap Özetine yapılan işlem ve tarihi gönderiliyor
                            }
                            else
                                MessageBox.Show("Ek bakiye limitiniz işlem için yeterli değildir.");
                        }
                        else //Bakiyenin 0 olduğu ve çekilmek istenen miktarın ek bakiyeden çekileceği durumda çalışır.
                        {
                            if (h.ekBakiye >= miktar) //Çekilmek istenen miktar için ek bakiye limiti yeterliyse 
                            {
                                h.ekBakiye -= miktar;
                                h.gunlukLimit -= miktar; //Çekilen parayı günlük limitten düşüyoruz

                                MessageBox.Show("'" + miktar + "' TL ek bakiyeden çekilmiştir.");
                                rapor = "'" + miktar + "' TL ek bakiyeden çekilmiştir.";
                                tarih = DateTime.Today;
                                h.RaporEkle(rapor, tarih); //Hesap Özetine yapılan işlem ve tarihi gönderiliyor
                            }
                            else
                                MessageBox.Show("Ek bakiye limitiniz işlem için yeterli değildir.");
                        }
                    }
                    else
                        MessageBox.Show("Çekmek istediğiniz miktar kalan günlük para çekme limitinizden fazla olamaz.");
                }
                else
                    MessageBox.Show("Günlük para çekme limitiniz dolmuştur.");
            }
            else //Belirlenen ek bakiyeden fazla para çekilmek istenirse
                MessageBox.Show("Ek Bakiye'den para çekme limitiniz dolmuştur.");
        }
    }
}
