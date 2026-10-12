using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem
{
    internal class Musteri
    {
        public string Name {  get; set; }
        public Sepet Sepet {  get; set; }
        public Musteri(string name) {

            Name = name;
            Sepet=new Sepet();
        }
    }
}
