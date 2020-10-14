using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class Pflanze
    {
        public int id_pflanze;
        public int zierpflanzenbau;
        public int gartenlandschaftsbau;
        //[JsonProperty("0")]
        public KategorieAbfrage[] kategorien;

        public string Name
        {
            get
            {
                return ToString();
            }
            set
            { }
        }


        public bool IstGala
        {
            get
            {
                return gartenlandschaftsbau != 0;
            }
        }

        public bool IstZier
        {
            get
            {
                return zierpflanzenbau != 0;
            }
        }

        public override string ToString()
        {
            return $"{kategorien[0].antwort} {kategorien[1].antwort} {kategorien[2].antwort}";
        }
    }
}
