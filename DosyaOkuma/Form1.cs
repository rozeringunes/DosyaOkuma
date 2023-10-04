using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SatrancDosyasiGorsellestirme;
using System.Net.Sockets;
using System.Threading;

namespace DosyaOkuma
{
    public partial class Form1 : Form
    {
        public SatrancDosyasiGorsellestirme.ChessVisibility chessVisibility = new SatrancDosyasiGorsellestirme.ChessVisibility();
        public string Tas(char stone)
        {
            string tas;
            switch (stone)
            {
                case 'k':
                    tas = "Kale";
                    break;
                case 'a':
                    tas = "At";
                    break;
                case 'f':
                    tas = "Fil";
                    break;
                case 'v':
                    tas = "Vezir";
                    break;
                case 's':
                    tas = "Şah";
                    break;
                case 'p':
                    tas = "Piyon";
                    break;
                default:
                tas=  "Taş yok ";
                    break;
             }
            return tas; 
        }        
        public string TasRengi(char stoneColor)
        {
            string renk;
            switch (stoneColor)
            {
                case 's':
                    renk = "Siyah";
                    break;
                case 'b':
                    renk = "Beyaz";
                    break;
                default:
                    renk = "Renk yok ";
                    break;
            }
            return renk;
        }

        public string ip;
        public int port;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static void SendMatrix(TcpClient client, Stone[,] matrix)
        {
            byte[] bytes = null;
            NetworkStream stream = client.GetStream();
            int ptr =0;
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    bytes[ptr++] = (byte)matrix[i, k].Pos.row;
                    bytes[ptr++] = (byte)matrix[i, k].Pos.col;
                    bytes[ptr++] = (byte)matrix[i, k].StoneColor;
                    bytes[ptr++] = (byte)matrix[i, k].StoneType;
                }
            }
            stream.Write(bytes, 0, bytes.Length);
            //// Matris boyutlarını gönder
            //byte[] sizeData = BitConverter.GetBytes(matrix.GetLength(0));
            //stream.Write(sizeData, 0, sizeData.Length);
            //sizeData = BitConverter.GetBytes(matrix.GetLength(1));
            //stream.Write(sizeData, 0, sizeData.Length);

            //// Matrisi gönder
            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        byte[] data = BitConverter.GetBytes(matrix[i, j]);
            //        stream.Write(data, 0, data.Length);
            //        foreach (var item in data)
            //        {
            //            Console.WriteLine(item);
            //        }
            //    }
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Stone[,] stones = new Stone[8, 8];
            int x = 0, y = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[,] chessArray = new string[8,8];              
                FileStream fileStream = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    int row = 0;
                   // label2.Text = string.Empty;
                    while (true)
                    {
                        string satir = reader.ReadLine();
                        if (satir == null)
                            break;
                        string[] strArr = satir.Split(' ');                        
                        for (int column=0; column < strArr.Length; column++)
                        {
                            chessArray[row, column] = strArr[column];
                            var b = strArr[column];
                            if (strArr[column] != "--")
                            {
                                char stone = strArr[column][0];
                                char stoneColor = strArr[column][1];
                                // label2.Text += strArr[i]+" " +"="+" "+Tas(stone)+" "+TasRengi(stoneColor)+"\n";
                                switch (stone)
                                {
                                    case 'k':
                                        SatrancDosyasiGorsellestirme.Kale kale = new SatrancDosyasiGorsellestirme.Kale();
                                        stones[row, column] = kale;
                                        kale.SetStone(stoneColor);
                                        kale.Pos.row = row;
                                        kale.Pos.col = column;
                                        kale.Location = new Point(x, y);
                                        chessVisibility.Controls.Add(kale);
                                        break;
                                    case 'a':
                                        SatrancDosyasiGorsellestirme.At at = new SatrancDosyasiGorsellestirme.At();
                                        stones[row, column] = at;
                                        at.SetStone(stoneColor);
                                        at.Pos.row = row;
                                        at.Pos.col = column;
                                        at.Location = new Point(x, y);
                                        chessVisibility.Controls.Add(at);                                       
                                        break;
                                    case 'f':
                                        SatrancDosyasiGorsellestirme.Fil fil = new SatrancDosyasiGorsellestirme.Fil();
                                        stones[row, column] = fil;
                                        fil.SetStone(stoneColor);
                                        fil.Pos.row = row;
                                        fil.Pos.col = column;
                                        fil.Location = new Point(x, y);
                                        chessVisibility.Controls.Add(fil);
                                        break;
                                    case 'v':
                                        SatrancDosyasiGorsellestirme.Vezir vezir = new SatrancDosyasiGorsellestirme.Vezir();
                                        stones[row, column] = vezir;
                                        vezir.SetStone(stoneColor);
                                        vezir.Pos.row = row;
                                        vezir.Pos.col = column;
                                        vezir.Location = new Point(x, y);
                                        chessVisibility.Controls.Add(vezir);
                                        break;
                                    case 's':
                                        SatrancDosyasiGorsellestirme.Sah sah = new SatrancDosyasiGorsellestirme.Sah();
                                        stones[row, column] = sah;
                                        sah.SetStone(stoneColor);
                                        sah.Pos.row = row;
                                        sah.Pos.col = column;
                                        sah.Location = new Point(x, y);
                                        chessVisibility.Controls.Add(sah);
                                        break;
                                    case 'p':
                                        SatrancDosyasiGorsellestirme.Piyon piyon = new SatrancDosyasiGorsellestirme.Piyon(); 
                                        stones[row, column] = piyon;
                                        piyon.SetStone(stoneColor);
                                        piyon.Pos.row = row;
                                        piyon.Pos.col = column;                                    
                                        piyon.Location = new Point(x, y);                                       
                                        chessVisibility.Controls.Add(piyon);
                                        break;
                                }
                            }
                            x += 50; 
                        }
                        label1.Text += satir + "\n";
                        y += 50;
                        x = 0;
                        row++;
                    }                        
                    label2.Text = string.Empty;
                    label5.Text = string.Empty;
                    for (int i = 0; i <8; i++)
                    {          
                        for (int j= 0; j <8; j++)
                        {
                            //label2.Text += chessArray[i,j].ToString()+"  ";
                            if (chessArray[i, j] == "ps")
                            {                             
                               label2.Text += "Siyah Piyon Index Değeri:" + "[" + i + "," + j + "]" + "\n";
                               label5.Text += "  " + chessArray[i, j] + "\n" + ((j - 1) >= 0 ? chessArray[i + 1, j - 1] : ".") + "      " + ((j + 1) <= 7 ? chessArray[i + 1, j + 1] : ".") + "\n";
                               char color = (j - 1) >= 0 ? chessArray[i+1,j-1][1]: ' ';
                                if (color == 'b')
                                {
                                    label5.Text += ((j - 1) >= 0 ? chessArray[i + 1, j - 1] : " ") + "*";
                                }
                                char color2 =(j + 1) <= 7 ? chessArray[i +1, j + 1][1] : ' ';
                                if (color2 == 'b')
                                {
                                    label5.Text += ((j + 1) <= 7 ? chessArray[i + 1, j + 1] : " ") + "*";
                                }                                  
                            }
                        }                     
                        label2.Text += "\n";                        
                    }
                    reader.Close();
                    chessVisibility.Refresh();
                    chessVisibility.SetChess(stones);

                    if (stones != null)
                    {
                        TcpClient client = new TcpClient();
                        client.Connect(ip, port);
                        Console.WriteLine("Sunucuya bağlanıldı.");

                        //int[,] intStones = new int[8, 8];

                        //for (int i = 0; i < 8; i++)
                        //{
                        //    for (int j = 0; j < 8; j++)
                        //    {
                        //        Stone stone = stones[i, j];
                        //        if (stone != null)
                        //        {
                        //            intStones[i, j] = (int)stone.StoneType;
                        //        }
                                
                        //    }
                        //}
                        SendMatrix(client, stones);
                    }

                    chessVisibility.ShowDialog();
                }
                fileStream.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}              


                    
        
             
             
             
                    


             

   





