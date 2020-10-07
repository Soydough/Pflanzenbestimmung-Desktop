
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Windows;

namespace Pflanzenbestimmung_Desktop
{
    public class API_Anbindung
    {
        //private readonly string url = "http://localhost/dbSchnittstelle.php";
        private readonly string url = "http://10.33.11.142/API/dbSchnittstelle.php";
        //private readonly string url = "http://localhost/pflanzenbestimmung/api/dbSchnittstelle.php";
        //private readonly string url = "http://karteigarten.rf.gd/API/dbSchnittstelle.php";

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
            catch (System.Exception e) { MessageBox.Show("" + e); }
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

        public QuizPZuweisung[] BekommeQuizPZuweisung(int IDaz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getQuizPZuweisung",
                        ["IDaz"] = IDaz.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<QuizPZuweisung[]>(responseString);
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
                    var values = new NameValueCollection
                    {
                        ["method"] = "createPBild",
                        ["IDp"] = IDp.ToString(),
                        //["Bild"] = bild.GetString()
                        ["Bild"] = MySql.Data.MySqlClient.MySqlHelper.EscapeString(bild.GetString())
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch { }
        }

        public void KategorieErstellen(string kategorie)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createKategorie",
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch { }
        }

        public void BenutzerErstellen(bool admin, string benutzername, string passwort, string name, string vorname, int ausbildungsart, int fachrichtung, int ausbilder, int pruefung, int groeßeQuizArt)
        {

            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["User"] = benutzername;
                    values["PW"] = passwort;
                    values["Name"] = name;
                    values["Vorname"] = vorname;
                    if (!admin)
                    {
                        values["method"] = "createAzubi";
                        int ausbildungarten = ausbildungsart + 1;
                        int fachrichtungen = fachrichtung + 1;
                        int ausb = ausbilder + 1;
                        int quizgroeßeArt = groeßeQuizArt + 1;
                        values["IDaa"] = ausbildungarten.ToString();
                        values["IDf"] = fachrichtungen.ToString();
                        values["IDab"] = ausb.ToString();
                        values["IDqa"] = quizgroeßeArt.ToString();
                        values["Pruefung"] = pruefung.ToString();
                    }
                    else
                    {
                        values["method"] = "createAdmin";
                    }
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString != null || responseString != "")
                    {
                        MessageBox.Show(responseString);
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e + "");
            }
        }

        public void PflanzeErstellen(string gattung, string art, string dename,
            string famname, string herkunft, string bluete, string bluetezeit,
            string blatt, string wuchs, string besonderheiten)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection()
                    {
                        ["dbgattung"] = gattung,
                        ["dbart"] = art,
                        ["dbdename"] = dename,
                        ["dbfamname"] = famname,
                        ["herkunft"] = herkunft,
                        ["bluete"] = bluete,
                        ["bluetezeit"] = bluetezeit,
                        ["blatt"] = blatt,
                        ["wuchs"] = wuchs,
                        ["besonderheiten"] = besonderheiten
                    };
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public AzubiStatistik[] BekommeStatistikenListe(int IDaz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getStatList",
                        ["IDaz"] = IDaz.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<AzubiStatistik[]>(responseString);
                }
            }
            catch
            {
                MessageBox.Show("Ein Fehler ist aufgetreten! Bitte stellen sie sicher, dass eine Internetverbindung besteht. Danke. Dies ist das Ende der Fehlermeldung.");
            }
            return null;
        }

        public AzubiStatistik[] BekommeStatistik(int IDs)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getStatistik",
                        ["IDaz"] = IDs.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<AzubiStatistik[]>(responseString);
                }
            }
            catch { }
            return null;
        }

        public void ErstelleStatistik(int IDaz, int FQuote, TimeSpan Zeit, int IDp)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createStatistik",
                        ["IDaz"] = IDaz.ToString(),
                        ["FQuote"] = FQuote.ToString(),
                        ["Zeit"] = Zeit.ToString("yyyy-MM-dd HH:mm:ss"),
                        ["IDp"] = IDp.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch { }
        }

        public void ErstelleEinzelStatistik(int IDs, int IDk, int IDp, string Eingabe)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createStatistik",
                        ["IDs"] = IDs.ToString(),
                        ["IDk"] = IDk.ToString(),
                        ["IDp"] = IDp.ToString(),
                        ["Eingabe"] = Eingabe
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch { }
        }

        public void BenutzerAendern(bool admin, int id, string benutzername, string passwort, string name, string vorname, int ausbildungsart, int fachrichtung, int ausbilder, int pruefung, int groeßeQuizArt)
        {
            try
            {
                using (var client = new WebClient())
                {
                    Azubis Azubidaten = new Azubis();
                    var values = new NameValueCollection();
                    values["method"] = "updateAzubi";
                    values["IDaz"] = id.ToString();
                    if (benutzername != null)
                    {
                        values["User"] = benutzername;
                    }
                    if (passwort != "")
                    {
                        values["PW"] = passwort;
                    }
                    if (name != null)
                    {
                        values["Name"] = name;
                    }
                    if (vorname != null)
                    {
                        values["Vorname"] = vorname;
                    }
                    if (!admin)
                    {

                        if (ausbildungsart != -1)
                        {
                            int ausbildungarten = ausbildungsart + 1;
                            values["IDaa"] = ausbildungarten.ToString();
                        }
                        if (fachrichtung != -1)
                        {
                            int fachrichtungen = fachrichtung + 1;
                            values["IDf"] = fachrichtungen.ToString();
                        }
                        if (ausbilder != -1)
                        {
                            int ausb = ausbilder + 1;
                            values["IDab"] = ausb.ToString();
                        }
                        if (groeßeQuizArt != -1)
                        {
                            int art = groeßeQuizArt + 1;
                            values["IDqa"] = art.ToString();
                        }
                        values["Pruefung"] = pruefung.ToString();

                    }
                    //else
                    //{
                    //    values["method"] = "createAdmin";
                    //}
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString != null || responseString != "")
                    {
                        MessageBox.Show(responseString);
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e + "");
            }



        }

        public void QuizArtErstellen(string quizName, string quizGroeße)
        {
            try
            {
                using (var client = new WebClient())
                {
                    Azubis Azubidaten = new Azubis();
                    var values = new NameValueCollection();
                    values["method"] = "createQuizArt";
                    values["Quizname"] = quizName;
                    values["Groeße"] = quizGroeße;

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString != null || responseString != "")
                    {
                        MessageBox.Show(responseString);
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e + "");
            }
        }

    }//End Class
}//End Namespace
