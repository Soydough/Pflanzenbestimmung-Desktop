using System;

namespace Pflanzenbestimmung_Desktop
{
    public class Abgefragt
    {
#pragma warning disable CS0618 // Nur damit VS nicht nervt
        public int IDp;
        public int Counter;
        [Obsolete("Stattdessen bitte IstGelernt verwenden")]
        public int Bool_Gelernt;

        public bool IstGelernt
        {
            get
            {
                return Bool_Gelernt != 0;
            }
        }
    }
}
