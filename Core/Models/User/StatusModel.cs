namespace Database.Models.User;

public class StatusModel
{
	/// <summary>
	/// Status type, e.g. "online", "idle", "dnd", "offline", "playing", "watching", "listening".
	/// </summary>
	public required string? Type { get; set; }

	/// <summary>
	/// Status text, e.g. "a game", "a movie", "to music", "Sleeping".
	/// </summary>
	public required string Text { get; set; }
}