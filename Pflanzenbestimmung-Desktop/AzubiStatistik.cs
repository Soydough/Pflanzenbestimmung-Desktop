using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pflanzenbestimmung_Desktop
{
    public class AzubiStatistik
    {
        public int id_statistik;
        //format: yyyy-mm-dd hh:mm:ss
        public string erstellt;
        public int fehlerquote;
        //format: hh:mm:ss
        public string zeit;
        public int id_beste_pflanze;
        public StatistikPflanze[] pflanzen;
    }
}
