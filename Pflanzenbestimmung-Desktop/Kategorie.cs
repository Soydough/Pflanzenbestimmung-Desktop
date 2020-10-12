using System;

namespace Pflanzenbestimmung_Desktop
{
#pragma warning disable CS0618 // Nur damit VS nicht nervt
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

        public string Kategorien { get {return kategorie;} set {kategorie = value;} }

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
        [Obsolete("Bitte stattdessen wirdFürWerkGewertet verwenden")]
        public int werker_gewertet;

        
        public int imQuiz;

        public bool wirdFürGalaAngezeigt
        {
            get
            {
                return anzeige_gartenlandschaftbau != 0;
            }
            set
            {
                anzeige_gartenlandschaftbau = value.ToInt();
            }
        }

        public bool wirdFürZierAngezeigt
        {
            get
            {
                return anzeige_ziergartenbau != 0;
            }
            set
            {
                anzeige_ziergartenbau = value.ToInt();
            }
        }

        public bool wirdFürWerkGewertet
        {
            get
            {
                return werker_gewertet != 0;
            }
            set
            {
                werker_gewertet = value.ToInt();
            }
        }
    }
}
