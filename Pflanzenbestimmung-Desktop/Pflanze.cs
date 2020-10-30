namespace Pflanzenbestimmung_Desktop
{
    public static class PflanzeExt
    {
        public static Pflanze FindeMitID(this Pflanze[] self, int id)
        {
            foreach (Pflanze pflanze in self)
            {
                if (pflanze.id_pflanze == id)
                    return pflanze;
            }
            return null;
        }
    }

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
        public string IstGalaReturnString
        {
            get
            {
                if (gartenlandschaftsbau == 0)
                {
                    return "Nein";
                }
                else
                {
                    return "Ja";
                }

            }
        }


        public string IstZierReturnString
        {
            get
            {
                if (zierpflanzenbau == 0)
                {
                    return "Nein";
                }
                else
                {
                    return "Ja";
                }

            }
        }


        public override string ToString()
        {
            return $"{kategorien[1].antwort}, {kategorien[0].antwort}";
        }
    }
}
