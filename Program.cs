using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;


namespace UDPNetwork
{
    class Program
    {
       Class1 Class1 { get; set; }
   
        static void Main(string[] args)
        {
            //Class1.StartMethode();
            Class1.PingMethod();
            Class1.CalculateLatency();
        }
    }
}
