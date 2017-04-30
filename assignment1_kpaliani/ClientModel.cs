using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace assignment1_kpaliani
{
    class ClientModel
    {

        //Information about client
        private Socket _clientSocket;
        private int _sessionNum;
        private string _videoWanted;
        private int _port;

        //Delegate methods to respond when client sends in a RTSP Packet
        private clientSetupDelegate _clientSetupDel;
        private clientPlayDelegate _clientPlayDel;
        private clientPauseDelegate _clientPauseDel;
        private clientTearDownDelegate _clientTearDownDel;

        public ClientModel(int session, Socket clientSocket, clientSetupDelegate clientSetupDel, clientPlayDelegate clientPlayDel, clientPauseDelegate clientPauseDel, clientTearDownDelegate clientTearDownDel) {

            _clientSocket = clientSocket;

            _sessionNum = session;

            _clientSetupDel = clientSetupDel;
            _clientPlayDel = clientPlayDel;
            _clientPauseDel = clientPauseDel;
            _clientTearDownDel = clientTearDownDel;
        }

        //Listens for incoming RTSP packets
        public void listen() {

            byte[] rtspPacketBytes = new byte [4096];

            while (true) {
                try {
                    int bytesReceived = _clientSocket.Receive(rtspPacketBytes);
                    if (bytesReceived > 0) {
                        string rtspPacket = Encoding.UTF8.GetString(rtspPacketBytes).TrimEnd(new char[] { (char)0 });
                        rtspAction(rtspPacket);
                        Array.Clear(rtspPacketBytes, 0, 4096);
                    }
                } catch (SocketException err) {
                    Console.WriteLine(err);
                }    
            }
        }

        public void rtspAction(string rtspPacket) {

            //Parsing the RTSP packet
            string[] packetLines = rtspPacket.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            string[] requestLine = packetLines[0].Split(' ');
            string action = requestLine[0];
            int videoLocation = requestLine[1].IndexOf("/video");
            _videoWanted = requestLine[1].Substring(videoLocation + 1);

            string[] cseqLine = packetLines[1].Split(' ');
            int sequenceNum = Int32.Parse(cseqLine[1]);

            if (action == "SETUP") {
                int portLocation = packetLines[2].IndexOf("=");
                _port = Int32.Parse(packetLines[2].Substring(portLocation + 2));
            }

            //Call appropiate action on controller
            if (action == "SETUP") {
                _clientSetupDel(_clientSocket, _port, _sessionNum, _videoWanted, rtspPacket);
            } else if (action == "PLAY") {
                _clientPlayDel(_clientSocket, _port, _sessionNum, rtspPacket, _videoWanted);
            } else if (action == "PAUSE") {
                _clientPauseDel(_clientSocket, _port, _sessionNum, rtspPacket, _videoWanted);
            } else if (action == "TEARDOWN") {
                _clientTearDownDel(_clientSocket, _sessionNum, rtspPacket);
            }

            //Response to client
            string response = "RTSP/1.0 200 OK" + Environment.NewLine + "CSeq: " + sequenceNum + Environment.NewLine + "Session: " + _sessionNum;
            byte[] responseBytes = Encoding.ASCII.GetBytes(response);
            try {
                _clientSocket.Send(responseBytes);
            } catch (SocketException err) {
                Console.WriteLine(err);
            }
        }
    }
}
