namespace WebApi.Filters
{
    using System;
    using Microsoft.AspNetCore.Http;


    public class HttpContextTokenProvider : ITokenProvider
    {
        private const string TokenHttpKey = "http-item-token-key";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Token? GetToken()
            => _httpContextAccessor.HttpContext?.Items[TokenHttpKey] as Token;

        public void SetToken(Token token)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null) httpContext.Items[TokenHttpKey] = token;
        }
    }
}
