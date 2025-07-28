using Database.Models.Chat;

namespace Database.Handlers.Interface;

public interface IChannelCategoriesHandler
{
	public Task<MChannelCategory> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	public Task<MChannelCategory?> Get(Guid id);
	public Task<MChannelCategory> Update(MChannelCategory channel);
	public Task Delete(Guid id);
	public Task<bool> Exists(Guid id);
}