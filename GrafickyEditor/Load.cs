using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafickyEditor
{
    //slouží pro předávání informací mezi okny
    interface INazev
    {
        string Nazev { get; set; }
    }
    public class Load : INazev
    {
        public string Nazev { get; set; }
        public DateTime Zmena { get; set; }
        public Load(string Nazev, DateTime Zmena)
        {
            this.Nazev = Nazev;
            this.Zmena = Zmena;
        }
        public Load()
        {

        }
    }
}
