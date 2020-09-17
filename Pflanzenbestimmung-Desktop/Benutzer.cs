using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class Benutzer : BenutzerJSONTempObjekt
    {
        public bool istAdmin = false;
        public bool IstGueltig = true;

        //Wird nur für ungültigen Benutzer verwendet
        public Benutzer(int _)
        {
            IstGueltig = false;
        }


        [JsonConstructor]
        public Benutzer()
        {
            if (berflag != -1)
                istAdmin = true;
        }

        public static Benutzer fromTempObjekt(BenutzerJSONTempObjekt temp)
        {
            Benutzer b = new Benutzer();
            b.nutzername = temp.nutzername;
            b.name = temp.name;
            b.vorname = temp.vorname;
            b.istAdmin = false;

            return b;
        }

        public static Benutzer ungueltigerBenutzer = new Benutzer(-1);
    }
}
