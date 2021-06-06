using LibCustom.Request;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static int Main(string[] args)
        {
            MailSend mailSend = new("qqwwzoopark@gmail.com", "smtp.gmail.com", "kostyash");
            _ = mailSend.AttachMail("test-hackaton@yandex.ru", "ТЕСТ", "ТЕСТ");
            mailSend.SendMail();
            return 0;
        }
    }
}
