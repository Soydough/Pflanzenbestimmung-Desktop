using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    public class StatistikPflanzeAntwort
    {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
        public string kategorie;
        public string eingabe;
        public string korrekt;
        [Obsolete("Bitte stattdessen WirdFürWerkerVerwendet verwenden")]
        public int wertung_werker;
        public int imQuiz;

        public bool WirdFürWerkGewertet
        {
            get
            {
                return wertung_werker == 1;
            }
            set
            {
                wertung_werker = value.ToInt();
            }
        }

        public bool IstImQuiz
        {
            get
            {
                return imQuiz != 0;
            }
            set
            {
                imQuiz = value.ToInt();
            }
        }
    }
}
