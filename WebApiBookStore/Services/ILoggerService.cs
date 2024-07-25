namespace WebApiBookStore.Services
{
    /// <summary>
    /// Bu arayüz, loglama işlemlerini gerçekleştiren sınıfların uygulaması için metotları içerir.
    /// </summary>
    public interface ILoggerService
    {
        public void Write(string message);
    }
}
