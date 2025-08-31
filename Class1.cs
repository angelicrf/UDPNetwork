using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace UDPNetwork
{
    public class Class1 : Interface1
    {
        public double Time_latenncy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string This_port { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double ClaculateLatency(int latency)
        {
            return Math.PI * Time_latenncy * Time_latenncy;
        }

        public double GetLatency()
        {
            try
            {
                if (Time_latenncy > 1.1)
                {
                    Console.WriteLine("Valid");
                    return Time_latenncy                }
                else
                {
                    Console.WriteLine("InValid");
                    return 0.0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string SetPort(string port)
        {
            return This_port = port;
        }

        public string GetPort()
        {
            try
            {
                return string.IsNullOrEmpty(This_port) ? "8.8.8.8" : This_port;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
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
        public static void PingMethod(string googleIp = "8.8.8.8", int timeToIterate = 6, int timeOut = 3000)
        {


            using (Ping thisPing = new Ping())
            {
                double countLatency = 0;
                int sucess = 0;

                Console.WriteLine($"Start to Ping {timeToIterate}");
                for (int i = 0; i < timeToIterate; i++)
                {
                    try
                    {
                        PingReply pingReply = thisPing.Send(googleIp, timeOut);
                        if (pingReply.Status == IPStatus.Success)
                        {
                            sucess++;
                            countLatency += pingReply.RoundtripTime;
                            Console.WriteLine($"{pingReply.RoundtripTime}");
                        }
                        else
                        {
                            Console.WriteLine($"Packet {i + 1}: Failed, Status: {pingReply.RoundtripTime}");
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        throw;
                    }
                }
                double packetLossPercentage = ((timeToIterate - sucess) / (double)timeToIterate) * 100;
                double averageLatency = sucess > 0 ? countLatency / sucess : 0;

                string summary = $"\nNetwork Diagnosis Summary for {googleIp}:\n" +
                     $"Packets Sent: {timeToIterate}\n" +
                     $"Packets Received: {sucess}\n" +
                     $"Packet Loss: {packetLossPercentage:F2}%\n" +
                     $"Average Latency: {averageLatency:F2} ms\n" +
                     $"Diagnosis: {(averageLatency > 60 ? "High latency detected. May impact gaming or real-time applications." : "Latency within acceptable range for most applications.")}\n" +
                     $"{(packetLossPercentage > 10 ? "Significant packet loss detected. Possible network issue." : "Packet loss within acceptable limits.")}";
                Console.WriteLine(summary);
            }
        }
        public static void CalculateLatency()
        {
            string targetHost = "8.8.8.8";
            int targetPort = 3074;
            int packetsToSend = 5;
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