using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SatrancDosyasiGorsellestirme.Stone;

namespace SatrancDosyasiGorsellestirme
{
    public class Position
    {
        public int row
        {
            get;
            set;
        }
        public int col
        {
            get;
            set;
        }

        public EnColor StoneColor
        {
            get;
            set;
        }    
    }
}
