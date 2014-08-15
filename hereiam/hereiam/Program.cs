using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace hereiam
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = LocalIPAddress();
            if (addressChanged(address))
            {

                address = address.Replace(".", "_");
                string serial = "no machine";
                if (args.Length > 0)
                {
                    serial = args[0];
                }

                string url = "http://frankensteinosaur.us/htgbot/hereiam/" + address + "/" + serial;
                System.Net.WebRequest request = System.Net.WebRequest.Create(url);
                System.Net.WebResponse response = request.GetResponse();
            }
        }

        public static bool addressChanged(string address)
        {
            bool didItChange = true;
            if (File.Exists("address.txt"))
            {
                StreamReader sr = new StreamReader("address.txt");

                //Read the first line of text
                string line = sr.ReadLine();
                line = line.Trim();
                sr.Close();
                if (address == line)
                {
                    didItChange = false;
                }
             }
            if (didItChange)
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                if (File.Exists("address.txt"))
                {
                    File.Delete("address.txt");
                }
                StreamWriter sw = new StreamWriter("address.txt");
                //Write a line of text
                sw.WriteLine(address);
                //Close the file
                sw.Close();
            }
            return didItChange;
        }

        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

    }
}
