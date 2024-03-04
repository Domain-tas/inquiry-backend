namespace RestApi.Controllers.v1.Auth.Responses;

public class SuccessfulLoginResponse
{
    public int Expires { get; set; }
    public string Token { get; set; } = null!;
}
