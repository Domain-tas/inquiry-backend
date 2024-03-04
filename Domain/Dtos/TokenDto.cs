namespace Domain.Dtos;

public class TokenDto
{
    public string Token { get; set; } = null!;
    public int Expires { get; set; }
}
