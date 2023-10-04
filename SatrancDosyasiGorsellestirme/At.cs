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
    public partial class At : Stone
    {
        public At()
        {
            InitializeComponent();
            stone = EnStone.At;
        }
        public override void SetStone(char color)
        {
            switch (color)
            {
                case 's':
                    this.BackgroundImage = Properties.Resources.siyahAt;
                    stoneColor = EnColor.siyah;
                    break;
                case 'b':
                    this.BackgroundImage = Properties.Resources.beyazAt;
                    stoneColor = EnColor.beyaz;
                    break;
            }
        }
        public override List<Position> GetStoneMove(int row, int col, Stone[,] stones)
        {
            List<Position> lst = new List<Position>();

            GetPosition(-2, -1, stones, ref lst);
            GetPosition(-2, 1, stones, ref lst);
            GetPosition(-1, -2, stones, ref lst);
            GetPosition(-1, 2, stones, ref lst);
            GetPosition(1, -2, stones, ref lst);
            GetPosition(1, 2, stones, ref lst);
            GetPosition(2, -1, stones, ref lst);
            GetPosition(2, 1, stones, ref lst);

            return lst;
        }

        private void GetPosition(int rowStep, int colStep, Stone[,] stones, ref List<Position> lst)
        {
            Position position = null;

            var rowP = Pos.row + rowStep;
            var colP = Pos.col + colStep;

            var CrossColor = this.StoneColor == EnColor.beyaz ? EnColor.siyah : EnColor.beyaz;

            if (colP < 0)
            {
                return;
            }
            else if (colP > 7)
            {
                return;
            }

            if (rowP < 0)
            {
                return;
            }
            else if (rowP > 7)
            {
                return;
            }

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
            }

            if (position != null)
            {
                lst.Add(position);
            }
        }
    }
}
