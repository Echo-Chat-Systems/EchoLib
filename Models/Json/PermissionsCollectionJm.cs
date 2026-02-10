using Models.Permissions;

namespace Models.Json;

public class PermissionsCollectionJm
{
	public GuildPermissions? Guild { get; set; }
	public TextPermissions? Channel { get; set; }
	public VoicePermissions? Member { get; set; }
}