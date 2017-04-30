using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace assignment1_kpaliani
{

    public delegate void createClientDelegate(Socket clientSocket);
    public delegate void clientSetupDelegate(Socket clientSocket, int port, int session, string videoWanted, string rtspPacket);
    public delegate void clientPlayDelegate(Socket clientSocket, int port, int session, string rtspPacket, string videoWanted);
    public delegate void clientPauseDelegate(Socket clientSocket, int port, int session, string rtspPacket, string videoWanted);
    public delegate void clientTearDownDelegate(Socket clientSocket, int session, string rtspPacket);

    public delegate void printRTPHeader(byte[] header);

    class Controller {

        private Form1 _view;
        private RTSPModel _rtspModel;

        private bool _listening = false;

        private int _session = 0;

        Dictionary<int, Thread> connectedClientsRTP;

        public Controller(Form1 view) {
            _view = view;
            connectedClientsRTP = new Dictionary<int, Thread>();
        }

        //Print RTPHeader
        public void printHeader(byte[] header) {

            if (_view.printHeader) {
                string byteString = "";
                foreach (byte b in header) {
                    byteString += Convert.ToString(b, 2).PadLeft(8, '0');
                    byteString += " ";
                }
                _view.Invoke(new MethodInvoker(delegate { _view.updateServerStatus(byteString + Environment.NewLine); }));
            }
        }

        //Client RTSP connection thread for a client is created
        public void createClient(Socket clientSocket) {

            //Delgate methods for client model to call
            clientSetupDelegate clientSetupDel = new clientSetupDelegate(clientSetup);
            clientPlayDelegate clientPlayDel = new clientPlayDelegate(clientPlay);
            clientPauseDelegate clientPauseDel = new clientPauseDelegate(clientPause);
            clientTearDownDelegate clientTearDownDel = new clientTearDownDelegate(clientTearDown);

            //Creating the new client model thread for a connected client
            ClientModel client = new ClientModel(_session, clientSocket, clientSetupDel, clientPlayDel, clientPauseDel, clientTearDownDel);
            _session++;
            Thread clientThread = new Thread(new ThreadStart(() => client.listen()));
            clientThread.Start();
        }

        //Client setup
        public void clientSetup(Socket clientSocket, int port, int session, string videoWanted, string rtspPacket) {

            //Delegate methods for RTP model to call
            printRTPHeader printRTPHeader = new printRTPHeader(printHeader);

            //Create thread to start streaming video to client
            RTPModel rtpConnection = new RTPModel(((IPEndPoint)clientSocket.RemoteEndPoint).Address, port, printRTPHeader);
            Thread rtpThread = new Thread(new ThreadStart(() => rtpConnection.sendVideo(videoWanted)));
            connectedClientsRTP.Add(session, rtpThread);

            _view.updateClientRequests(rtspPacket);
            _view.updateServerStatus("The client " + ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString() + " has been joined." + Environment.NewLine);
        }

        //Client plays stream
        public void clientPlay(Socket clientSocket, int port, int session, string rtspPacket, string videoWanted) {
            if (!connectedClientsRTP[session].IsAlive) {
                connectedClientsRTP[session].Start();
            } else {
                connectedClientsRTP[session].Resume();
            }

            _view.updateClientRequests(rtspPacket);
            _view.updateServerStatus("The client " + ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString() + " is playing " + videoWanted + Environment.NewLine);
        }

        //Client pauses stream
        public void clientPause(Socket clientSocket,  int port, int session, string rtspPacket, string videoWanted) {
            connectedClientsRTP[session].Suspend();

            _view.updateClientRequests(rtspPacket);
            _view.updateServerStatus("The client " + ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString() + " paused " + videoWanted + Environment.NewLine);
        }

        //Client tears down connection
        public void clientTearDown(Socket clientSocket, int session, string rtspPacket) {
            try {
                connectedClientsRTP[session].Abort();
            } catch (Exception) { }
            connectedClientsRTP.Remove(session);

            _view.updateClientRequests(rtspPacket);
            _view.updateServerStatus("The client " + ((IPEndPoint)clientSocket.RemoteEndPoint).ToString() + " connection has been torn down." + Environment.NewLine);
        }

        //Thread that creates the socket that will listen to incoming connections from clients
        public void listenerThread(int port) {

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, port);
            Socket tcpServer = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
                );
            tcpServer.Bind(listenEndPoint);
            tcpServer.Listen(int.MaxValue);

           // _view.updateServerStatus("Server is waiting for a new connection." + Environment.NewLine);
            _view.serverStatus.Invoke(new MethodInvoker(delegate { _view.serverStatus.Text += "Server is waiting for a new connection." + Environment.NewLine; }));

            createClientDelegate createClientDel = new createClientDelegate(createClient);
            _rtspModel = new RTSPModel(createClientDel);
            _rtspModel.listen(tcpServer);         
            
        }

        //User clicks listen
        public void listenOnPortButton_Click(object sender, EventArgs e) {
            if (_listening == false) {
                int port = _view.getPort();
                if (port > 1024) {
                    Thread listenThread = new Thread(new ThreadStart(() => listenerThread(port)));
                    listenThread.Start();
                } else {
                    MessageBox.Show("Port number must be greater than 1024");
                }
            } else {
                _listening = true;
            }
        }

    }
}
