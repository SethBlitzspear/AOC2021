using System;
using System.Collections.Generic;

namespace AOC2021_Day16
{

    class Packet
    {
        int version;
        int iD;
        List<Packet> subPackets = new List<Packet>();
        Int64 literal;

        public int Version { get => version; set => version = value; }
        public int ID { get => iD; set => iD = value; }
        public Int64 Literal { get => literal; set => literal = value; }
        internal List<Packet> SubPackets { get => subPackets; set => subPackets = value; }

        public int GetVersionTotal()
        {
            int versionTotal = Version;
            foreach (Packet packet in SubPackets)
            {
                versionTotal += packet.GetVersionTotal();
            }
            return versionTotal;
        }

        public Int64 GetPacketValue()
        {
            switch(ID)
            {
                case 0:
                    Int64 total = 0;
                    foreach (Packet packet in subPackets)
                    {
                        total += packet.GetPacketValue();
                    }
                    return total;

                case 1:
                    total = 1;
                    foreach (Packet packet in subPackets)
                    {
                        total *= packet.GetPacketValue();
                    }
                    return total;
                case 2:
                    total = int.MaxValue;
                    foreach (Packet packet in subPackets)
                    {
                        if (packet.GetPacketValue() < total)
                        {
                            total = packet.GetPacketValue();
                        }
                    }
                    return total;
                case 3:
                    total = int.MinValue;
                    foreach (Packet packet in subPackets)
                    {
                        if (packet.GetPacketValue() > total)
                        {
                            total = packet.GetPacketValue();
                        }
                    }
                    return total;
                case 4:
                    return Literal;
                case 5:
                    Int64 val1 = subPackets[0].GetPacketValue();
                    Int64 val2 = subPackets[1].GetPacketValue();
                    if (val1 > val2)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 6:
                     val1 = subPackets[0].GetPacketValue();
                     val2 = subPackets[1].GetPacketValue();
                    if (val1 < val2)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 7:
                     val1 = subPackets[0].GetPacketValue();
                     val2 = subPackets[1].GetPacketValue();
                    if (val1 == val2)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }


                default:
                    return 0;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string Puzzleinput = "F600BC2D8F";
            List<Packet> Packets = new List<Packet>();
            Puzzleinput = "220D62004EF14266BBC5AB7A824C9C1802B360760094CE7601339D8347E20020264D0804CA95C33E006EA00085C678F31B80010B88319E1A1802D8010D4BC268927FF5EFE7B9C94D0C80281A00552549A7F12239C0892A04C99E1803D280F3819284A801B4CCDDAE6754FC6A7D2F89538510265A3097BDF0530057401394AEA2E33EC127EC3010060529A18B00467B7ABEE992B8DD2BA8D292537006276376799BCFBA4793CFF379D75CA1AA001B11DE6428402693BEBF3CC94A314A73B084A21739B98000010338D0A004CF4DCA4DEC80488F004C0010A83D1D2278803D1722F45F94F9F98029371ED7CFDE0084953B0AD7C633D2FF070C013B004663DA857C4523384F9F5F9495C280050B300660DC3B87040084C2088311C8010C84F1621F080513AC910676A651664698DF62EA401934B0E6003E3396B5BBCCC9921C18034200FC608E9094401C8891A234080330EE31C643004380296998F2DECA6CCC796F65224B5EBBD0003EF3D05A92CE6B1B2B18023E00BCABB4DA84BCC0480302D0056465612919584662F46F3004B401600042E1044D89C200CC4E8B916610B80252B6C2FCCE608860144E99CD244F3C44C983820040E59E654FA6A59A8498025234A471ED629B31D004A4792B54767EBDCD2272A014CC525D21835279FAD49934EDD45802F294ECDAE4BB586207D2C510C8802AC958DA84B400804E314E31080352AA938F13F24E9A8089804B24B53C872E0D24A92D7E0E2019C68061A901706A00720148C404CA08018A0051801000399B00D02A004000A8C402482801E200530058AC010BA8018C00694D4FA2640243CEA7D8028000844648D91A4001088950462BC2E600216607480522B00540010C84914E1E0002111F21143B9BFD6D9513005A4F9FC60AB40109CBB34E5D89C02C82F34413D59EA57279A42958B51006A13E8F60094EF81E66D0E737AE08";
           
            string binaryMessage = "";

            foreach (char hex in Puzzleinput)
            {
                binaryMessage += GetBinary(hex);
            }

            while(binaryMessage.Contains("1"))
            {
                Packets.Add(Parsepacket(ref binaryMessage));
            }

            int totalVersion = 0;

            foreach (Packet packet in Packets)
            {
                totalVersion += packet.GetVersionTotal();
            }
            Console.WriteLine(totalVersion);
            Console.WriteLine(Packets[0].GetPacketValue());
        }

        private static Packet Parsepacket(ref string binaryMessage )
        {
            Packet newPacket = new Packet();

            newPacket.Version = Convert.ToInt32(binaryMessage.Substring(0, 3), 2);
            binaryMessage = binaryMessage.Substring(3);
            newPacket.ID = Convert.ToInt32(binaryMessage.Substring(0, 3), 2);
            binaryMessage = binaryMessage.Substring(3);

            switch (newPacket.ID)
            {
                case 4:
                    string literalBinary = "";
                    string literalChunk = " ";
                    int bitcount = 6;
                    do
                    {
                        literalChunk = binaryMessage.Substring(0, 5);
                        binaryMessage = binaryMessage.Substring(5);
                        literalBinary += literalChunk.Substring(1, 4);
                        bitcount += 5;
                    } while (literalChunk[0] != '0');
                    newPacket.Literal = Convert.ToInt64(literalBinary, 2);
                    //int residual = 16 - (bitcount % 16);
                    //binaryMessage = binaryMessage.Substring(residual);
                    break;

                default:
                    char lengthType = binaryMessage[0];
                    binaryMessage = binaryMessage.Substring(1);
                    switch(lengthType)
                    {
                        case '0':
                            int subPacketLength = Convert.ToInt32(binaryMessage.Substring(0, 15), 2);
                            binaryMessage = binaryMessage.Substring(15);

                            string subPacketBinary = binaryMessage.Substring(0, subPacketLength);
                            binaryMessage = binaryMessage.Substring(subPacketLength);
                            while (subPacketBinary.Length > 0)
                            {
                                newPacket.SubPackets.Add(Parsepacket(ref subPacketBinary));
                            }
                            break;
                        case '1':
                            int PacketTotal = Convert.ToInt32(binaryMessage.Substring(0, 11), 2);
                            binaryMessage = binaryMessage.Substring(11);
                            for (int packetCount = 0; packetCount < PacketTotal; packetCount++)
                            {
                                newPacket.SubPackets.Add(Parsepacket(ref binaryMessage));
                            }
                            break;
                    }
                    break;
            }
            return newPacket;
        }

        private static string GetBinary(char hex)
        {
            switch (hex)
            {
                case '0':
                    return "0000";
                case '1':
                    return "0001";
                case '2':
                    return "0010";
                case '3':
                    return "0011";
                case '4':
                    return "0100";
                case '5':
                    return "0101";
                case '6':
                    return "0110";
                case '7':
                    return "0111";
                case '8':
                    return "1000";
                case '9':
                    return "1001";
                case 'A':
                    return "1010";
                case 'B':
                    return "1011";
                case 'C':
                    return "1100";
                case 'D':
                    return "1101";
                case 'E':
                    return "1110";
                case 'F':
                    return "1111";

                default:
                    return null;
            }
        }
    }
}
