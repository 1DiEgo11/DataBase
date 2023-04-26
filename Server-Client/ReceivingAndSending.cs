using System.Net.Sockets;
using System.Text;
using System;

namespace Server_Client
{
    public class ReceivingAndSending
    {
        public static string Receiving(NetworkStream stream)
        {
            byte[] bytes = new byte[1024];
            int lenght = stream.Read(bytes, 0, bytes.Length);
            string request = Encoding.UTF8.GetString(bytes, 0, lenght);
            return request;
        }//suck my cockies


        public static void Sending(NetworkStream stream, string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
    }
}