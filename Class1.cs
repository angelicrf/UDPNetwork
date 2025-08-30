using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace UDPNetwork
{
	public class Class1
	{
        public static void StartMethod()
        {
            Console.WriteLine("My Class One");
            int target_value = 0;
            int invremented_latency = 0;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }
        }
        public static void PingMethod() { 
                string googleIp = "8.8.8.8";
                int timeToIterate = 6;
                int timeOut = 3000;

                using(Ping thisPing = new Ping())
                {
                    int countLatency = 0;
                    int sucess = 0; 

                    Console.WriteLine($"Start to Ping {timeToIterate}");
                    for (int i = 0; i < timeToIterate; i++)
                    {
                        try
                        {
                            PingReply pingReply = thisPing.Send(googleIp, timeToIterate);
                            if (pingReply.Status == IPStatus.Success)
                            {
                                sucess++;
                                countLatency += (int)pingReply.RoundtripTime;

                            }


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                            throw;
                        }
                    double packetLossPercentage = ((timeToIterate - sucess) / (double)timeToIterate) * 100;
                    double averageLatency = sucess > 0 ? countLatency / sucess : 0;

                    Console.WriteLine("\nTest Summary:");
                    Console.WriteLine($"Packets Sent: {timeToIterate}");
                    Console.WriteLine($"Packets Received: {sucess}");
                    Console.WriteLine($"Packet Loss: {packetLossPercentage:F2}%");
                    Console.WriteLine($"Average Latency: {averageLatency:F2} ms");
                }
                }
        }
        public static void CalculateLatency()
        {
            string targetHost = "8.8.8.8"; 
            int targetPort = 3074; 
            int packetsToSend = 10; 
            int timeoutMs = 3000;

            using (UdpClient udpClient = new UdpClient())
            {
                IPEndPoint targetEndpoint = new IPEndPoint(IPAddress.Parse(targetHost), targetPort);
                IPEndPoint receiveEndpoint = new IPEndPoint(IPAddress.Any, 0);

                int successfulPackets = 0;
                double totalLatency = 0;

                for (int i = 0; i < packetsToSend; i++)
                {
                    try
                    {
                     
                        string message = $"TestPacket_{i}_{DateTime.UtcNow.Ticks}";
                        byte[] sendData = Encoding.ASCII.GetBytes(message);
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        udpClient.Send(sendData, sendData.Length, targetEndpoint);
                        Console.WriteLine($"Sent packet {i + 1}/{packetsToSend}");

                        udpClient.Client.ReceiveTimeout = timeoutMs;
                        byte[] receiveData = udpClient.Receive(ref receiveEndpoint);
                        stopwatch.Stop();
                        double latencyMs = stopwatch.Elapsed.TotalMilliseconds;
                        totalLatency += latencyMs;
                        successfulPackets++;

                        Console.WriteLine($"Received response for packet {i + 1}. Latency: {latencyMs:F2} ms");
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine($"Packet {i + 1} failed: {ex.Message} (Possible packet loss or timeout)");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error on packet {i + 1}: {ex.Message}");
                    }
                }
                double packetLossPercentage = ((packetsToSend - successfulPackets) / (double)packetsToSend) * 100;
                double averageLatency = successfulPackets > 0 ? totalLatency / successfulPackets : 0;

                Console.WriteLine("\nTest Summary:");
                Console.WriteLine($"Packets Sent: {packetsToSend}");
                Console.WriteLine($"Packets Received: {successfulPackets}");
                Console.WriteLine($"Packet Loss: {packetLossPercentage:F2}%");
                Console.WriteLine($"Average Latency: {averageLatency:F2} ms");
            }

            Console.WriteLine("Test completed. Press any key to exit.");
            Console.ReadKey();
        }
    }
       
	
}