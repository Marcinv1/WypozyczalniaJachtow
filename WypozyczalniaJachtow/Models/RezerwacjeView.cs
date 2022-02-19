using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WypozyczalniaJachtow.Models
{
    public class RezerwacjeView
    {
            public int ID { get; set; }
            public int JachtID { get; set; }
            public string Imie { get; set; }
            public string NrTel { get; set; }
            public DateTime Od { get; set; }
            public DateTime DO { get; set; }
            public bool Zaliczka { get; set; }
    }
}