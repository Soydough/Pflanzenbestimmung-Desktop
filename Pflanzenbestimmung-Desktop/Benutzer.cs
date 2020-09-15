using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    public class Benutzer
    {
        public int ausbilderId;
        public int ausbildungsartId;
        public int fachrichtungId;
        public string benutzername;
        public string name;
        public string vorname;
        public int pruefung;

        public Benutzer(int ausbilderId, int ausbildungsartId, int fachrichtungId, string benutzername, string name = null,
            string vorname = null, int pruefung = 0)
        {
            this.ausbilderId = ausbilderId;
            this.ausbildungsartId = ausbildungsartId;
            this.fachrichtungId = fachrichtungId;
            this.benutzername = benutzername;
            this.name = name;
            this.vorname = vorname;
            this.pruefung = pruefung;
        }

        //Wird nur für ungültigen Benutzer verwendet
        public Benutzer(int ausbilderId)
        {
            this.ausbilderId = ausbilderId;
        }

        public static Benutzer ungueltigerBenutzer = new Benutzer(-1);
    }

    public static class BenutzerExt
    {
        public static bool IstGueltig(this Benutzer b)
        {
            return b.ausbilderId >= 0;
        }
    }
}
