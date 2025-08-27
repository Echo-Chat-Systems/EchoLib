using System.ComponentModel.DataAnnotations;
using Models.DatabaseModels;
using Models.Permissions;

namespace Models.Guild;

public class RoleModel : BaseDbm
{
	public required string Name { get; set; }
	public required PermissionsCollectionJm Permissions { get; set; }
	public RoleCustomisationModel? Customisation { get; set; }
}