using System;
using System.Runtime.CompilerServices;

namespace Pflanzenbestimmung_Desktop
{
    public static class KategorieAbfrageExt
    {
        public static KategorieAbfrage FindeKategorie(this KategorieAbfrage[] arr, string name)
        {
            foreach(KategorieAbfrage k in arr)
            {
                if (k.kategorie_name == name)
                    return k;
            }
            return null;
        }
    }

    public class KategorieAbfrage
    {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
        public int kategorie_id;
        public string kategorie_name;
        public int abfrage;
        public string antwort;

        [Obsolete("Bitte stattdessen wirdFürGalaAngezeigt verwenden.")]
        public int anzeige_zier;
        [Obsolete("Bitte stattdessen wirdFürZierAngezeigt verwenden")]
        public int anzeige_gala;
        [Obsolete("Bitte stattdessen wirdFürWerkAngezeigt verwenden")]
        public int wertung_werker;
        [Obsolete("Bitte stattdessen IstImQuiz verwenden (aber wenn nicht ist auch nicht schlimm :) )")]
        public int imQuiz;

        public bool wirdFürGalaAngezeigt
        {
            get
            {
                return anzeige_gala != 0;
            }
        }

        public bool wirdFürZierAngezeigt
        {
            get
            {
                return anzeige_zier != 0;
            }
        }

        public bool wirdFürWerkGewertet
        {
            get
            {
                return wertung_werker != 0;
            }
        }

        public bool IstImQuiz
        {
            get
            {
                return imQuiz != 1;
            }
        }

        public string gegebeneAntwort;
    }
}
