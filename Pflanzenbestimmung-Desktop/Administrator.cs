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
            a.id_ausbilder = temp.id_ausbilder;

            return a;
        }
        public override string ToString()
        {
            return nutzername;
        }


        //string name;
        //string vorname;
        //string nutzername;

        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}
        //public string Vorname
        //{
        //    get { return vorname; }
        //    set { vorname = value; }
        //}
        //public string Nutzername
        //{
        //    get { return nutzername; }
        //    set { nutzername = value; }
        //}

    }
}
