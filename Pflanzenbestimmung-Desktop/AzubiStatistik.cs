using System;

namespace Pflanzenbestimmung_Desktop
{
    public class AzubiStatistik
    {
        public int id_statistik;
        //format: yyyy-mm-dd hh:mm:ss
        //DateTime
        public DateTime erstellt;
        public string fehlerquote;
        //format: hh:mm:ss
        //TimeSpan
        public TimeSpan zeit;
        public int id_beste_pflanze;
        public StatistikPflanze[] pflanzen;


        //public DateTime Erstellt
        //{
        //    get
        //    {
        //        return DateTime.Parse(erstellt);
        //    }
        //    set
        //    {
        //        erstellt = value.ToString();
        //    }
        //}

        public override string ToString()
        {
            return $"Quiz von {erstellt:yyyy.MM.dd HH:mm:ss} (Benötigte Zeit: {zeit:hh\\:mm\\:ss}, Fehlerquote: {fehlerquote})";
        }
    }
}
