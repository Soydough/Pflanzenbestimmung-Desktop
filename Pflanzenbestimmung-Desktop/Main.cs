using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using static Pflanzenbestimmung_Desktop.Helper;
using System.Runtime.InteropServices;

namespace Pflanzenbestimmung_Desktop
{
    public static class Main
    {
        // Klasse zum Speichern von Variablen, die auf mehreren Seiten gebraucht werden

        #region Variablen
        //Veraltet
        //public static Datenbankverbindung datenbankverbindung = new Datenbankverbindung();

        public static API_Anbindung api_anbindung = new API_Anbindung();

        public static Benutzer benutzer;

        public static Dictionary<int, Ausbildungsart> ausbildungsarten;

        // public static Ausbildungsart[] ausbildungsarten;

        public static Dictionary<int, Fachrichtung> fachrichtungen;

        // public static Fachrichtung[] fachrichtungen;

        public static Dictionary<int, Administrator> ausbilder;

        //  public static Administrator[] ausbilder;

        public static int ausbilderId;

        public static Pflanze[] pflanzen;

        public static Pflanzenbild[] pflanzenbilder;

        public static Dictionary<int, QuizArt> quizArt;

        public static QuizArt azubiQuizArt;

        public static QuizPflanze[] quiz;

        public static List<Kategorie> kategorien;

        public static AzubiStatistik[] azubiStatistiken;

        public static AzubiStatistik azubiStatistik;

        public static Random random = new Random();

        public static Pflanzenbild[] momentanePflanzenbilder;

        public static int momentanePflanzeAusQuiz = -1; // -1: kein Quiz

        public static int momentanePflanzeAusStatistik = -1;

        public static ImageSource fullscreenImage;

        public static QuizPZuweisung[] quizPZuweisungen;

        public static Azubis[] azubi;
        
        public static QuizPZuweisung[] azubiQuizZuweisungen;

        //Zeitpunkt des Beginns des Quiz
        public static DateTime quizAngefangenZeit;

        //Timer für das gesamtes Quiz
        public static Stopwatch quizTimer = new Stopwatch();

        //Einzelstatistiken
        public static StatistikPflanze[] einzelStatistiken;

        public static Abgefragt[] abgefragt;

        public static Dictionary<int, bool> abgefragtZuweisung; //Ja, das gehört so.

        #endregion

        public static void Initialize()
        {
            //datenbankverbindung.BekommeAllePflanzenTest();

            //Platzhalter-Bilder hochladen
            //byte[] platzhalter = File.ReadAllBytes(@"..\..\platzhalter a.jpg");
            //api_anbindung.BildHochladen(1, platzhalter);
            //platzhalter = File.ReadAllBytes(@"..\..\platzhalter b.jpg");
            //api_anbindung.BildHochladen(2, platzhalter);
            //platzhalter = File.ReadAllBytes(@"..\..\platzhalter c.jpg");
            //api_anbindung.BildHochladen(3, platzhalter);

            pflanzen = api_anbindung.Bekommen<Pflanze>();
            kategorien = api_anbindung.Bekommen<Kategorie>().ToList();
        }



        public static void LadeAzubiDaten()
        {
            ausbildungsarten = api_anbindung.Bekommen<Ausbildungsart>("Ausbildungsart").ToDictionary();
            fachrichtungen = api_anbindung.Bekommen<Fachrichtung>("Fachrichtung").ToDictionary();
            ausbilder = api_anbindung.Bekommen<Administrator>("Admins").ToDictionary();
            azubi = api_anbindung.Bekommen<Azubis>("Azubis");
            quizArt = api_anbindung.Bekommen<QuizArt>("QuizArt").ToDictionary();
        }

        public static void LadeAbgefragt()
        {
            abgefragt = api_anbindung.BekommeAbgefragt(benutzer.id);
            abgefragtZuweisung = new Dictionary<int, bool>();

            for(int i = 0; i < abgefragt.Length; i++)
            {
                abgefragtZuweisung.Add(i, abgefragt[i].IstGelernt);
            }
        }

        public static ObservableCollection<Azubis> azubiVerwaltungListe
        {
            get; set;
        }

        public static ObservableCollection<Azubis> InitializeAzubiVerwaltungListe()
        {
            azubiVerwaltungListe = new ObservableCollection<Azubis>();
            for (int i = 0; i < azubi.Length; i++)
            {
                azubiVerwaltungListe.Add(azubi[i]);
            }
            return azubiVerwaltungListe;
        }

        public static ObservableCollection<Kategorie> kategorieVerwaltungListe
        {
            get; set;
        }

        public static ObservableCollection<Kategorie> InitializeKategorieVerwaltungListe()
        {
            kategorieVerwaltungListe = new ObservableCollection<Kategorie>();
            for (int i = 0; i < kategorien.Count; i++)
            {
                kategorieVerwaltungListe.Add(kategorien[i]);
                if (kategorien[i].im_quiz == 0)
                {
                    kategorien[i].quizAuswahl = "Nein";
                }
                else
                {
                    kategorien[i].quizAuswahl = "Ja";
                }
            }
            return kategorieVerwaltungListe;
        }      

        public static ObservableCollection<Administrator> AdminVerwaltungListe
        {
            get; set;
        }

        public static ObservableCollection<Administrator> InitializeAdminVerwaltungListe()
        {
            AdminVerwaltungListe = new ObservableCollection<Administrator>();
            for (int i = 0; i < ausbilder.Count; i++)
            {
                AdminVerwaltungListe.Add(ausbilder[i]);
            }
            return AdminVerwaltungListe;
        }

        public static void LadeStatistiken()
        {
            if (!benutzer.istAdmin)
            {
                azubiStatistiken = api_anbindung.BekommeStatistikenListe(benutzer.id);
            }
        }

        public static void LadeStatistikenHoch()
        {
            int fehlersumme = 0;

            int wenigsteFehler = int.MaxValue;

            int wenigsteFehlerPflanzeId = -1;

            LadeAbgefragt();

            int gesamtSumme = 0;
            for (int i = 0; i < einzelStatistiken.Length; i++)
            {
                int tempFehlerSumme = 0;
                for (int j = 0; j < kategorien.Count; j++)
                {
                    StatistikPflanzeAntwort temp = einzelStatistiken[i].antworten[j];

                    gesamtSumme++;
                    //if (temp.eingabe != temp.korrekt)
                    if (!IstRichtig(temp.eingabe, temp.korrekt))
                    {
                        if (!benutzer.IstWerker)
                        {
                            //Antwort falsch und kein Werker
                            fehlersumme++;
                            tempFehlerSumme++;
                        }
                        else
                        {
                            if(!temp.WirdFürWerkGewertet)
                            {
                                //Antwort falsch, aber Werker
                                gesamtSumme--;
                            }
                            else
                            {
                                //Antwort falsch und Werker, Kategorie wird aber trotzdem gezählt
                                fehlersumme++;
                                tempFehlerSumme++;
                            }
                        }
                    }
                }

                //Wenn Pflanze richtig war, Abgefragt speichern
                if (tempFehlerSumme == 0) {
                    bool found = false;
                    for (int j = 0; j < abgefragt.Length; j++)
                    {
                        if (abgefragt[j].IDp == einzelStatistiken[i].id_pflanze)
                        {
                            abgefragt[0].Counter++;
                            bool gelernt = abgefragt[0].Counter >= 7;
                            api_anbindung.AbgefragtAktualisieren(benutzer.id, abgefragt[j].IDp, abgefragt[j].Counter, gelernt);
                            found = true;
                        }
                    }

                    //Wenn es noch keinen passenden Abgefrag-Eintrag gibt; erstellen
                    if (!found)
                    {
                        api_anbindung.AbgefragtErstellen(benutzer.id, einzelStatistiken[i].id_pflanze, 1, false);
                    }
                }

                if(tempFehlerSumme < wenigsteFehler)
                {
                    wenigsteFehler = tempFehlerSumme;
                    wenigsteFehlerPflanzeId = einzelStatistiken[i].id_pflanze;
                }
            }

            //int fehlerquote = (int)(100.0 * kategorien.Count / fehlersumme);
            string fehlerquote = fehlersumme + "/" + (gesamtSumme);
            api_anbindung.ErstelleStatistik(benutzer.id, fehlerquote, quizTimer.Elapsed, wenigsteFehlerPflanzeId);

            LadeStatistiken();
            azubiStatistik = azubiStatistiken[azubiStatistiken.Length - 1];

            for (int i = 0; i < einzelStatistiken.Length; i++)
            {
                for (int j = 0; j < kategorien.Count; j++)
                {
                    //statistiken.Length ist die neuste, also hoffentlich die gerade hinzugefügte?
                    api_anbindung.ErstelleEinzelStatistik(azubiStatistik.id_statistik, j + 1, einzelStatistiken[i].id_pflanze, einzelStatistiken[i].antworten[j].eingabe);
                }
            }
        }

        public static void QuizBekommen()
        {
            LadeAbgefragt();
            if (benutzer.istAdmin)
            {
                MessageBox.Show("Ihnen ist kein Quiz zugewiesen!");
                return;
            }

            azubiQuizArt = api_anbindung.BekommeQuizArt(benutzer.id);
            int anzahl = azubiQuizArt.quizgröße;
            //quiz = new QuizPflanze[anzahl];
            List<QuizPflanze> tempQuiz = new List<QuizPflanze>();

            azubiQuizZuweisungen = api_anbindung.BekommeQuizPZuweisung(benutzer.id);

            if (azubiQuizZuweisungen.IsNullOrEmpty())
            {
                MessageBox.Show("Ihnen ist kein Quiz zugewiesen!");
                return;
            }
            else
            {
                einzelStatistiken = new StatistikPflanze[anzahl];
                //List<Pflanze> tempPflanzen = ((Pflanze[])pflanzen.Clone()).ToList();
                List<Pflanze> tempPflanzen = new List<Pflanze>();

                for (int i = 0; i < azubiQuizZuweisungen.Length; i++)
                {
                    int index = azubiQuizZuweisungen[i].id_pflanze - 1;

                    bool gelernt = false;
                    abgefragtZuweisung.TryGetValue(index, out gelernt);

                    if (!gelernt && ((benutzer.IstGala && pflanzen[index].IstGala) || (benutzer.IstZier && pflanzen[index].IstZier)))
                        tempPflanzen.Add(pflanzen[index]);
                }

                if(tempPflanzen.IsNullOrEmpty())
                {
                    MessageBox.Show("Sie haben bereits alle zugewiesenen Pflanzen gelernt!\n" +
                        "\n" +
                        "Sie können stattdessen ein zufälligen Quiz starten", "Herzlichen Glückwunsch");
                    return;
                }

                for (int i = 0; i < anzahl; i++)
                {
                    //quiz[i] = new QuizPflanze();
                    int index = random.Next(tempPflanzen.Count);
                    //quiz[i].pflanze = tempPflanzen[index];
                    tempQuiz.Add(new QuizPflanze());
                    tempQuiz[i].pflanze = tempPflanzen[index];

                    //Enfernt die hinzugefügte Pflanze, damit jede Pflanzen nur einmal vorkommt
                    tempPflanzen.RemoveAt(index);
                    //Beendet den for-loop, wenn keine Pflanzen mehr verfügbar sind
                    if (tempPflanzen.IsNullOrEmpty())
                    {
                        break;
                    }
                }

                quiz = tempQuiz.ToArray();
                einzelStatistiken = new StatistikPflanze[quiz.Length];

            }
        }

        public static void ZufälligesQuizBekommen()
        {
            if (benutzer.istAdmin)
            {
                MessageBox.Show("Ihnen ist kein Quiz zugewiesen!");
                return;
            }

            quizArt = api_anbindung.Bekommen<QuizArt>("QuizArt").ToDictionary();
            int anzahl = quizArt[benutzer.id].quizgröße;
            //quiz = new QuizPflanze[anzahl];
            List<QuizPflanze> tempQuiz = new List<QuizPflanze>();

            azubiQuizZuweisungen = api_anbindung.BekommeQuizPZuweisung(benutzer.id);

            einzelStatistiken = new StatistikPflanze[anzahl];
            //List<Pflanze> tempPflanzen = ((Pflanze[])pflanzen.Clone()).ToList();
            List<Pflanze> tempPflanzen = new List<Pflanze>();

            for (int i = 0; i < anzahl; i++)
            {
                tempPflanzen.Add(pflanzen[azubiQuizZuweisungen[i].id_pflanze - 1]);
            }

            for (int i = 0; i < quiz.Length; i++)
            {
                quiz[i] = new QuizPflanze();
                int index = random.Next(tempPflanzen.Count);
                //quiz[i].pflanze = tempPflanzen[index];
                tempQuiz.Add(new QuizPflanze());
                tempQuiz[i].pflanze = tempPflanzen[index];

                //Enfernt die hinzugefügte Pflanze, damit jede Pflanzen nur einmal vorkommt
                tempPflanzen.RemoveAt(index);
                //Beendet den for-loop, wenn keine Pflanzen mehr verfügbar sind
                if (tempPflanzen.IsNullOrEmpty())
                {
                    break;
                }
            }

            quiz = tempQuiz.ToArray();
            einzelStatistiken = new StatistikPflanze[quiz.Length];
        }

        public static void PflanzenbilderBekommen()
        {
            momentanePflanzeAusQuiz++;
            pflanzenbilder = api_anbindung.BekommePflanzenbilder(quiz[momentanePflanzeAusQuiz].pflanze.id_pflanze);
        }

        public static void AktualisiereAusbilderId()
        {
            //ausbilderId = datenbankverbindung.BekommeAusbilderId(benutzer.nutzername);
        }

        public static Dictionary<int, T> ToDictionary<T>(this T[] arr)
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();

            for (int i = 0; i < arr.Length; i++)
            {
                dict.Add(i, arr[i]);
            }

            return dict;
        }

        private static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }

        /// <summary>
        /// Diese Funktion nicht benutzen
        /// </summary>
        /// <param name="StringIn"></param>
        /// <returns></returns>
        private static string SHA256HexHashString(string StringIn)
        {
            string hashString;
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(StringIn));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }

        /// <summary>
        /// Diese Funktion benutzen, um den Hash zu bekommen
        /// </summary>
        /// <param name="pass">Passwort</param>
        /// <param name="ben">Benutzername</param>
        /// <returns>Gehashtes Passwort</returns>
        public static string GetHashWithSalt(string pass, string ben)
        {
            //Add salt to string
            string StringIn = pass + ben.Substring(ben.Length - Math.Min(3, ben.Length), Math.Min(3, ben.Length));
            //Get Hash as Hex code
            return SHA256HexHashString(StringIn);
        }

        public static void LadenStart()
        {
            MainWindow.StartLoading();
        }

        public static void Laden(int amount = 10000)
        {
            //MainWindow.StartLoading();
            Stall.Bubblesort(amount);
            MainWindow.StopLoading();
        }

        /// <summary>
        /// Gibt zurück, ob eine gegebene Antwort "richtig genug" ist
        /// </summary>
        /// <param name="eingabe">Eingabe</param>
        /// <param name="korrekt">Korrekter String</param>
        /// <returns></returns>
        public static bool IstRichtig(string eingabe, string korrekt)
        {
            if (eingabe.ToArray().IsNullOrEmpty())
                return false;

            //Bekommt alle möglichen Antworten (mit , getrennt)
            string[] tempArr = korrekt.Split(',');
            string[] tempArr2;
            
            //Wenn es mehrere korrekte Antworten gibt: bekommt alle Eingaben (durch komma getrennt)
            if (tempArr.Length > 1)
            {
                tempArr2 = eingabe.Split(',');
            }
            else
            {
                tempArr2 = new string[] { eingabe };
            }

            //Wenn mindestens einer der eingegebenen Werte = mindestens ein korrekter Wert ist, "true" ausgeben
            for (int i = 0; i < tempArr.Length; i++)
            {
                for (int j = 0; j < tempArr2.Length; j++)
                {
                    if (tempArr[i].Trim().ToLower().Equals(tempArr2[j].Trim().ToLower()))
                        return true;
                }
            }

            //Ansonsten "false" ausgeben
            return false;
        }
    } 
}
