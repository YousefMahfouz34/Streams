namespace IDisposableTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (currency currency = new currency())
            {
                var result = currency.GetCurrencies();
                Console.WriteLine(result);
            }
        }
    }
    class currency : IDisposable
    {
        bool isdisposed = false;
        private readonly HttpClient client;
        public currency()
        {
            client = new HttpClient();
        }
        ~currency()
        {
            Dispose(false);
        }
        public string GetCurrencies()
        {

            string url = "https://coinbase.com/api/v2/currencies";
            var res = client.GetStringAsync(url).Result;
            return res;


        }

        protected virtual void Dispose(bool dispoising)
        {
            if (isdisposed)
                return;
            if (dispoising)
                client.Dispose();
            isdisposed = true;


        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
