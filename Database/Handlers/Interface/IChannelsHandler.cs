using Database.Models;
using Database.Models.Chat;

namespace Database.Handlers.Interface;

public interface IChannelsHandler
{
	public Task<MChannel> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	public Task<MChannel?> Get(Guid id);
	public Task<MChannel> Update(MChannel mChannel);
	public Task Delete(Guid id);
	public Task<bool> Exists(Guid id);
}