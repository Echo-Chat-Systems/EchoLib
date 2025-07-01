using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IChannelCategoriesHandler
{
	ChannelCategoryRow Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	ChannelCategoryRow? Get(Guid id);
	ChannelCategoryRow Update(ChannelCategoryRow channel);
	void Delete(Guid id);
	bool Exists(Guid id);
}