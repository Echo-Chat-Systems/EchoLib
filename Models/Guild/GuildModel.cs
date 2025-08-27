using Models.Crypto.Signing;
using Models.DatabaseModels;

namespace Models.Guild;

public class GuildModel : BaseDbm
{
	public required string Name { get; set; }
	public required UserId Owner { get; set; }
	public required ConfigModel Config { get; set; }
	public required CustomisationModel Customisation { get; set; }
}