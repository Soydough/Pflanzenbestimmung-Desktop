using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace Pflanzenbestimmung_Desktop
{
    public class API_Anbindung
    {
        //private readonly string url = "http://localhost/dbSchnittstelle.php";
        private readonly string url = "http://10.33.11.142/API/dbSchnittstelle.php";
        //private readonly string url = "http://localhost/pflanzenbestimmung/api/dbSchnittstelle.php";

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

        public Pflanzenbild[] BekommePflanzenbilder(int IDp)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getPBilder",
                        ["IDp"] = IDp.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<Pflanzenbild[]>(responseString);
                }
            }
            catch { }
            return null;
        }


        public Pflanzenantwort[] BekommePflanzenantworten()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getPBilder",
                        //["IDpb"] = IDpb.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    //return JsonConvert.DeserializeObject<Pflanzenbild[]>(responseString);
                }
            }
            catch { }
            return null;
        }

        public List<int[]> BekommeQuizPersonZuweisungen(int IDa)
        {
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
                    values["Bild"] = MySql.Data.MySqlClient.MySqlHelper.EscapeString(bild.GetString());

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch { }
        }




        public bool BenutzerErstellen(string benutzername, string passwort,string name, string vorname, string ausbilderBenutzername, int ausbildungsart, int fachrichtung, int ausbilder)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["method"] = "createAzubi";
                    values["User"] = benutzername;
                    values["PW"] = passwort;
                    values["Name"] = name;
                    values["Vorname"] = vorname;
                    values["IDaa"] = ausbildungsart.ToString();
                    values["IDf"] = fachrichtung.ToString();
                    values["IDab"] = ausbilder.ToString();

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    BenutzerTemplate[] benutzerTempArr = JsonConvert.DeserializeObject<BenutzerTemplate[]>(responseString);
                    BenutzerTemplate b = benutzerTempArr[0];
                    b.nutzername = benutzername;
                    return true;
                }
            }
            catch
            {
            }

            return !true;
        }


        public Ausbildungsart[] BekommeAusbildungsart()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getAusbildungsart",
                        // ["IDaB"] = IDaB.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    // return JsonConvert.DeserializeObject<Ausbildungsart[]>(responseString);
                }
            }
            catch { }
            return null;
        }

        public Fachrichtung[] BekommeFachrichtung()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getFachrichtung",
                        //["IDpb"] = IDpb.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    //  return JsonConvert.DeserializeObject<Fachrichtung[]>(responseString);
                }
            }
            catch { }
            return null;
        }
    }
}
