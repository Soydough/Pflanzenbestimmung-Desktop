using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    public class Azubis
    {
        int id;
        string name;
        string vorname;
        string nutzername;
        string ausbildungsart;
        string ausbilder;
        string fachrichtung;
        int id_quiz_art;
        int pruefung;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Vorname
        {
            get { return vorname; }
            set { vorname = value; }
        }
        public string Nutzername
        {
            get { return nutzername; }
            set { nutzername = value; }
        }
        public string Ausbildungsart
        {
            get { return ausbildungsart; }
            set { ausbildungsart = value; }
        }
        public string Ausbilder
        {
            get { return ausbilder; }
            set { ausbilder = value; }
        }
        public string Fachrichtung
        {
            get { return fachrichtung; }
            set { fachrichtung = value; }
        }
        public int ID_quiz_art
        {
            get { return id_quiz_art; }
            set { id_quiz_art = value; }
        }
        public int Pruefung
        {
            get { return pruefung; }
            set { pruefung = value; }
        }


    }

}
