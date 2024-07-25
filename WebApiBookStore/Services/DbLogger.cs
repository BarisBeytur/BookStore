using System;

namespace WebApiBookStore.Services
{
    /// <summary>
    /// Bu sınıf, veritabanına loglama işlemlerini gerçekleştirir.
    /// </summary>
    public class DbLogger : ILoggerService
    {

        public void Write(string message)
        {
            Console.WriteLine("[DbLogger] - " + message);
        }
    }
}
