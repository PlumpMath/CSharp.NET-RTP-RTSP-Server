using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1_kpaliani
{
    class RTP_Packet
    {
        //Holds frame
        private byte[] _frame;

        //Holds header
        private byte[] _rtpPacketHeader;

        //Constructor for rtppacket
        public RTP_Packet(byte[] frameContent, byte[] sequenceNum, int timeStamp, int ssrc) {

            //Create packet header
            _rtpPacketHeader = new byte[12];
            byte[] rtpPacketHeaderPortion1 = { 128, 26 };
            Buffer.BlockCopy(rtpPacketHeaderPortion1, 0, _rtpPacketHeader, 0, 2);
            Buffer.BlockCopy(sequenceNum, 0, _rtpPacketHeader, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(timeStamp), 0, _rtpPacketHeader, 4,4);
            Buffer.BlockCopy(BitConverter.GetBytes(ssrc), 0, _rtpPacketHeader, 8, 4);

            //Create frame
            _frame = new byte[_rtpPacketHeader.Length + frameContent.Length];
            Buffer.BlockCopy(_rtpPacketHeader, 0, _frame, 0, _rtpPacketHeader.Length);
            Buffer.BlockCopy(frameContent, 0, _frame, _rtpPacketHeader.Length, frameContent.Length);
        }

        //Return frame
        public byte[] getPacket() {
            return _frame;
        }

        //Return header
        public byte[] getHeader() {
            return _rtpPacketHeader;
        }

    }
}