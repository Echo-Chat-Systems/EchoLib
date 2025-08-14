using System.ComponentModel.DataAnnotations;
using Models.Permissions;

namespace Models.Guild;

public class RoleModel : BaseEntityModel
{
	[Required] public required string Name { get; set; }
	[Required] public required PermissionsCollectionModel Permissions { get; set; }
	public RoleCustomisationModel? Customisation { get; set; }
}