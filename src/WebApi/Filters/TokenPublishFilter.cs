namespace WebApi.Filters
{
    using System;
    using System.Threading.Tasks;
    using GreenPipes;
    using MassTransit;
    using Microsoft.Extensions.Logging;


    public class TokenPublishFilter<T> :
        IFilter<PublishContext<T>>
        where T : class
    {
        private readonly ILogger<TokenPublishFilter<T>> _logger;
        private readonly ITokenProvider _tokenProvider;

        public TokenPublishFilter(ITokenProvider tokenProvider, ILogger<TokenPublishFilter<T>> logger)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Created for {Type}", typeof(T).Name);
        }

        public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {
            var token = _tokenProvider.GetToken();
            if (token != null) context.Headers.Set("Token", token.Value);
            _logger.LogInformation("Attached token {Token}", token);

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
            => context.CreateScope("publish-token");
    }
}
