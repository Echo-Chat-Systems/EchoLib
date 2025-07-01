namespace EchoLib.Models.Net.Users.Reputation;

public class Remark
{
	public required Direction Direction { get; set; }
	public required string Comment { get; set; }
	public required string User { get; set; }
}