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

namespace HarwareMonitorApp
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
            ConnectButton.Enabled = false;
            DisconnectButton.Enabled = false;
        }
        void ScanPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                PortsComboBox.Items.Add(port);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ScanPorts();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PortsComboBox.Items.Clear();
            ConnectButton.Enabled=false;
            DisconnectButton.Enabled=false;
            ScanPorts();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Enabled = false;
            DisconnectButton.Enabled=true;
            InitializeSerialPort();
            
        }

        private void PortsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = true;
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            ConnectStatus.Text = "";
            RefreshButton.Enabled = true;
            ConnectButton.Enabled = false;
        }
        private void InitializeSerialPort()
        {
            string portName = PortsComboBox.Text;
            int baudRate = 9600;
            Parity parity = Parity.None; 
            int dataBits = 8;
            StopBits stopBits = StopBits.One; 
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            try
            {
                serialPort.Open();
                ConnectButton.Enabled = false;
                RefreshButton.Enabled = false;
                ConnectStatus.Text = "✔";
            }
            catch (Exception ex)
            {
                serialPort.Close();
                MessageBox.Show("Port Connection Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
