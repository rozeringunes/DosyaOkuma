using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SatrancDosyasiGorsellestirme
{
    public partial class ChessVisibility : Form
    {
        private List<List<Position>> list;       
        Stone[,] stones = new Stone[8, 8];
        Point currentPoint;
        Thread th;
        public ChessVisibility()
        {
            InitializeComponent();

            list = new List<List<Position>>();          
            Paint += Form1_Paint;
            Invalidate();
            DoubleBuffered = true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            // graphics.DrawLine(pen, 100, 150, 200, 250);
            SolidBrush brushBlack = new SolidBrush(Color.Black);
            SolidBrush brushWhite = new SolidBrush(Color.White);
            SolidBrush brushOrange = new SolidBrush(Color.Orange);
            SolidBrush brushRed = new SolidBrush(Color.Red);
            Point[] kare = new Point[64];
            int x = 0, y = 0;
            bool state = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (state == false)
                    {
                        graphics.FillRectangle(brushBlack, x, y, 50, 50);
                    }
                    else
                    {
                        graphics.FillRectangle(brushWhite, x, y, 50, 50);
                    }
                    x += 50;
                    state = state == false;
                }
                state = state == false;
                x = 0;
                y += 50;
            }
            for (int i = 0; i < list.Count; i++)
            {
                List<Position> _list = list[i];
                if (_list == null)
                {
                    continue;
                }
                for (int j = 0; j < _list.Count; j++)
                {
                    graphics.FillRectangle(brushOrange, _list[j].col * 50, _list[j].row * 50, 50, 50);
                }                
            }            
        }
        public void SetList(List<Position> lst)
        {
            list.Add(lst);
        }  
        
        public void SetChess(Stone[,] stones)
        {
            this.stones = stones;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (stones[i, j] != null)
                    {
                        stones[i, j].MouseDown += ChessVisibility_MouseDown;
                        stones[i, j].MouseUp += ChessVisibility_MouseUp;
                        stones[i, j].MouseMove += ChessVisibility_MouseMove;
                        stones[i, j].MouseLeave += ChessVisibility_MouseLeave;

                        var list = stones[i, j].Tehdit(i, j);
                        if (list == null)
                        {
                            continue;
                        }

                        for (int k = 0; k < list.Count; k++)
                        {
                            var stone = list[k];
                            var color = stones[i, j].StoneColor == Stone.EnColor.siyah ? 
                                Stone.EnColor.beyaz : Stone.EnColor.siyah;

                            if (stones[stone.row,stone.col] != null &&
                                stones[stone.row, stone.col].StoneColor == color)
                            {
                                stones[stone.row, stone.col].BackColor = Color.Red;
                            }
                        }



                    }
                }
            }
        }

        
        //pos.col* 50 > stone.Location.X &&
        private void ChessVisibility_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        Position posCurrent = null;
        private void ChessVisibility_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdrop)
            {
                Cursor = Cursors.Hand;
                Stone stone = (sender as Stone);
                Point dif = new Point(e.X - currentPoint.X , e.Y- currentPoint.Y);
                stone.Location = new Point(stone.Location.X + dif.X, stone.Location.Y + dif.Y);
                for (int i = 0; i < list.Count; i++)
                {
                    var a = list[i];
                    if (a == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < a.Count; j++)
                    {
                        var pos = a[j];
                        if (pos.col * 50 <= stone.Location.X && pos.col * 50 + 50 >= stone.Location.X &&
                            pos.row * 50 <= stone.Location.Y && pos.row * 50 + 50 >= stone.Location.Y)
                        {
                            posCurrent = pos;                           
                            return;
                        }
                        else
                        {
                            posCurrent = null;
                        }
                    }
                }
            }          
        }
       
        private void ChessVisibility_MouseUp(object sender, MouseEventArgs e)
        {
            Stone stone = (sender as Stone);
            isdrop = false;
            Cursor = Cursors.Arrow;
            stone.BackColor = Color.Transparent;

            if (posCurrent != null)
            {
                Position oldPosition = new Position()
                {
                    row = stone.Pos.row,
                    col = stone.Pos.col
                };

                stone.Location = new Point(posCurrent.col * 50, posCurrent.row * 50);
                stone.Pos.row = posCurrent.row;
                stone.Pos.col = posCurrent.col;
                stones[oldPosition.row, oldPosition.col] = null;

                var color = stone.StoneColor == Stone.EnColor.siyah ?
                                       Stone.EnColor.beyaz : Stone.EnColor.siyah;               

                if (stones[stone.Pos.row, stone.Pos.col] != null && stones[stone.Pos.row, stone.Pos.col].StoneColor == color)
                {
                    Controls.Remove(stones[stone.Pos.row, stone.Pos.col]);
                    if (stone.StoneColor == Stone.EnColor.beyaz)
                    {
                        flowLayoutPanel2.Controls.Add(stones[stone.Pos.row, stone.Pos.col]);
                    }
                    else if (stone.StoneColor == Stone.EnColor.siyah)
                    {
                        flowLayoutPanel1.Controls.Add(stones[stone.Pos.row, stone.Pos.col]);
                    }

                   stones[oldPosition.row, oldPosition.col] = null;
                }
                else
                {
                    stone.Location = new Point(stone.Pos.col * 50, stone.Pos.row * 50);
                }

                stones[posCurrent.row, posCurrent.col] = stone;
            }
            else
            {
                stone.Location = new Point(stone.Pos.col * 50, stone.Pos.row * 50);
            }           
            list.Clear();
            Invalidate();
        }
        bool isdrop = false;
        private void ChessVisibility_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            Stone stone = (sender as Stone);
            isdrop = true;
            (sender as Stone).BackColor = Color.LightSkyBlue;
            currentPoint = e.Location;
            var list = stone.GetStoneMove(stone.Pos.row, stone.Pos.col,stones);
            var tehditList = stone.Tehdit(stone.Pos.row, stone.Pos.col);

            var color = stone.StoneColor == Stone.EnColor.siyah ?
                               Stone.EnColor.beyaz : Stone.EnColor.siyah;

            if (tehditList != null)
            {
                for (int i = 0; i < tehditList.Count; i++)
                {
                    if (stones[tehditList[i].row, tehditList[i].col] != null &&
                        stones[tehditList[i].row, tehditList[i].col].StoneColor == color)
                    {
                        list.Add(tehditList[i]);                       
                    }                  
                }
            }
            SetList(list);
            Invalidate();
        }

    }
}
