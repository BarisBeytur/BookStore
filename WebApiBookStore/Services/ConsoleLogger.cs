using System;

namespace WebApiBookStore.Services
{
    /// <summary>
    /// Bu sınıf, console'a loglama işlemlerini gerçekleştirir.
    /// </summary>
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
