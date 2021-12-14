namespace TestASP.NET.Services
{
    public class MailSender: IMessageSender
    {
        public void Send()
        {
            Console.WriteLine("MAIL");
        }
    }
}
