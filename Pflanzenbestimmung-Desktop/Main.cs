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

namespace Pflanzenbestimmung_Desktop
{
    public static class Main
    {
        public static Datenbankverbindung datenbankverbindung = new Datenbankverbindung();

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

        public static void Laden(int amount = 30000)
        {
            //MainWindow.StartLoading();
            Stall.Bubblesort(amount);
            MainWindow.StopLoading();
        }
    }
}
