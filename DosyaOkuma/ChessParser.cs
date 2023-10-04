using SatrancDosyasiGorsellestirme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosyaOkuma
{
    internal class ChessParser
    {
        private byte[] data;
        public Stone[,] Stones()
        {
            Stone[,] stones = null;
            int ptr = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Position position = new Position();
                    position.row = data[ptr++];
                    position.col = data[ptr++];
                    position.StoneColor = (Stone.EnColor)data[ptr++];
                    Stone stn = new Stone();
                    stn.Pos = position;
                    stn.StoneType = (Stone.EnStone)data[ptr++];
                    stones[i, k] = stn;
                }
            }

            //0x01 0x03 0x01 0x5
            return stones;
        }
        public ChessParser(byte[] bytes) {
            data= bytes;
        }
    }
}
