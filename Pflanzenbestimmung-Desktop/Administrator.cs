using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class Administrator : Benutzer
    {
        public Administrator()
        {
        }

        [JsonConstructor]
        public Administrator(int berflag)
        {
            this.berflag = berflag;
            istAdmin = true;
        }

        new public static Administrator fromTempObjekt(BenutzerTemplate temp)
        {
            Administrator a = new Administrator();
            a.nutzername = temp.nutzername;
            a.name = temp.name;
            a.vorname = temp.name;
            a.berflag = temp.berflag;
            a.istAdmin = true;

            return a;
        }
        public override string ToString()
        {
            return nutzername;
        }
    }
}
