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

namespace Pflanzenbestimmung_Desktop
{
    public static class Main
    {
        // Klasse zu speichern von Variablen, die auf mehreren Seiten gebraucht werden

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

        public static QuizPflanze[] quiz;

        public static List<Kategorie> kategorien;

        public static AzubiStatistik[] statistiken;

        public static AzubiStatistik statistik;

        public static Random random = new Random();

        public static Pflanzenbild[] momentanePflanzenbilder;

        public static int momentanePflanzeAusQuiz = -1; // -1: kein Quiz

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

        public static int fehlersumme;

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

        public static ObservableCollection<Azubis> MyList
        {
            get; set;
        }

        public static ObservableCollection<Azubis> InitializeMylist()
        {
            MyList = new ObservableCollection<Azubis>();
            for (int i = 0; i < azubi.Length; i++)
            {
                MyList.Add(azubi[i]);
            }
            return MyList;
        }

        public static void LadeStatistiken()
        {
            if (!benutzer.istAdmin)
            {
                statistiken = api_anbindung.BekommeStatistikenListe(benutzer.id);
            }
        }

        public static void LadeStatistikenHoch()
        {
            fehlersumme = 0;

            for (int i = 0; i < einzelStatistiken.Length; i++)
            {
                for (int j = 0; j < kategorien.Count; j++)
                {
                    StatistikPflanzeAntwort temp = einzelStatistiken[i].antworten[j];

                    //if (temp.eingabe != temp.korrekt)
                    if (!IstRichtig(temp.eingabe, temp.korrekt))
                    {
                        fehlersumme++;
                    }
                }

                int fehlerquote = (int)(100.0 * kategorien.Count / fehlersumme);
                api_anbindung.ErstelleStatistik(benutzer.id, fehlerquote, quizTimer.Elapsed, quiz[i].pflanze.id_pflanze);

                LadeStatistiken();

                for (int j = 0; j < kategorien.Count; j++)
                {
                    //statistiken.Length ist die neuste, also hoffentlich die gerade hinzugefügte?
                    api_anbindung.ErstelleEinzelStatistik(statistiken.Length, j + 1, quiz[i].pflanze.id_pflanze, einzelStatistiken[i].antworten[j].eingabe);
                }

                fehlersumme = 0;
            }
        }

        public static void QuizBekommen()
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
                    tempPflanzen.Add(pflanzen[azubiQuizZuweisungen[i].id_pflanze - 1]);
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
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IstRichtig(string a, string b)
        {
            string[] tempArr = b.Split(',');
            string[] tempArr2;
            if (tempArr.Length > 1)
            {
                tempArr2 = a.Split(',');
            }
            else
            {
                tempArr2 = new string[] { a };
            }

            for (int i = 0; i < tempArr.Length; i++)
            {
                for (int j = 0; j < tempArr2.Length; j++)
                {
                    if (tempArr[i].Trim().ToLower().Equals(tempArr2[j].Trim().ToLower()))
                        return true;
                }
            }

            return false;
        }
    } 
}
