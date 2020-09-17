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
        public API_Anbindung()
        {
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

        public bool FuegePflanzeHinzu(string gattung, string art, string deutscherName,
            string famname, string herkunft, string bluete, string bluetezeit,
            string blatt, string wuchs, string besonderheiten)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["method"] = "login";
                    values["gattung"] = gattung;
                    values["art"] = art;

                    
                    var response = client.UploadValues("http://localhost/azubi.php", values);
                    var responseString = Encoding.Default.GetString(response);

                    throw new Exception("Noch nicht fertig programmiert!");
                }
            }
            catch
            {
            }

            return false;
        }

        public bool BenutzerErstellen(string benutzername, string passwort, string ausbilderBenutzername, int ausbildungsart, int fachrichtung)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["method"] = "login";
                    values["User"] = benutzername;
                    values["PW"] = passwort;

                    var response = client.UploadValues("http://localhost/azubi.php", values);
                    var responseString = Encoding.Default.GetString(response);

                    BenutzerJSONTempObjekt[] benutzerTempArr = JsonConvert.DeserializeObject<BenutzerJSONTempObjekt[]>(responseString);
                    BenutzerJSONTempObjekt b = benutzerTempArr[0];
                    b.nutzername = benutzername;
                }
            }
            catch
            {
            }

            return false;
        }
    }
}
