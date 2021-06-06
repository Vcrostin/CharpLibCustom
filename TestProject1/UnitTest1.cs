using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibCustom.Request;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Тест проверки MailSend
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var utf8reader = new Utf8JsonReader(File.ReadAllBytes("login_data.json"));
            var array = JsonSerializer.Deserialize<Dictionary<string, string>>(ref utf8reader);
            MailSend mailSend = new(array["mail"], array["smtpserver"], array["password"],int.Parse(array["port"]));
            var k = mailSend.AttachMail("test-hackaton@yandex.ru", "ТЕСТ", "ТЕСТ");
            var variable = mailSend.AttachMail("qqwwzoopark@gmail.com", "", "");
            mailSend.SendMail();
            Assert.AreEqual(true, k.Item1&variable.Item1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var utf8reader = new Utf8JsonReader(File.ReadAllBytes("login_data.json"));
            var array = JsonSerializer.Deserialize<Dictionary<string, string>>(ref utf8reader);
            MailSend mailSend = new(array["mail"], array["smtpserver"], array["password"], int.Parse(array["port"]));
            var variable = mailSend.AttachMail("qqwwzoopark@gmail.com", "", "");
            Console.WriteLine(variable.Item2);
            mailSend.SendMail();
            Assert.IsTrue(variable.Item1);
        }
    }
}
