namespace UDPNetwork
{
    internal interface Interface1
    {
        double Time_latenncy { get; set; }
        int This_port { get; set; }
        string IpAddress { get; set; }
        double ClaculateLatency(int latency);

    }
}
