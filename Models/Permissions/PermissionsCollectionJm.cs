namespace Models.Permissions;

public class PermissionsCollectionJm
{
	public GuildPermissions? Guild { get; set; }
	public TextPermissions? Channel { get; set; }
	public VoicePermissions? Member { get; set; }
}