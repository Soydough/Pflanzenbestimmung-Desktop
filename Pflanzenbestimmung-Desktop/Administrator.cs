using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    class Administrator : Benutzer
    {
        public Administrator()
        {
        }

        [JsonConstructor]
        public Administrator(int berflag)
        {
            this.berflag = berflag;
        }

        public static Administrator fromTempObjekt(BenutzerJSONTempObjekt temp)
        {
            Administrator a = new Administrator();
            a.nutzername = temp.nutzername;
            a.name = temp.name;
            a.vorname = temp.name;
            a.berflag = temp.berflag;
            a.istAdmin = true;

            return a;
        }
    }
}
