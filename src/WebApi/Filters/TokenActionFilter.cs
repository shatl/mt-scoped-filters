namespace WebApi.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;


    public class TokenActionFilter :
        IActionFilter
    {
        private readonly ITokenProvider _tokenProvider;

        public TokenActionFilter(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Token"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                _tokenProvider.SetToken(new Token {Value = token});
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
