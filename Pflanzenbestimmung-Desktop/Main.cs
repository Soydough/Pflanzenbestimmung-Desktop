using Flurl.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Dirk.Warnsholdt.Helper.Stall;
using System.Windows;
using System.Windows.Media;

namespace Pflanzenbestimmung_Desktop
{
    public static class Main
    {
        // Klasse zu speichern von Variablen, die auf mehreren Seiten gebraucht werden

        //Veraltet
        //public static Datenbankverbindung datenbankverbindung = new Datenbankverbindung();

        public static API_Anbindung api_anbindung = new API_Anbindung();

        public static Benutzer benutzer;

        public static Dictionary<int, string> ausbildungsarten;

        public static Dictionary<int, string> fachrichtungen;

        public static Dictionary<int, Administrator> ausbilder;

        public static int ausbilderId;

        public static Pflanze[] pflanzen;

        public static Kategorie[] kategorien;

        public static Pflanzenbild[] pflanzenbilder;

        public static QuizArt quizArt;

        //public static Quiz quiz;
        public static Quizfrage[] quiz;

        public static Random random = new Random();

        public static Pflanzenbild[] momentanePflanzenbilder;

        public static int momentanePflanzeAusQuiz = -1; // -1: kein Quiz


        public static void Initialize()
        {
            //datenbankverbindung.BekommeAllePflanzenTest();
            //
            //byte[] platzhalter = File.ReadAllBytes(@"..\..\platzhalter.png");
            //api_anbindung.BildHochladen(1, platzhalter);
            //
            ausbildungsarten = datenbankverbindung.BekommeAusbildungsArten();
            fachrichtungen = datenbankverbindung.BekommeFachrichtungen();

            ausbilder = api_anbindung.Bekommen<Administrator>("Admins").ToDictionary();

            pflanzen = api_anbindung.Bekommen<Pflanze>();
            kategorien = api_anbindung.Bekommen<Kategorie>();
        }

        public static void QuizBekommen()
        {
            quizArt = api_anbindung.Bekommen<QuizArt>("QuizArt")[0];
            int anzahl = quizArt.quizgröße;

            //quiz = new Quiz();
            //quiz.pflanzen = new Pflanze[anzahl];
            //quiz.kategorien = kategorien;
            quiz = new Quizfrage[anzahl];

            List<Pflanze> tempPflanzen = ((Pflanze[])pflanzen.Clone()).ToList();
            for(int i = 0; i < quiz.Length; i++)
            {
                quiz[i] = new Quizfrage();
                int index = random.Next(tempPflanzen.Count - 1);
                quiz[i].pflanze = tempPflanzen[index];
            }

            kategorien = api_anbindung.Bekommen<Kategorie>();
        }

        public static void FragenBekommen()
        {
        }

        public static void PflanzenbilderBekommen()
        {
            pflanzenbilder = api_anbindung.BekommePflanzenbilder(quiz[++momentanePflanzeAusQuiz].pflanze.id);
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
            using (var sha256 = SHA256Managed.Create())
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
