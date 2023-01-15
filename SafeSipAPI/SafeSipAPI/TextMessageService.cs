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
            //_logger.Log(LogLevel.Information, "Asking SQL for messages...");
            SMSMessage? message = SQLDB.Instance.GetTamperedMessage();
            //_logger.Log(LogLevel.Information, $"SQL responded with {message}");

            if (message == null) { return; }
            message.SendNow();

            //_logger.Log(LogLevel.Information, $"Sent message about {message.FullName} to {message.PersonalPhoneNumber}{Environment.NewLine}\n");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
            //_logger.Log(LogLevel.Information, "Starting up service...");
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
