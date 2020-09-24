using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class Pflanze
    {
        public int id;
        public int zier;
        public int gala;
        [JsonProperty("0")]
        public KategorieAbfrage[] kategorieAbfragen;
    }
}
