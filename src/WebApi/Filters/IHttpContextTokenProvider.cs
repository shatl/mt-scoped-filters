namespace WebApi.Filters
{
    public interface ITokenProvider
    {
        Token? GetToken();
        void SetToken(Token token);
    }
}
