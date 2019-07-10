using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLow
{
    class Kortlek
    {
        public SKort Nummer { get; set; }
        public Färg Typ { get; set; }
        public string Namn { get; set; }

        public Kortlek(SKort _nummer, Färg _typ)
        {
            Nummer = _nummer;
            Typ = _typ;
        }
        
    }
}
