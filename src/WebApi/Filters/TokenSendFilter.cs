namespace WebApi.Filters
{
    using System;
    using System.Threading.Tasks;
    using GreenPipes;
    using MassTransit;
    using Microsoft.Extensions.Logging;


    public class TokenSendFilter<T> :
        IFilter<SendContext<T>>
        where T : class
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<TokenSendFilter<T>> _logger;

        public TokenSendFilter(ITokenProvider tokenProvider, ILogger<TokenSendFilter<T>> logger)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            var token = _tokenProvider.GetToken();
            if (token != null) context.Headers.Set("Token", token.Value);
            _logger.LogInformation("Attached token {Token}", token);
            return next.Send(context);
        }

        public void Probe(ProbeContext context)
            => context.CreateScope("send-token");
    }
}
