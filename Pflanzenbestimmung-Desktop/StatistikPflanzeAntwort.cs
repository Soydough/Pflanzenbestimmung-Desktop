using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    public class StatistikPflanzeAntwort
    {
        public string kategorie;
        public string eingabe;
        public string korrekt;
        [Obsolete("Bitte stattdessen WirdFürWerkerVerwendet verwenden")]
        public int wertung_werker;

        public bool WirdFürWerkGewertet
        {
            get
            {
                return wertung_werker != 0;
            }
            set
            {
                wertung_werker = value.ToInt();
            }
        }
    }
}
