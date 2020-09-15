/*
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Pflanzenbestimmung_Desktop
{
    public class Datenbankverbindung
    {
        public MySqlConnection connection;
        string connectionString = "Server='localhost';Database='pflanzenbestimmung';Uid='root';Pwd='citrix170890';";
        MySqlDataAdapter adapter;

        public Datenbankverbindung()
        {
            connection = new MySqlConnection(connectionString);
        }


        public Benutzer BenutzerBekommen(string benutzername, string passwort)
        {
            MySqlParameter benutzernameParameter = new MySqlParameter("dbbenutzername", benutzername);
            MySqlParameter passwortParameter = new MySqlParameter("dbpasswort", passwort);

            MySqlCommand query = new MySqlCommand("call check_login(@dbbenutzername, @dbpasswort)", connection);

            query.Parameters.Add(benutzernameParameter);
            query.Parameters.Add(passwortParameter);

            DataSet ds = new DataSet();
            adapter = new MySqlDataAdapter(query);

            Benutzer b;
            try
            {
                adapter.Fill(ds);
                object[] ergebnisse = new object[ds.Tables[0].Rows[0].ItemArray.Length];

                ergebnisse = ds.Tables[0].Rows[0].ItemArray;

                //Wenn Administrator
                if (ergebnisse.Length == 4)
                {
                    //int id = Convert.ToInt32(ergebnisse[0]);
                    string nutzername = Convert.ToString(ergebnisse[1]);
                    //string passwort = Convert.ToString(ergebnisse[2]);
                    int beruf = Convert.ToInt32(ergebnisse[3]);
                    b = new Benutzer(nutzername, beruf);
                }
                else
                {
                    //int id = Convert.ToInt32(ergebnisse[0]);
                    int ausbilderId = Convert.ToInt32(ergebnisse[1]);
                    int ausbildungsArtId = Convert.ToInt32(ergebnisse[2]);
                    int fachrichrichtungId = Convert.ToInt32(ergebnisse[3]);
                    string nutzername = Convert.ToString(ergebnisse[4]);
                    //string passwort = Convert.ToString(ergebnisse[5]);

                    b = new Benutzer(ausbilderId, ausbildungsArtId, fachrichrichtungId, nutzername);

                    //Wenn Nachname angegeben wurde
                    if (ergebnisse.Length > 6)
                    {
                        b.name = Convert.ToString(ergebnisse[6]);
                    }
                }
            }
            catch
            {
                b = Benutzer.ungueltigerBenutzer;
            }

            return b;
        }
    }
}
*/
