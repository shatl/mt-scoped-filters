namespace WebApi
{
    using System;
    using Microsoft.Extensions.Logging;


    public class Token : IDisposable
    {
        private readonly ILogger<Token> _logger;
        private string _value;

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                _logger.LogInformation("Set token {Token}", value);
            }
        }

        public Token(ILogger<Token> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Created token {TokenId}", GetHashCode());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _logger.LogInformation("Disposed token {TokenId}", GetHashCode());
        }
    }
}
