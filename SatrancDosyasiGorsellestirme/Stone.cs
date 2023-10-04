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
    public partial class Stone : UserControl
    {
        public enum EnColor
        {
            siyah,
            beyaz
        }
        public enum EnStone
        {
            None,
            Piyon,
            Kale,
            At,
            Fil,
            Vezir,
            Şah
        }
        public Position Pos
        {
            get;
            set;
        }

        public EnStone StoneType
        {
            get; set;
        }


       
        protected EnColor stoneColor = EnColor.siyah;
        public EnStone stone = EnStone.None;             
        public Stone()
        {
            InitializeComponent();
            Pos = new Position();
        }      
        public EnColor StoneColor
        {
            get
            {
                return stoneColor;
            }
        }
        //public EnStone StoneProp
        //{
        //    get
        //    {
        //        return stone;
        //    }
        //}
        public virtual void SetStone(char color)
        {
        }
        public virtual List<Position> GetStoneMove(int row, int col, Stone[,] stones)
        {
            return null;
        }
        
        public virtual List<Position> Tehdit(int row,int col)
        {
            return null;
        }        
    }
}
