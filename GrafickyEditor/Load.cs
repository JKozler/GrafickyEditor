using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafickyEditor
{
    interface INazev
    {
        string Nazev { get; set; }
    }
    class Load : INazev
    {
        public string Nazev { get; set; }
        public DateTime Zmena { get; set; }
        public Load(string Nazev)
        {
            this.Nazev = Nazev;
        }
    }
}
