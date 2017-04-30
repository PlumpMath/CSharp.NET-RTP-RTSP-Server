using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace assignment1_kpaliani {
    class RTPModel {

        private IPEndPoint _IPEndPoint;

        private Socket s;

        private printRTPHeader _printRTPHeader;

        public RTPModel(IPAddress ipAddress, int port, printRTPHeader printRTPHeader) {
            _IPEndPoint = new IPEndPoint(ipAddress, port);

            _printRTPHeader = printRTPHeader;

            //Create socket for client connection
            s = new Socket(_IPEndPoint.Address.AddressFamily,
                SocketType.Dgram,
                ProtocolType.Udp);
        }

        public void sendVideo(string videoWanted) {

            //Get Random sequence number
            Random rand = new Random();
            byte[] sequenceNum = new byte[] {0,0};
            //rand.NextBytes(sequenceNum);
            short tempSeqNum;

            //Generate random time stamp and ssrc
            int timeStamp = rand.Next();
            int ssrc = rand.Next();

            //MJPEG file location
            string filePath;
            if (videoWanted == "video1.mjpeg") {
                filePath = System.IO.Directory.GetCurrentDirectory() + @"\video1.mjpeg";
            } else {
                filePath = System.IO.Directory.GetCurrentDirectory() + @"\video2.mjpeg";
            }

            //Read file
            using (FileStream fsSource = new FileStream(filePath,
                FileMode.Open, FileAccess.Read)) {

                //RTP Packet to send
                RTP_Packet rtpPacket;

                //Frame without header
                byte[] frame;
                //Frame with header
                byte[] frameToSend;

                //File reader helpers
                byte[] frameLengthBytes;
                int numBytesToRead;
                int numBytesRead;
                int offset = 0;
                int n;
                int frameLength;

                while (true) {

                    // Read the length of the frame
                    frameLengthBytes = new byte[8];
                    numBytesToRead = 5;
                    numBytesRead = offset;

                    n = fsSource.Read(frameLengthBytes, 0, 5);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    //Convert frameLength to int length of bytes of frame
                    frameLength = Convert.ToInt32(System.Text.Encoding.ASCII.GetString(frameLengthBytes));
                    //frameLength = BitConverter.ToInt32(frameLengthBytes, 0);

                    // Read the frame into a byte array
                    frame = new byte[frameLength];
                    numBytesToRead = frameLength;
                    numBytesRead = offset + 5;

                    // Read may return anything from 0 to numBytesToRead.
                    n = fsSource.Read(frame, 0, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    rtpPacket = new RTP_Packet(frame, sequenceNum, timeStamp, ssrc);
                    frameToSend = rtpPacket.getPacket();
                    _printRTPHeader(rtpPacket.getHeader()); 

                    //Send frame to client
                    try {
                        s.SendTo(frameToSend, _IPEndPoint);
                        Thread.Sleep(1000/30);
                    } catch (SocketException err) {
                        Console.WriteLine(err);
                    }
                    

                    //Increase offset
                    offset += (5 + frameLength);

                    //Increase timestamp
                    timeStamp += 100;

                    //Increase sequence number
                    tempSeqNum = BitConverter.ToInt16(sequenceNum, 0);
                    tempSeqNum += 1;
                    sequenceNum = BitConverter.GetBytes(tempSeqNum);

                }

                //Close file
                fsSource.Close();
            }
        }
    }
}
