using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

class TcpListener
{
    public static IPAddress getIp()
    {
        string host = Dns.GetHostName();
        var address = Dns.GetHostEntry(host);

        foreach (var ip in address.AddressList)
        {
            return ip;
        }
        return null;
    }

    public static void getData(Socket handler, Socket listener)
    {
        String data = null;
        byte[] bytes = null;

        while (true)
        {
                        
            bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes);
            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

            if (data.IndexOf("8==.") > -1)
            {
                break;
            }
            
            Console.WriteLine("TEXT RECEIVED: " + data);

        listener.Listen(100);
        handler = listener.Accept();

        }
    }

    public static void socketInit(IPEndPoint endPoint, IPAddress ip)
    {
        Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(endPoint);

        listener.Listen(100);
        Console.WriteLine("Awaiting connection...\n");
        Socket handler = listener.Accept();
        Console.WriteLine("Connected!");

        getData(handler, listener);
    }

    public static void Main(string[] Args)
    {
        //try{
        Int32 port = 6942;
        IPAddress ip = getIp();
        IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
        Console.WriteLine("ENDPOINT:" + ipEndPoint);

        socketInit(ipEndPoint, ip);
        //}


    }
}
