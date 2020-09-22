using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Pflanzenbestimmung_Desktop
{
    public class Datenbankverbindung
    {
        /// <summary>
        /// Diese Datei wird später wahrscheinlich gelöscht. Hier gibt es also nichts wichtiges zu sehen
        /// </summary>


        public MySqlConnection connection;
        string connectionString = "Server='localhost';Database='pflanzenbestimmung';Uid='root';Pwd='citrix170890';";
        MySqlDataAdapter adapter;

        #region MySQL-Befehl-Strings
        private readonly string administratorenIdBekommenString = "SELECT id from admins where nutzername = @dbnutzername";
        private readonly string benutzerHinzufügenString = "INSERT into azubis (nutzername, passwort, fk_ausbilder, fk_ausbildungsart, fk_fachrichtung) values (@dbnutzername, @dbpasswort, @dbausbilder, @dbausbildungsart, @dbfachrichtung)";

        private readonly string ausbildungsArtenBekommenString = "SELECT * from ausbildungsart";
        private readonly string fachrichtungenBekommenString = "SELECT * from  fachrichtung";
        private readonly string ausbilderBekommenString = "SELECT * from admins where id != 1";

        private readonly string pflanzeHinzufügenString = "INSERT into pflanze (gattungsname, artname, dename, famname, herkunft, bluete, bluetezeit, blatt, wuchs, besonderheiten) values " +
            "(@dbgattung, @dbart, @dbdename, @dbfamname, @dbherkunft, @dbbluete, @dbbluetezeit, @dbblatt, @dbwuchs, @dbbesonderheiten)";
        #endregion

        public Datenbankverbindung()
        {
            connection = new MySqlConnection(connectionString);
        }

        public bool FuegePflanzeHinzu(string gattung, string art, string dename,
            string famname, string herkunft, string bluete, string bluetezeit,
            string blatt, string wuchs, string besonderheiten)
        {
            MySqlCommand query = new MySqlCommand(pflanzeHinzufügenString, connection);

            query.Parameters.AddWithValue("dbgattung", gattung);
            query.Parameters.AddWithValue("dbart", art);
            query.Parameters.AddWithValue("dbdename", dename);
            query.Parameters.AddWithValue("dbfamname", famname);
            query.Parameters.AddWithValue("herkunft", herkunft);
            query.Parameters.AddWithValue("bluete", bluete);
            query.Parameters.AddWithValue("bluetezeit", bluetezeit);
            query.Parameters.AddWithValue("blatt", blatt);
            query.Parameters.AddWithValue("wuchs", wuchs);
            query.Parameters.AddWithValue("besonderheiten", besonderheiten);

            try
            {
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
            }

            return false;
        }

        public bool FuegeBenutzerHinzu(string benutzername, string passwort, int ausbilderId, int ausbildungsart, int fachrichtung)
        {
            if (ausbilderId == -1)
                MessageBox.Show("Die Ausbilder-ID des angemeldeten Benutzers kann nicht festegestellt werden. Mögliche Ursachen:\n" +
                    "   • Es besteht keine Internet-Verbindung\n" +
                    "   • Sie haben versucht, eine Sicherheitslücke zu finden ;)\n" +
                    "\n" +
                    "Bitte versuchen Sie es in einigen Minuten noch einmal");
            else
            {
                //try
                //{

                MySqlParameter benutzernameParameter = new MySqlParameter("dbnutzername", benutzername);
                MySqlParameter passwortParameter = new MySqlParameter("dbpasswort", passwort);
                //MySqlParameter nachnameParameter = new MySqlParameter("dbnachname", nachname);
                MySqlParameter ausbilderParameter = new MySqlParameter("dbausbilder", ausbilderId);
                MySqlParameter ausbildungsartParameter = new MySqlParameter("dbausbildungsart", ausbildungsart);
                MySqlParameter fachrichtungParameter = new MySqlParameter("dbfachrichtung", fachrichtung);

                MySqlCommand query = new MySqlCommand(benutzerHinzufügenString, connection);

                query.Parameters.Add(benutzernameParameter);
                query.Parameters.Add(passwortParameter);
                query.Parameters.Add(ausbilderParameter);
                query.Parameters.Add(ausbildungsartParameter);
                query.Parameters.Add(fachrichtungParameter);

                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
                return true;
                //}
                //catch
                //{
                //    connection.Close();
                //    MessageBox.Show("Ein Fehler ist aufgetreten! Der Benutzer konnte nicht hinzugefügt werden. Mögliche Ursachen:\n" +
                //        "   • Es besteht keine Internet-Verbindung\n" +
                //        "   • Das Programm hat einen Fehler\n" +
                //        "\n" +
                //        "Bitte versuchen Sie es in einigen Minuten noch einmal.");
                //}
            }
            return false;
        }

        public int BekommeAusbilderId(string benutzername)
        {
            MySqlCommand query = new MySqlCommand(administratorenIdBekommenString, connection);
            query.Parameters.AddWithValue("dbbenutzername", benutzerHinzufügenString);

            DataSet ds = new DataSet();
            adapter = new MySqlDataAdapter(query);

            try
            {
                adapter.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
            }
            catch
            {
                return -1;
            }
        }

        public Dictionary<int, string> BekommeFachrichtungen()
        {
            return BekommeDictionary(fachrichtungenBekommenString);
        }

        public Dictionary<int, string> BekommeAusbildungsArten()
        {
            return BekommeDictionary(ausbildungsArtenBekommenString);
        }

        public Dictionary<int, string> BekommeAusbilder()
        {
            return BekommeDictionary(ausbilderBekommenString);
        }

        public Dictionary<int, string> BekommeDictionary(string suchString)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            MySqlCommand query = new MySqlCommand(suchString, connection);

            DataSet ds = new DataSet();
            adapter = new MySqlDataAdapter(query);

            try
            {
                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow item = ds.Tables[0].Rows[i];

                    int id = Convert.ToInt32(item[0]);
                    string name = Convert.ToString(item[1]);

                    dict.Add(id, name);
                }
            }
            catch { }

            return dict;
        }
    }
}