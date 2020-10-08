using System;

namespace Pflanzenbestimmung_Desktop
{
    public class KategorieAbfrage
    {
        public int kategorie_id;
        public string kategorie_name;
        public int abfrage;
        public string antwort;

        [Obsolete("Bitte stattdessen wirdFürGalaAngezeigt verwenden.")]
        public int anzeige_zier;
        [Obsolete("Bitte stattdessen wirdFürZierAngezeigt verwenden")]
        public int anzeige_gala;
        [Obsolete("Bitte stattdessen wirdFürZierAngezeigt verwenden")]
        public int wertung_werker;

        public bool wirdFürGalaAngezeigt
        {
            get
            {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
                return anzeige_gala != 0;
#pragma warning restore CS0618 // Nur damit VS nicht nervt
            }
        }

        public bool wirdFürZierAngezeigt
        {
            get
            {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
                return anzeige_zier != 0;
#pragma warning restore CS0618 // Nur damit VS nicht nervt
            }
        }

        public bool wirdFürWerkAngezeigt
        {
            get
            {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
                return wertung_werker != 0;
#pragma warning restore CS0618 // Nur damit VS nicht nervt
            }
        }

        public string gegebeneAntwort;
    }
}
