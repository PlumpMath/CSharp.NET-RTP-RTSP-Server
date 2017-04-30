using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment1_kpaliani
{

    public partial class Form1 : Form
    {

        private Controller _controller;

        public TextBox serverStatus;
        public TextBox clientRequests;

        public bool printHeader = false;

        public Form1()
        {
            InitializeComponent();

            serverStatus = serverStatusText;
            clientRequests = clientRequestsText;

            _controller = new Controller(this);
            this.listenOnPortButton.Click += new System.EventHandler(_controller.listenOnPortButton_Click);
        }

        public int getPort() {
            return Int32.Parse(listenOnPortText.Text);
        }

        //Update server status text area
        public void updateServerStatus(string status)
        {
            serverStatus.Invoke(new MethodInvoker(delegate { serverStatus.Text += status; }));
        
        }

        //Update client requests text area
        public void updateClientRequests(string status)
        {
            clientRequests.Invoke(new MethodInvoker(delegate { clientRequests.Text += (status + "\r\n"); }));


        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            Environment.Exit(0);
        }

        private void listenOnPortButton_Click(object sender, EventArgs e) {
            listenOnPortButton.Enabled = false;
        }

        private void printRTPHeaderCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (printRTPHeaderCheckBox.Enabled) {
                printHeader = true;
            } else {
                printHeader = false;
            }
        }
    }
}
