using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatrancDosyasiGorsellestirme
{
    public partial class Vezir : Stone 
    {
        public Vezir()
        {
            InitializeComponent();
        }
        public override void SetStone(char color)
        {
            switch (color)
            {
                case 's':
                    this.BackgroundImage = Properties.Resources.siyahVezir;
                    stoneColor = EnColor.siyah;
                    break;
                case 'b':
                    this.BackgroundImage = Properties.Resources.beyazVezir;
                    stoneColor = EnColor.beyaz;
                    break;
            }

        }
        public override List<Position> GetStoneMove(int row, int col, Stone[,] stones)
        {
            List<Position> lst = new List<Position>();

            GetPosition(-1, +1, stones, ref lst);
            GetPosition(-1, -1, stones, ref lst);
            GetPosition(+1, -1, stones, ref lst);
            GetPosition(+1, +1, stones, ref lst);
            GetPosition(1, 0, stones, ref lst);
            GetPosition(-1, 0, stones, ref lst);
            GetPosition(0, 1, stones, ref lst);
            GetPosition(0, -1, stones, ref lst);
            return lst;
        }
        private void GetPosition(int rowStep, int colStep, Stone[,] stones, ref List<Position> lst)
        {
            var CrossColor = this.StoneColor == EnColor.beyaz ? EnColor.siyah : EnColor.beyaz;
            bool isLine = false;

            var rowP = Pos.row + rowStep;
            var colP = Pos.col + colStep;

            while (GetLine(rowP, colP) == false)
            {
                Position position = null;

                if (stones[rowP, colP] == null)
                {
                    position = new Position();
                    position.row = rowP;
                    position.col = colP;
                    position.StoneColor = stoneColor;
                }
                else if (stones[rowP, colP].StoneColor == CrossColor)
                {
                    position = new Position();
                    position.row = rowP;
                    position.col = colP;
                    position.StoneColor = stoneColor;

                    if (position != null)
                    {
                        lst.Add(position);
                    }

                    return;
                }
                else
                {
                    return;
                }

                if (position != null)
                {
                    lst.Add(position);
                }

                rowP = rowP + rowStep;
                colP = colP + colStep;
            }
        }

        private bool GetLine(int rowStep, int colStep)
        {
            bool isLine = false;

            if (colStep < 0)
            {
                isLine = true;
            }
            else if (colStep > 7)
            {
                isLine = true;
            }

            if (rowStep < 0)
            {
                isLine = true;
            }
            else if (rowStep > 7)
            {
                isLine = true;
            }

            return isLine;
        }
    }

}
