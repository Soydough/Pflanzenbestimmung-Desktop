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
        //private readonly string url = "http://karteigarten.rf.gd/API/dbSchnittstelle.php";

        //private readonly string url = "https://pflanzenbestimmung.000webhostapp.com/dbSchnittstelle.php";

        //it.tsbw
        private readonly string url = "https://pbstsbw.000webhostapp.com/dbSchnittstelle.php";

        //Steven
        //private readonly string url = "http://10.33.11.142/API/dbSchnittstelle.php";

        //Jan
        //private readonly string url = "http://10.33.156.189/API/dbSchnittstelle.php";

        //Dirk
        //private readonly string url = "http://10.33.156.144/API/dbSchnittstelle.php";

        //private readonly string url = "https://karteigarten.000webhostapp.com/API/dbSchnittstelle.php";

        public API_Anbindung()
        {
        }

        public Benutzer Login(string benutzername, string passwort)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "login",
                        ["User"] = benutzername,
                        ["PW"] = passwort
                    };

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
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
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
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
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
                        ["Bild"] = bild.GetString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString.Length > 0)
                    {
                        throw new Exception(responseString);
                    }
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void PflanzenbildLoeschen(int IDpb)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "deletePBild",
                        ["IDpb"] = IDpb.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString.Length > 0)
                    {
                        throw new Exception(responseString);
                    }
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        [Obsolete("Wird nicht mehr benötigt(?)")]
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
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
            return null;
        }

        public void KategorieErstellen(string kategorie, int AnzeigeGala, int AnzeigeZier, int WertungWerker, int imQuiz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createKategorie",
                        ["Kategorie"] = kategorie,
                        ["AnzeigeGala"] = AnzeigeGala.ToString(),
                        ["AnzeigeZier"] = AnzeigeZier.ToString(),
                        ["WertungWerker"] = WertungWerker.ToString(),
                        ["ImQuiz"] = imQuiz.ToString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                    if (responseString == "")
                    {
                        MessageBox.Show("Kategorie erfolgreich hinzugefügt. Bitte aktualisieren Sie die Pflanzen bevor Sie die Kategorie benutzen.");
                    }
                    else
                    {
                        if (responseString.Contains("Duplicate entry"))
                        {
                            MessageBox.Show("Diese Kategorie ist schon vorhanden. Bitte geben Sie einen anderen Namen ein.");
                        }
                        else
                        {
                            MessageBox.Show("Ein unerwarteter fehler ist aufgeträten, bitte versuchen Sie es später erneut oder Kontaktieren Sie den Programmadministrator.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }


        public void KategorieAktualisieren(int id, string kategorie, int AnzeigeGala, int AnzeigeZier, int WertungWerker, int imQuiz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "updateKategorie",
                        ["IDk"] = id.ToString(),
                        ["Kategorie"] = kategorie,
                        ["AnzeigeGala"] = AnzeigeGala.ToString(),
                        ["AnzeigeZier"] = AnzeigeZier.ToString(),
                        ["WertungWerker"] = WertungWerker.ToString(),
                        ["imQuiz"] = imQuiz.ToString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }

            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void KategorieLoeschen(int IDk)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "deleteKategorie",
                        ["IDk"] = IDk.ToString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void BenutzerErstellen(bool admin, string benutzername, string passwort, string name, string vorname, int ausbildungsart, int fachrichtung, int ausbilder, int pruefung, int groeßeQuizArt)
        {

            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["User"] = benutzername,
                        ["PW"] = passwort,
                        ["Name"] = name,
                        ["Vorname"] = vorname
                    };
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
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void PflanzeErstellen(bool istGala, bool istZier, List<(int, string)> liste)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection()
                    {
                        ["method"] = "createPflanze",
                        ["zierbau"] = istZier.ToInt().ToString(),
                        ["galabau"] = istGala.ToInt().ToString(),
                        //["dbgattung"] = gattung,
                        //["dbart"] = art,
                        //["dbdename"] = dename,
                        //["dbfamname"] = famname,
                        //["herkunft"] = herkunft,
                        //["bluete"] = bluete,
                        //["bluetezeit"] = bluetezeit,
                        //["blatt"] = blatt,
                        //["wuchs"] = wuchs,
                        //["besonderheiten"] = besonderheiten
                    };

                    for (int i = 0; i < liste.Count && i < Main.kategorien.Count; i++)
                    {
                        values[$"id_kategorie_{i}"] = liste[i].Item1.ToString();
                        values[$"antwort_{i}"] = liste[i].Item2;
                    }

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString.Length > 0)
                    {
                        throw new Exception(responseString);
                    }
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void PflanzeAktualisieren(int id_pflanze, bool istGala, bool istZier, List<(int, string)> liste)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection()
                    {
                        ["method"] = "updatePflanze",
                        ["id_pflanze"] = id_pflanze.ToString(),
                        ["galabau"] = istGala.ToInt().ToString(),
                        ["istzier"] = istZier.ToInt().ToString()
                    };

                    for (int i = 0; i < liste.Count && i < Main.kategorien.Count; i++)
                    {
                        values[$"id_kategorie_{i}"] = liste[i].Item1.ToString();
                        values[$"antwort_{i}"] = liste[i].Item2;
                    }

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString.Length > 0)
                    {
                        throw new Exception(responseString);
                    }
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void LoeschePflanze(int IDp)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection()
                    {
                        ["method"] = "deletePflanze",
                        ["IDp"] = IDp.ToString(),
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString.Length > 0)
                    {
                        throw new Exception(responseString);
                    }
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
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
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
            return null;
        }

        public AzubiStatistik BekommeStatistik(int IDs)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getStatistik",
                        ["IDs"] = IDs.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<AzubiStatistik[]>(responseString)[0];
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
            return null;
        }

        public void ErstelleStatistik(int IDaz, string FQuote, TimeSpan Zeit, int IDp)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createStatistik",
                        ["IDaz"] = IDaz.ToString(),
                        ["FQuote"] = FQuote,
                        ["Zeit"] = Zeit.ToString(@"hh\:mm\:ss"),
                        ["IDp"] = IDp.ToString()
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void ErstelleEinzelStatistik(int IDs, int IDk, int IDp, string Eingabe)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createStatEinzel",
                        ["IDs"] = IDs.ToString(),
                        ["IDk"] = IDk.ToString(),
                        ["IDp"] = IDp.ToString(),
                        ["Eingabe"] = Eingabe
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void BenutzerAendern(bool admin, int id, string benutzername, string passwort, string name, string vorname, int ausbildungsart, int fachrichtung, int ausbilder, int pruefung, int groeßeQuizArt)
        {
            try
            {
                using (var client = new WebClient())
                {
                    Azubis Azubidaten = new Azubis();
                    var values = new NameValueCollection { };
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
                        values["method"] = "updateAzubi";
                        values["IDaz"] = id.ToString();
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
                    else
                    {
                        values["IDad"] = id.ToString();
                        values["method"] = "updateAdmin";
                    }
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        private void VerbindungsFehler(Exception e)
        {
#if DEBUG
            MessageBox.Show(e.Message, e.HResult.ToString());
#else
            try
            {
                MessageBox.Show("Ein Fehler ist aufgetreten! Mögliche Ursachen:\n" +
                    "   • Es konnte keine Verbindung zur Datenbank hergestellt werden", ((Win32Exception)e.InnerException).ErrorCode.ToString());
            }
            catch
            {
                MessageBox.Show("Ein Fehler ist aufgetreten! Mögliche Ursachen:\n" +
                    "   • Es konnte keine Verbindung zur Datenbank hergestellt werden");
            }
#endif
        }

        public void QuizArtErstellen(string quizName, string quizGroeße)
        {
            try
            {
                using (var client = new WebClient())
                {
                    Azubis Azubidaten = new Azubis();
                    var values = new NameValueCollection
                    {
                        ["method"] = "createQuizArt",
                        ["Quizname"] = quizName,
                        ["Groeße"] = quizGroeße
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "");
            }
        }

        public void BenutzerLoeschen(int id, bool istAdmin)
        {

            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection { };
                    if (!istAdmin)
                    {
                        values["method"] = "deleteAzubi";
                        values["IDaz"] = id.ToString();
                        Azubis Azubidaten = new Azubis();
                    }
                    else
                    {
                        values["method"] = "deleteAdmin";
                        values["IDad"] = id.ToString();
                    }
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public QuizArt BekommeQuizArt(int IDaz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getQuizArt",
                        ["IDaz"] = IDaz.ToString(),
                    };

                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<QuizArt[]>(responseString)[0];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "");
            }
            return null;
        }

        public Abgefragt[] BekommeAbgefragt(int IDaz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "getAbgefragt",
                        ["IDa"] = IDaz.ToString() //Keine Ahnung, ob das IDaz sein sollte, aber jedenfalls ist es nicht so. Dies ist ein "call out". Ich calle die Schnittstelle out. ganz recht.
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    return JsonConvert.DeserializeObject<Abgefragt[]>(responseString);
                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }

            return null;
        }

        public void AbgefragtErstellen(int IDaz, int IDp, int Counter, bool Gelernt)
        {
            AbgefragtErstellen(IDaz, IDp, Counter, Gelernt.ToInt());
        }
        public void AbgefragtErstellen(int IDaz, int IDp, int Counter, int Gelernt)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "createAbgefragt",
                        ["IDaz"] = IDaz.ToString(),
                        ["IDp"] = IDp.ToString(),
                        ["Counter"] = Counter.ToString(),
                        ["Gelernt"] = Gelernt.ToString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }

            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void AbgefragtAktualisieren(int IDaz, int IDp, int Counter, bool Gelernt)
        {
            AbgefragtAktualisieren(IDaz, IDp, Counter, Gelernt.ToInt());
        }
        public void AbgefragtAktualisieren(int IDaz, int IDp, int Counter, int Gelernt)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "updateAbgefragt",
                        ["IDaz"] = IDaz.ToString(),
                        ["IDp"] = IDp.ToString(),
                        ["Counter"] = Counter.ToString(),
                        ["Gelernt"] = Gelernt.ToString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);
                }
            }

            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

        public void LoescheKategorie(int id)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "deleteKategorien",
                        ["IDk"] = id.ToString()
                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);


                }
            }
            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }


        public void UpdateKategorie(int id, string kategorie, int anzeigeGala, int anzeigeZier, int wertungWerker, int imQuiz)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["method"] = "updateKategorie",
                        ["IDk"] = id.ToString(),
                        ["Kategorie"] = kategorie,
                        ["AnzeigeGala"] = anzeigeGala.ToString(),
                        ["AnzeigeZier"] = anzeigeZier.ToString(),
                        ["WertungWerker"] = wertungWerker.ToString(),
                        ["ImQuiz"] = imQuiz.ToString()

                    };
                    var response = client.UploadValues(url, values);
                    var responseString = Encoding.Default.GetString(response);

                    if (responseString != null || responseString != "")
                    {
                        MessageBox.Show(responseString);
                    }
                }
            }

            catch (Exception e)
            {
                VerbindungsFehler(e);
            }
        }

    }//End Class
}//End Namespace
