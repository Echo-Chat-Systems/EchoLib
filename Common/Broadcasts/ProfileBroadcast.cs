namespace Models.Common.Broadcasts;

public class ProfileBroadcast
{
	public required string Username { get; set; }
	public required string Pronouns { get; set; }
	public required string Bio { get; set; }
	public required string Css { get; set; }
	public required string Pfp { get; set; }
	public required string Banner { get; set; }
	public required string Timezone { get; set; }
	public required UserStatus Status { get; set; }


}