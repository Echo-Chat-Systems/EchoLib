using System.Text.Json.Serialization;
using Models.Postie;

namespace Models.Channel;

/// <summary>
/// Channel configuration model.
/// </summary>
public class ChannelConfigJm
{
	[JsonPropertyName("modes")] public IEnumerable<string>? AllowedModes { get; set; }
	[JsonPropertyName("encrypted")] public bool? IsEncrypted { get; set; }
	public AccessModel? Access { get; set; }
	public NotifConfigModel? Notifications { get; set; }

	/// <summary>
	/// Channel access control model.
	/// </summary>
	public class AccessModel
	{
		[JsonPropertyName("minimum-role-index")]
		public int? MinimumRoleIndex { get; set; }
	}


	public static class Modes
	{
		public const string Text = "text";
		public const string Voice = "voice";
	}
}