using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class API_Anbindung
    {
        //HttpClient client;

        public API_Anbindung()
        {
            //client = new HttpClient();
        }

        public Benutzer BenutzerBekommenAsync(string benutzername, string passwort)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["method"] = "login";
                    values["User"] = benutzername;
                    values["PW"] = passwort;

                    var response = client.UploadValues("http://localhost/dbSchnittstelle.php", values);
                    var responseString = Encoding.Default.GetString(response);

                    BenutzerJSONTempObjekt[] benutzerTempArr = JsonConvert.DeserializeObject<BenutzerJSONTempObjekt[]>(responseString);
                    BenutzerJSONTempObjekt b = benutzerTempArr[0];
                    b.nutzername = benutzername;
                    if (b.berflag != -1)
                    {
                        //Admin
                        return new Administrator(b.berflag);
                    }
                    else
                    {
                        //normaler Benutzer;
                        return Benutzer.fromTempObjekt(b);
                    }

                }
            }
            catch
            {
                return Benutzer.ungueltigerBenutzer;
            }
        }
    }
}
