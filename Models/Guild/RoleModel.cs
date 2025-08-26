using System.ComponentModel.DataAnnotations;
using Models.Permissions;

namespace Models.Guild;

public class RoleModel : BaseDbm
{
	[Required] public required string Name { get; set; }
	[Required] public required PermissionsCollectionJm Permissions { get; set; }
	public RoleCustomisationModel? Customisation { get; set; }
}