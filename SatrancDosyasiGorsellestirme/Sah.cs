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
    public partial class Sah : Stone 
    {
        public Sah()
        {
            InitializeComponent();
        }
        public override void SetStone(char color)
        {
            switch (color)
            {
                case 's':
                    this.BackgroundImage = Properties.Resources.siyahSah;
                    stoneColor = EnColor.siyah;
                    break;
                case 'b':
                    this.BackgroundImage = Properties.Resources.beyazSah;
                    stoneColor = EnColor.beyaz;
                    break;
            }
        }
        public override List<Position> GetStoneMove(int row, int col, Stone[,] stones)
        {
            List<Position> lst = new List<Position>();
            GetPosition(-1, -1, stones, ref lst);
            GetPosition(-1, 0, stones, ref lst);
            GetPosition(-1, 1, stones, ref lst);
            GetPosition(0, 1, stones, ref lst);
            GetPosition(1, 1, stones, ref lst);
            GetPosition(1, 0, stones, ref lst);
            GetPosition(0, -1, stones, ref lst);
            GetPosition(1, -1, stones, ref lst);
            return lst;
        }

        private void GetPosition(int rowstep, int colstep, Stone[,] stones, ref List<Position> lst )
        {
            Position position = null;
            var rowp = Pos.row + rowstep;
            var colp = Pos.col + colstep;

            var CrossColor = this.StoneColor == EnColor.beyaz ? EnColor.siyah : EnColor.beyaz;
             if (colp < 0)
            {
                return;
            }
            else if (colp > 7)
            {
                return;
            }

            if (rowp < 0)
            {
                return;
            }
            else if (rowp > 7)
            {
                return;
            }
            if (stones[rowp,colp] == null)
            {
                position = new Position();
                position.row = rowp;
                position.col = colp;
                position.StoneColor = stoneColor;
            }
            else if(stones[rowp,colp].StoneColor == CrossColor)
            {
                position = new Position();
                position.row = rowp;
                position.col = colp;
                position.StoneColor = stoneColor;
            }
            if(position != null)
            {
                lst.Add(position);
            }
        }  
    }
}
