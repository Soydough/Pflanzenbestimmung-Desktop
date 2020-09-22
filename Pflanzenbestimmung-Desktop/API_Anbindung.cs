using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace Pflanzenbestimmung_Desktop
{
    public class API_Anbindung
    {
        //private readonly string url = "http://localhost/dbSchnittstelle.php";
        private readonly string url = "http://10.33.11.142/API/dbSchnittstelle.php";

        public API_Anbindung()
        {
        }

        public Benutzer BenutzerBekommen(string benutzername, string passwort)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["method"] = "login";
                    values["User"] = benutzername;
                    values["PW"] = passwort;

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    BenutzerJSONTempObjekt[] benutzerTempArr = JsonConvert.DeserializeObject<BenutzerJSONTempObjekt[]>(responseString);
                    BenutzerJSONTempObjekt b = benutzerTempArr[0];
                    b.nutzername = benutzername;
                    if (b.berflag != -1)
                    {
                        //Admin
                        return Administrator.fromTempObjekt(b);
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

        public List<Pflanze> PflanzenBekommen()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["method"] = "getPflanzen";

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    Pflanze[] pflanzen = JsonConvert.DeserializeObject<Pflanze[]>(responseString);
                    return pflanzen.ToList();
                }
            }
            catch { }
            return new List<Pflanze>();
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
