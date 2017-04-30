using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace assignment1_kpaliani {
    class RTSPModel {

        private createClientDelegate _createClientDel;

        public RTSPModel(createClientDelegate createClientDel) {
            _createClientDel = createClientDel;
        }

        //Listens for incoming connections and creates a socket
        public void listen(Socket listeningSocket) {
            while (true) {
                try {
                    Socket tcpClient = listeningSocket.Accept();
                    _createClientDel(tcpClient);
                } catch (SocketException err) {
                    System.Diagnostics.Debug.WriteLine(err);
                }
            }
        }

    }
}
