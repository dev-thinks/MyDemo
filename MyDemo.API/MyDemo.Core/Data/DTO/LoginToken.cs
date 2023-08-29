
namespace MyDemo.Core.Data.DTO;

public class LoginToken
{
    /// <summary>
    /// The JWT token if the login attempt is successful, or NULL if not
    /// </summary>
    public string? access_token { get; set; }
    public string? user_id { get; set; }
    public string? token_type { get; set; }
    /// <summary>
    /// expires time in seconds
    /// </summary>
    /// <value></value>
    public int? expires_in { get; set; }
}