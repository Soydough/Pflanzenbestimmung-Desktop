using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
