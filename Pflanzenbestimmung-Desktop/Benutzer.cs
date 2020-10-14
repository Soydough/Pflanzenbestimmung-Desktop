using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class Benutzer : BenutzerTemplate
    {
        public bool IstGueltig = true;

        //Wird nur für ungültigen Benutzer verwendet
        public Benutzer(int _)
        {
            IstGueltig = false;
        }

        public bool IstGala
        {
            get
            {
                return fachrichtung == 0;
            }
        }

        public bool IstZier
        {
            get
            {
                return fachrichtung == 1;
            }
        }

        public bool IstVollzeit
        {
            get
            {
                return ausbildung == 0;
            }
        }

        public bool IstWerker
        {
            get
            {
                return ausbildung == 1;
            }
        }

        [JsonConstructor]
        public Benutzer()
        {
            if (berflag != -1)
                istAdmin = false;
        }

        public static Benutzer fromTempObjekt(BenutzerTemplate temp)
        {
            Benutzer benutzer = new Benutzer()
            {
                id = temp.id,
                nutzername = temp.nutzername,
                name = temp.name,
                vorname = temp.vorname,
                istAdmin = false
            };
        
            return benutzer;
        }

        public static Benutzer ungueltigerBenutzer = new Benutzer(-1);
    }
}
