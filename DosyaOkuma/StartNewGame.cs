using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SatrancDosyasiGorsellestirme;

namespace DosyaOkuma
{
    public partial class StartNewGame : Form
    {
        Form1 form1 = new Form1();
        private IPAddress ip;
        private int port;
        public StartNewGame()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                return;
            }
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked == true)
            {
                ip = IPAddress.Parse("127.0.0.1");
                port = 3360;
                Thread th = new Thread(StartServer);
                th.Start();
                form1.ip = "127.0.0.1";
                form1.port = port;
                form1.Show();
                return;
            }

            ip = IPAddress.Parse(textBox1.Text);
            port = int.Parse(textBox2.Text);
            ConnectToServer();
            form1.ip = textBox1.Text;
            form1.port = int.Parse(textBox2.Text);
            form1.Show();
            this.Close();
        }

        public void ConnectToServer()
        {
            try
            {
                TcpClient client = new TcpClient();

                client.Connect(ip, port);

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string connectionInfo = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Baglanti Kimligi : " + connectionInfo);

                while (true)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    Stone[,] stones = new Stone[8, 8];
                    ChessParser chessParser = new ChessParser(buffer);
                    stones = chessParser.Stones();
                    form1.chessVisibility.SetChess(stones);
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("Hata: " + e.Message);
            }
        }

        private void StartServer()
        {
            TcpListener server = null;
            TcpClient client1 = null;
            TcpClient client2 = null;
            bool isBaglanti = false;
            try
            {
                Int32 port = 3360;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);
                //Sunucu Basladi
                server.Start();

                //Ilk baglantidan sonra kendimiz baglaniyoruz.
                if (!isBaglanti)
                {
                    Thread connectThread = new Thread(ConnectToServer);
                    connectThread.Start();
                    isBaglanti = true;
                }


                //1. Kullanici Baglandi
                client1 = server.AcceptTcpClient();

                // 2. Kullanici Baglandi
                client2 = server.AcceptTcpClient();

                NetworkStream stream1 = client1.GetStream();
                NetworkStream stream2 = client2.GetStream();

                byte[] buffer = Encoding.ASCII.GetBytes("Player 1");
                stream1.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes("Player 2");
                stream2.Write(buffer, 0, buffer.Length);

                while (true)
                {
                    
                    buffer = new byte[1024];
                    int bytesRead = stream1.Read(buffer, 0, buffer.Length);

                    Stone[,] stones = new Stone[8, 8];
                    ChessParser chessParser = new ChessParser(buffer);
                    stones = chessParser.Stones();
                    //ChessVisibilty
                    ChessVisibility chessVisibility = new ChessVisibility();
                    form1.chessVisibility.SetChess(stones);

                    // İkinci istemciye hamleyi gönderin
                    stream2.Write(buffer, 0, bytesRead);

                    // İkinci istemciden hamle alın
                    bytesRead = stream2.Read(buffer, 0, buffer.Length);
                    
                   chessParser = new ChessParser(buffer);
                    stones = chessParser.Stones();
                    form1.chessVisibility.SetChess(stones);

                    // Birinci istemciye hamleyi gönderin
                    stream1.Write(buffer, 0, bytesRead);
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("Hata: " + e.Message);
            }
            finally
            {
                client1?.Close();
                client2?.Close();
                server?.Stop();
            }
        }
    }
}
