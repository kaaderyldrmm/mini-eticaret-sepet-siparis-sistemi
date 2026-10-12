using OrderingSystem;
int[] urunId = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
string[] urunAdlari = { "Klavye", "Mouse", "Kulaklık", "Şarj Cihazı", "Monitör", "Web Kamera", "Laptop Çantası", "USB Bellek", "Harici Disk", "Mouse Pad" };
decimal[] fiyatlar = { 500m, 300m, 800m, 1200m, 30000m, 1800m, 900m, 350m, 3200m, 300m };
int[] stoklar = { 12, 5, 9, 13, 27, 9, 33, 2, 17,29 };

Urun[] urunler = new Urun[10];
for (int i = 0; i < urunler.Length; i++)
{
    urunler[i] = new Urun(urunId[i], urunAdlari[i], fiyatlar[i], stoklar[i]);
}

Musteri musteri = new Musteri("Ceren");
bool Donsun = true;
int sonrakiSiparisNo = 1002;
while (Donsun)
{
    Console.Clear();
    Console.WriteLine("\n----- E-TİCARET SEPET VE SİPARİŞ SİSTEMİ -----\n");
    Console.WriteLine("1. Ürünleri Gör");
    Console.WriteLine("2. Sepete Ekle");
    Console.WriteLine("3. Sepeti Gör");
    Console.WriteLine("4. Sepetten Çıkar");
    Console.WriteLine("5. Siparişi Tamamla");
    Console.WriteLine("6. Çıkış");

    Console.Write("\nSeçiminiz: ");
    string secim = Console.ReadLine();

    switch (secim)
    {
        case "1":

            Console.WriteLine("\n--------------- Ürün Listesi ----------------\n");
            Console.WriteLine($"{"ID",-6}{"Ürün Adı",-20} {"Fiyat",-12}");
            Console.WriteLine("------------------------------------------------");

            // Ürünleri foreach ile göster
            foreach (var urun in urunler)
            {
                Console.WriteLine($"{urun.Id,-5} {urun.Ad,-20} {urun.Fiyat,-5} TL");
            }
            break;

        case "2":
            Console.Write("Sepete Eklemek İstediğiniz Ürün Id'sini Girin: ");
            int arananUrun = Convert.ToInt32(Console.ReadLine());
            Urun bulunanUrun = urunler.FirstOrDefault(u => u.Id == arananUrun);
            if (bulunanUrun == null)
            {
                Console.WriteLine("Bu Id'ye Ait Ürün Bulunamadı!");
            }
            else
            {
                Console.WriteLine($"{bulunanUrun.Ad} - {bulunanUrun.Fiyat} tl");

                Console.Write("Almak İstediğiniz Adet: ");
                int UrunSayisi = Convert.ToInt32(Console.ReadLine());

                int mevcutStok = bulunanUrun.StokGetir();

                if (UrunSayisi <= 0)
                {
                    Console.WriteLine("Ürün Sayısı Sıfırdan Büyük Olmalıdır");
                } else if (UrunSayisi > mevcutStok)
                {
                    Console.WriteLine("Yeterli Stok Yok");
                }
                else
                {
                    SepetUrunu sepetSatiri = musteri.Sepet.Urunler.FirstOrDefault(su => su.Urun.Id == bulunanUrun.Id);

                    if (sepetSatiri != null)
                    {
                        sepetSatiri.Adet += UrunSayisi;
                        Console.WriteLine("Ürün Sayısı Güncellendi");
                    }
                    else
                    {
                        SepetUrunu yeniSatir = new SepetUrunu(bulunanUrun, UrunSayisi);
                        musteri.Sepet.Urunler.Add(yeniSatir);
                        Console.WriteLine("Ürün Sepete Başarıyla Eklendi...");

                    }
                    bulunanUrun.StokAzalt(UrunSayisi);
                }
            }
            break;
        case "3":
            Console.WriteLine("\n--------- SEPETİM -----------\n");
            if (musteri.Sepet.Urunler.Count == 0)
            {
                Console.WriteLine("Sepetiniz Boş");
            }
            else
            {
                decimal genelToplam = 0;
                foreach (SepetUrunu sepetSatiri in musteri.Sepet.Urunler)
                {
                    decimal satirToplami = sepetSatiri.Urun.Fiyat * sepetSatiri.Adet;

                    Console.WriteLine(
                        $"Ürün: {sepetSatiri.Urun.Ad} " +
                        $"\nBirim Fiyat: {sepetSatiri.Urun.Fiyat}" +
                        $"\nAdet: {sepetSatiri.Adet}" +
                        $"\nToplam Fiyat: {satirToplami:N2} TL");
                    genelToplam += satirToplami;
                }
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"GENEL TOPLAM: {genelToplam:N2} TL");
            }
            break;
        case "4":

            Console.Write("İptal Edilecek Ürün Id: ");
            int iptalEdilen = Convert.ToInt32(Console.ReadLine());
            SepetUrunu Aranan_Urun = musteri.Sepet.Urunler.FirstOrDefault(a => a.Urun.Id == iptalEdilen);
            if (Aranan_Urun == null)
            {
                Console.WriteLine("Bu Ürün Sepetinizde Bulunamadı!..");
            }
            else
            {
                Console.Write($"Sepetteki Ürün:{Aranan_Urun.Urun.Ad}\n");
                Console.Write($"Sepetteki Adet: {Aranan_Urun.Adet}");

                Console.Write("\nSepetten Çıkarmak İstediğiniz Ürün Sayısı: ");
                int İptalAdet = Convert.ToInt32(Console.ReadLine());

                if (İptalAdet <= 0 || İptalAdet > Aranan_Urun.Adet)
                {
                    Console.WriteLine("Geçersiz ürün adedi");
                }
                else if (İptalAdet == Aranan_Urun.Adet)
                {
                    Aranan_Urun.Urun.StokArttir(İptalAdet);
                    musteri.Sepet.Urunler.Remove(Aranan_Urun);
                    Console.WriteLine("Ürün Sepetten Çıkarıldı...");
                }
                else
                {
                    Aranan_Urun.Urun.StokArttir(İptalAdet);
                    Aranan_Urun.Adet -= İptalAdet;
                    Console.WriteLine("\nÜRÜN ADEDİ GÜNCELLENDİ!");
                }
            }
            break;
        case "5":

            if (musteri.Sepet.Urunler.Count == 0)
            {
                Console.WriteLine("Sepetiniz boş!");
            }
            else
            {
                decimal GenelToplam = 0;
                foreach (SepetUrunu satir in musteri.Sepet.Urunler)
                {
                    GenelToplam += satir.Urun.Fiyat * satir.Adet;
                }
                decimal indirimTutari = 0;

                if (GenelToplam > 2500m)
                {
                    indirimTutari = GenelToplam * 15 / 100;
                }
                else if (GenelToplam > 1000m)
                {
                    indirimTutari = GenelToplam * 10 / 100;
                }
                Siparis yeniSiparis = new Siparis(sonrakiSiparisNo,musteri,musteri.Sepet.Urunler,GenelToplam,indirimTutari);

                sonrakiSiparisNo++;
                yeniSiparis.SiparisOzeti();
                musteri.Sepet.Urunler.Clear();

                Console.WriteLine("\nSiparişiniz başarıyla tamamlandı!");
            }

            break;

        case "6":
            Console.WriteLine("ÇIKIŞ YAPILIYOR...");
            Donsun = false;
            break;
        default:
            Console.WriteLine("geçersiz işlem!");
            break;
    }
    if (Donsun)
    {
        Console.Write("\nSeçimi yenilemek için bir tuşa basın:\n");
        Console.ReadKey();
    } 

}
Console.ReadKey();