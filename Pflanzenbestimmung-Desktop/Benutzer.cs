using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    public class Benutzer
    {
        int ausbilderId;
        int ausbildungsartId;
        int fachrichtungId;
        string benutzername;
        string name;
        string vorname;
        int pruefung;

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

        bool IstGueltig()
        {
            return ausbilderId >= 0;
        }

        public static Benutzer ungueltigerBenutzer = new Benutzer(-1);
    }
}
