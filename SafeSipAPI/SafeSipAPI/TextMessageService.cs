using SafeSipAPI.Model;

namespace SafeSipAPI
{
    public class TextMessageService : IHostedService, IDisposable
    {
        private readonly ILogger<TextMessageService> _logger;
        private Timer _timer;

        public TextMessageService(ILogger<TextMessageService> logger)
        {
            _logger = logger;
        }

        private void DoWork(object? state)
        {
            SMSMessage? message = SQLDB.Instance.GetTamperedMessage();
            if (message == null) { return; }

            message.SendNow();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}
