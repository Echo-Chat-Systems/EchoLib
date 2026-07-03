namespace Models.User;

public class ProfileModel
{
	/// <summary>
	/// Username.
	/// </summary>
	public required string Username { get; set; }

	/// <summary>
	/// User's tag
	/// </summary>
	public required int Tag { get; set; }

	/// <summary>
	/// User pronouns.
	/// </summary>
	public string? Pronouns { get; set; }

	/// <summary>
	/// User bio.
	/// </summary>
	public string? Bio { get; set; }

	/// <summary>
	/// User customisable CSS.
	/// </summary>
	public string? Css { get; set; }

	/// <summary>
	/// User profile picture.
	/// </summary>
	public string? Pfp { get; set; }

	/// <summary>
	/// User banner.
	/// </summary>
	public string? Banner { get; set; }

	/// <summary>
	/// User timezone information.
	/// </summary>
	public TimeZoneInfo? TimeZone { get; set; }

	/// <summary>
	/// User status.
	/// </summary>
	public StatusModel? Status { get; set; }
}