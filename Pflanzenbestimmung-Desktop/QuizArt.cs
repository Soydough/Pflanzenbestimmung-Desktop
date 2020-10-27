using Newtonsoft.Json;

namespace Pflanzenbestimmung_Desktop
{
    public class QuizArt
    {
        public int id;
        public string quizname;
        [JsonProperty("quizgroe\u00dfe")]
        public int quizgröße;
        public QuizArtPflanze[] pflanzen;

        public override string ToString()
        {
            return quizname.ToString();
        }
    }

    public class QuizArtPflanze
    {
        public int id_pflanze { get; set; }
    }
}
