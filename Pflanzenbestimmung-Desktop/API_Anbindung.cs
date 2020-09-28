using Dirk.Warnsholdt.Helper.ByteExt;
using Dirk.Warnsholdt.Helper.ArrayExt;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
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

        public Benutzer Login(string benutzername, string passwort)
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

                    BenutzerTemplate[] benutzerTempArr = JsonConvert.DeserializeObject<BenutzerTemplate[]>(responseString);
                    BenutzerTemplate b = benutzerTempArr[0];
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

        public T[] Bekommen<T>(string parName = "null")
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();

                    string methodStr;

                    if (parName.Equals("null"))
                    {
                        methodStr = typeof(T).Name + "n";
                    }
                    else
                    {
                        methodStr = parName;
                    }
                    
                    values["method"] = "get" + methodStr;

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<T[]>(responseString);
                }
            }
            catch { }
            return new T[0];
        }

        public Pflanzenbild BekommePflanzenbild(int IDpb)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();

                    values["method"] = "getPBild";
                    values["IDpb"] = IDpb.ToString();

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<Pflanzenbild>(responseString);
                }
            }
            catch { }
            return null;
        }

        public void BildHochladen(int IDp, byte[] bild)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();

                    values["method"] = "createPBild";
                    values["IDp"] = IDp.ToString();
                    //values["Bild"] = bild.ToBinaryString();
                    values["Bild"] = bild.ToSeperatedString(",", "", "");

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch { }
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

                    BenutzerTemplate[] benutzerTempArr = JsonConvert.DeserializeObject<BenutzerTemplate[]>(responseString);
                    BenutzerTemplate b = benutzerTempArr[0];
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
