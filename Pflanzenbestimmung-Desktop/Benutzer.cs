using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class Benutzer : BenutzerTemplate
    {
        public bool IstGueltig = false;

        //Wird nur für ungültigen Benutzer verwendet
        public Benutzer(int _)
        {
            IstGueltig = true;
        }


        [JsonConstructor]
        public Benutzer()
        {
            if (berflag != -1)
                istAdmin = false;
        }

        public static Benutzer fromTempObjekt(BenutzerTemplate temp)
        {
            Benutzer b = new Benutzer();
            b.id = temp.id;
            b.nutzername = temp.nutzername;
            b.name = temp.name;
            b.vorname = temp.vorname;
            b.istAdmin = false;

            return b;
        }

        public static Benutzer ungueltigerBenutzer = new Benutzer(-1);
    }
}
