namespace Application.Common.Helper;

public static class JwtHelper
{
    public static int GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            throw new ArgumentException("Invalid JWT token");

        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId");
        if (userIdClaim == null)
            throw new ArgumentException("UserId not found in JWT token");

        return int.Parse(userIdClaim.Value);
    }
}