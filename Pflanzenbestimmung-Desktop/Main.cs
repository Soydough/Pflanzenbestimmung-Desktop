using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public static class Main
    {
        // Klasse zu speichern von Variablen, die auf mehreren Seiten gebraucht werden

        #region Variablen
        //Veraltet
        public static Datenbankverbindung datenbankverbindung = new Datenbankverbindung();

        public static API_Anbindung api_anbindung = new API_Anbindung();

        public static Benutzer benutzer;

        public static Dictionary<int, Ausbildungsart> ausbildungsarten;

        public static Dictionary<int, Fachrichtung> fachrichtungen;

        public static Dictionary<int, Administrator> ausbilder;

        public static int ausbilderId;

        public static Pflanze[] pflanzen;

        public static Pflanzenbild[] pflanzenbilder;

        public static QuizArt quizArt;

        public static QuizPflanze[] quiz;

        public static Kategorie[] kategorien;

        public static AzubiStatistik[] statistiken;

        public static AzubiStatistik statistik;

        public static Random random = new Random();

        public static Pflanzenbild[] momentanePflanzenbilder;

        public static int momentanePflanzeAusQuiz = -1; // -1: kein Quiz

        public static ImageSource fullscreenImage;

        public static QuizPZuweisung[] quizPZuweisungen;

        #endregion

        public static void Initialize()
        {
            //datenbankverbindung.BekommeAllePflanzenTest();
            //

            //Platzhalter-Bilder hochladen
            //byte[] platzhalter = File.ReadAllBytes(@"..\..\platzhalter.jpg");
            //api_anbindung.BildHochladen(1, platzhalter);
            //api_anbindung.BildHochladen(2, platzhalter);
            //api_anbindung.BildHochladen(3, platzhalter);

            ausbildungsarten = api_anbindung.Bekommen<Ausbildungsart>("Ausbildungsart").ToDictionary();
            fachrichtungen = api_anbindung.Bekommen<Fachrichtung>("Fachrichtung").ToDictionary();
            
            ausbilder = api_anbindung.Bekommen<Administrator>("Admins").ToDictionary();
            pflanzen = api_anbindung.Bekommen<Pflanze>();
            kategorien = api_anbindung.Bekommen<Kategorie>();
        }

        public static void LadeStatistiken()
        {
            if(!benutzer.istAdmin)
            {
                statistiken = api_anbindung.BekommeStatistiken(benutzer.id);
            }
        }

        public static void QuizBekommen()
        {
            if (benutzer.istAdmin)
            {
                MessageBox.Show("Ihnen ist kein Quiz zugewiesen!");
                return;
            }

            quizArt = api_anbindung.Bekommen<QuizArt>("QuizArt")[0];
            int anzahl = quizArt.quizgröße;
            quiz = new QuizPflanze[anzahl];

            quizPZuweisungen = api_anbindung.BekommeQuizPZuweisung(benutzer.id);

            if (quizPZuweisungen.IsNullOrEmpty())
            {
                MessageBox.Show("Ihnen ist kein Quiz zugewiesen!");
                return;
            }

            List<Pflanze> tempPflanzen = ((Pflanze[])pflanzen.Clone()).ToList();

            for (int i = 0; i < quiz.Length; i++)
            {
                quiz[i] = new QuizPflanze();
                int index = random.Next(quizPZuweisungen.Length - 1);
                quiz[i].pflanze = pflanzen[quizPZuweisungen[index].id_pflanze];

                //Enfernt die hinzugefügte Pflanze, damit jede Pflanzen nur einmal vorkommt
                tempPflanzen.Remove(quiz[i].pflanze);
                //Beendet den for-loop, wenn keine Pflanzen mehr verfügbar sind
                if (tempPflanzen.IsNullOrEmpty())
                {
                    break;
                }
            }
        }

        public static void PflanzenbilderBekommen()
        {
            momentanePflanzeAusQuiz++;
            pflanzenbilder = api_anbindung.BekommePflanzenbilder(quiz[momentanePflanzeAusQuiz].pflanze.id_pflanze);
        }

        public static void AktualisiereAusbilderId()
        {
            ausbilderId = datenbankverbindung.BekommeAusbilderId(benutzer.nutzername);
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
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
