using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Port_Land
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string port = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] portlar = SerialPort.GetPortNames();

            foreach (string portAdi in portlar)
            {
                port += portAdi + " ";
            }
            for (int i = 1; i < 30; i++)
            {
                comboBox2.Items.Add("COM" + i);
            }
            int band = 300;
            for (int i = 1; i < 9; i++)
            {
                comboBox1.Items.Add(band);
                band *= 2;
            }
            for (int i = 5; i < 9; i++)
            {
                comboBox3.Items.Add(i);
            }
            comboBox1.SelectedIndex = 5;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 3;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            baglanti_kes.Enabled = false;

        }

        
      

       


        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string veri = serialPort1.ReadExisting();
            richTextBox1.Text += veri;
        }

     

        private void Baglan_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox2.Text;
                serialPort1.BaudRate = int.Parse(comboBox1.Text);
                serialPort1.DataBits = int.Parse(comboBox3.Text);
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                serialPort1.Open();
            }
            catch (Exception z)
            {
                MessageBox.Show("Port aktif değil \n\n" + z.ToString());
            }
            if (serialPort1.IsOpen)
            {

                Baglan.Enabled = false;
                baglanti_kes.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
            }
        }

        private void baglanti_kes_Click(object sender, EventArgs e)
        {
            serialPort1.Close();

            Baglan.Enabled = true;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            baglanti_kes.Enabled = false;
        }

        private void aktif_portlar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aktif portlar: \n" + port, "Aktif portlar");
        }

        private void temizle_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void kaydet_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "TXT Dosyaları|*.txt";
            saveFileDialog1.Title = "Geçmişi kaydet";
            saveFileDialog1.ShowDialog();
            StreamWriter kaydet = new StreamWriter(saveFileDialog1.FileName);
            kaydet.WriteLine(richTextBox1.Text);
            kaydet.Close();
            MessageBox.Show("Geçmiş Kayıt edildi");
        }

        private void gonder_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(textBox1.Text + "\n");
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Port'a erişilemedi", "uyarı");
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                baglanti_kes.Enabled = false;
            }
        }

        private void temizle2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

        }
    }
}
