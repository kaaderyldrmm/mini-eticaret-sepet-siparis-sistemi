using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem
{
    internal class SepetUrunu
    {
        public Urun Urun { get; set; }
        public int Adet { get; set; }
        public SepetUrunu(Urun urun, int sayi)
        {
            Urun = urun;
            Adet = sayi;
        }
    }
}
