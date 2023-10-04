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
    public partial class Piyon : Stone
    {

        public Piyon()
        {
            InitializeComponent();
            stone = EnStone.Piyon;
        }
        public override void SetStone(char color)
        {
            switch (color)
            {
                case 's':
                    this.BackgroundImage = Properties.Resources.siyahPiyon;
                    stoneColor = EnColor.siyah;
                    break;
                case 'b':
                    this.BackgroundImage = Properties.Resources.beyazPiyon;
                    stoneColor = EnColor.beyaz;
                    break;
            }
        }
        public override List<Position> GetStoneMove(int row, int col, Stone[,] stones)
        {
            List<Position> lst = new List<Position>();
            switch (stoneColor)
            {
                case EnColor.siyah:
                    if (row == 1)
                    {
                        if(stones[row + 1, col]==null && stones[row + 2, col] == null)
                        {
                            lst.Add(new Position() { row = row + 1, col = col, StoneColor = EnColor.siyah });
                            lst.Add(new Position() { row = row + 2, col = col, StoneColor = EnColor.siyah });
                        }else if(stones[row + 1, col] == null)
                        {
                            lst.Add(new Position() { row = row + 1, col = col, StoneColor = EnColor.siyah });
                        }
                       
                    }
                    else
                    {
                        if (stones[row + 1, col] == null)
                        {
                            lst.Add(new Position() { row = row + 1, col = col, StoneColor = EnColor.siyah });
                        }
                        
                    }
                    break;
                case EnColor.beyaz:
                    if (row == 6)
                    {
                        if (stones[row - 1, col] == null && stones[row - 2, col] == null)
                        {
                            lst.Add(new Position() { row = row - 1, col = col, StoneColor = EnColor.beyaz });
                            lst.Add(new Position() { row = row - 2, col = col, StoneColor = EnColor.beyaz });
                        }
                        else if (stones[row - 1, col] == null)
                        {
                            lst.Add(new Position() { row = row - 1, col = col, StoneColor = EnColor.beyaz });
                        }

                        
                    }
                    else
                    {
                        if (stones[row - 1, col] == null)
                        {
                            lst.Add(new Position() { row = row - 1, col = col, StoneColor = EnColor.beyaz });
                        }
                    }
                    break;
            }
            return lst;
        }
        public override List<Position> Tehdit(int row, int col)
        {
            List<Position> list = new List<Position>();

            switch (stoneColor)
            { 
                case EnColor.siyah:
                    if (col==0)
                    {                     
                        list.Add(new Position() { row = row + 1, col = col + 1, StoneColor = EnColor.siyah });
                    }
                    if (col == 7)
                    {
                        list.Add(new Position() { row = row + 1, col = col - 1, StoneColor = EnColor.siyah });
                    }
                    if(col>0 && col<7)
                    {
                        list.Add(new Position() { row = row + 1, col = col + 1, StoneColor = EnColor.siyah });
                        list.Add(new Position() { row = row + 1, col = col - 1, StoneColor = EnColor.siyah });
                    }
                    break;


                case EnColor.beyaz:
                    if (col==0)
                    {
                        list.Add(new Position() { row = row - 1, col = col + 1, StoneColor = EnColor.beyaz });
                    }
                    if (col == 7)
                    {
                       list.Add(new Position() { row = row - 1, col = col - 1, StoneColor = EnColor.beyaz });
                    }
                    if (col>0 && col<7 )
                    {
                        list.Add(new Position() { row = row - 1, col = col - 1, StoneColor = EnColor.beyaz });
                        list.Add(new Position() { row = row - 1, col = col + 1, StoneColor = EnColor.beyaz });
                    }
                    break;
            }
            return list;
        }
    }
}
