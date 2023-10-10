namespace OnlineLibrarySystem.Jwt;

public interface IJwtTokenHandler
{
    public Task<string> GetToken(string userId);
}