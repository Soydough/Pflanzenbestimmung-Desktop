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
            object[] ergebnisse = new object[ds.Tables[0].Rows.Count];

            Benutzer b;
            try
            {
                adapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ergebnisse[i] = ds.Tables[0].Rows[i].ItemArray[0] + ", " + ds.Tables[0].Rows[i].ItemArray[1];
                }

                //Wenn Nachname angegeben wurde
                if (ergebnisse.Length == 5)
                    b = new Benutzer((int)ergebnisse[0], (int)ergebnisse[1], (int)ergebnisse[2], (string)ergebnisse[3], (string)ergebnisse[4]);
                //Wenn kein echter Name angegeben wurde
                else
                    b = new Benutzer((int)ergebnisse[0], (int)ergebnisse[1], (int)ergebnisse[2], (string)ergebnisse[3]);
            }
            catch
            {
                b = Benutzer.ungueltigerBenutzer;
            }

            return b;
        }
    }
}
