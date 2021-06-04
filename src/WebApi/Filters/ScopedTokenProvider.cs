namespace WebApi.Filters
{
    using System;
    using Microsoft.Extensions.Logging;


    public class ScopedTokenProvider : ITokenProvider, IDisposable
    {
        private readonly ILogger<ScopedTokenProvider> _logger;
        private Token? _token;

        public ScopedTokenProvider(ILogger<ScopedTokenProvider> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Created {InstanceId:x}", GetHashCode());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _logger.LogInformation("Disposed {InstanceId:x}", GetHashCode());
        }

        /// <inheritdoc />
        public Token? GetToken()
            => _token;

        /// <inheritdoc />
        public void SetToken(Token token)
        {
            _token = token;
        }
    }
}
