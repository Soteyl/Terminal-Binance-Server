namespace TestASP.NET.Services
{
    public class SmsSender: IMessageSender
    {
        public void Send()
        {
            Console.WriteLine("SMS");
        }
    }
}
