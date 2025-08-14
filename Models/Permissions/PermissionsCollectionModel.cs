namespace Models.Permissions;

public class PermissionsCollectionModel
{
	public GuildPermissions? Guild { get; set; }
	public TextPermissions? Channel { get; set; }
	public VoicePermissions? Member { get; set; }
}