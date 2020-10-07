using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class QuizArt
    {
        public int id;
        public string quizname;
        [JsonProperty("quizgroe\u00dfe")]
        public int quizgröße;

        public override string ToString()
        {
            return quizgröße.ToString();
        }
    }
}
