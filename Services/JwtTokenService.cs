using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenService
{
    // Vars
    private readonly IConfiguration _configuration;


    // Constructor
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    // Methods
    public string GenerateToken(string username)
    {
        // Get the JWT key from environment variables
        var jwtKey = Environment.GetEnvironmentVariable("MyAPI_JWT_KEY") ?? throw new Exception("JWT key not found in environment variables");

        // Get the JWT settings from appsettings.json
        var jwtSettings = _configuration.GetSection("Jwt");

        // Create the JWT token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            NotBefore = DateTime.UtcNow,
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }




    // END OF CLASS //
}