using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem
{
    internal class Urun
    {
        public int Id {  get; set; }
        public string Ad {  get; set; }
        public decimal Fiyat {  get; set; }
        private int Stok {  get; set; }

        public Urun (int id, string ad, decimal fiyat, int stok)
        {
            Id = id;
            Ad = ad;
            Fiyat = fiyat;
            Stok = stok;
        }
        public int StokGetir()
        {
            return Stok;
        }

        public void StokArttir(int miktar)
        {
            if(miktar > 0)
            {
                Stok += miktar;
            }
            else
            {
                Console.WriteLine("Miktar sıfırdan büyük olmalıdır.");
            }
        }

        public void StokAzalt(int miktar)
        {
            if (miktar > 0 && miktar<= Stok)
            {
                Stok -= miktar;
            }
            else
            {
                Console.WriteLine("Yeterli stok yok.");
            }
        }
       
    }
}
