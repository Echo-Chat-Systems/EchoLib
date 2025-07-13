using System.Data.Common;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BChannelCategoriesHandler : BaseHandler
{
	public abstract Task<MChannelCategory> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	public abstract Task<MChannelCategory?> Get(Guid id);
	public abstract Task<MChannelCategory> Update(MChannelCategory channel);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid id);
}