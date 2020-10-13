namespace Pflanzenbestimmung_Desktop
{
    public class BenutzerTemplate
    {
        public int id;
        public int id_ausbilder;
        public int quiz_art;
        public int bool_pruefung;

        public string nutzername;
        public string vorname;
        public string name;
        // 0: Vollzeit, 1 Werker
        public int ausbildung;
        // 0: Gala, 1: Zier
        public int fachrichtung;
        public int berflag = -1;

        public bool istAdmin = false;

        public bool IstSysAdmin
        {
            get
            {
                return berflag == 1;
            }
            set { }
        }
    }
}
