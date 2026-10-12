using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem
{
    internal class Siparis
    {
        public int SiparisNo {  get; set; }
        public Musteri Musteri {  get; set; }
        public List<SepetUrunu> Urunler { get; set; }
        public decimal ToplamTutar {  get; set; }
        public decimal İndirim { get; set; }
        public decimal OdenecekTutar
        {
            get
            {
                return ToplamTutar - İndirim;
            }
        }
        public Siparis(int siparisNo, Musteri musteri, List<SepetUrunu> urunler, decimal toplam, decimal indirim)
        {
            SiparisNo = siparisNo;
            Musteri = musteri;
            Urunler = new List<SepetUrunu>(urunler);
            ToplamTutar = toplam;
            İndirim = indirim;

            {
                SiparisNo = siparisNo;
                Musteri = musteri;

                Urunler = new List<SepetUrunu>();

                foreach (SepetUrunu satir in urunler)
                {
                    SepetUrunu yeniSatir = new SepetUrunu(
                        satir.Urun,
                        satir.Adet);

                    Urunler.Add(yeniSatir);
                }

                ToplamTutar = toplam;
                İndirim = indirim;
            }
        }

    public void SiparisOzeti()
        {
            Console.WriteLine("\n========== SİPARİŞ ÖZETİ ==========\n");
            Console.WriteLine($"Sipariş No: {SiparisNo}");

            Console.WriteLine("\n--- Ürünler ---");

            foreach (SepetUrunu satir in Urunler)
            {
                decimal araToplam = satir.Urun.Fiyat * satir.Adet;

                Console.WriteLine(
                    $"Ürün: {satir.Urun.Ad} | " +
                    $"\nAdet: {satir.Adet} | " +
                    $"\nBirim Fiyat: {satir.Urun.Fiyat:N2} TL | " +
                    $"\nAra Toplam: {araToplam:N2} TL");
            }

            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"İndirim Öncesi Toplam: {ToplamTutar:N2} TL");
            Console.WriteLine($"İndirim: {İndirim:N2} TL");
            Console.WriteLine($"Ödenecek Tutar: {OdenecekTutar:N2} TL");
            Console.WriteLine("===================================");
        }
    }
}
   
