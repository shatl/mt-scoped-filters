namespace WebApi.Filters
{
    using System;
    using System.Threading.Tasks;
    using GreenPipes;
    using MassTransit;


    public class TokenConsumeFilter<T> :
        IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly ITokenProvider _tokenProvider;

        public TokenConsumeFilter(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var token = context.Headers.Get<string>("Token");
            if (!string.IsNullOrEmpty(token)) _tokenProvider.SetToken(new Token {Value = token});

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("send-token");
        }
    }
}
