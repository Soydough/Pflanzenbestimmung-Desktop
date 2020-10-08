using System;

namespace Pflanzenbestimmung_Desktop
{
    public class Kategorie
    {
        /// <summary>
        /// Die ID
        /// </summary>
        public int id;
        /// <summary>
        /// Die Kategorie
        /// </summary>
        public string kategorie;

        /// <summary>
        /// Ob die Kategorie für Gartenlandschaftsbauer angezeigt werden soll
        /// </summary> 
        [Obsolete("Bitte stattdessen wirdFürGalaAngezeigt verwenden.")]
        public int anzeige_gartenlandschaftbau;

        /// <summary>
        /// Ob die Kategorie für Ziergartenbauer angezeigt werden soll
        /// </summary>
        [Obsolete("Bitte stattdessen wirdFürZierAngezeigt verwenden")]
        public int anzeige_ziergartenbau;

        /// <summary>
        /// Ob die Kategorie für den Ausbildungstyp Werk gewertet werden soll. Sicherlich sehr wichtig
        /// </summary>
        [Obsolete("Bitte stattdessen wirdFürWerkAngezeigt verwenden")]
        public int werker_gewertet;

        public bool wirdFürGalaAngezeigt
        {
            get
            {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
                return anzeige_gartenlandschaftbau != 0;
#pragma warning restore CS0618 // Nur damit VS nicht nervt
            }
        }

        public bool wirdFürZierAngezeigt
        {
            get
            {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
                return anzeige_ziergartenbau != 0;
#pragma warning restore CS0618 // Nur damit VS nicht nervt
            }
        }

        public bool wirdFürWerkAngezeigt
        {
            get
            {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
                return werker_gewertet != 0;
#pragma warning restore CS0618 // Nur damit VS nicht nervt
            }
        }
    }
}
