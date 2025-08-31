namespace UDPNetwork
{
    internal interface Interface1
    {
        double Time_latenncy { get; set; }
        string This_port { get; set; }
        double ClaculateLatency(int latency);
        double GetLatency();
        string SetPort(string port);
        string GetPort();
    }
}
