using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WypozyczalniaJachtow.Models
{
    public class JachtyView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Zdjecie { get; set; }
        public string Port { get; set; }
        public Nullable<int> IloscMiejsc { get; set; }
        public Nullable<decimal> Dlugosc { get; set; }
        public Nullable<decimal> Szerokosc { get; set; }
        public string Silnik { get; set; }
        public Nullable<int> Koszt { get; set; }
        public string Wyposazenie { get; set; }
        public string TypJachtu { get; set; }
    }
}
