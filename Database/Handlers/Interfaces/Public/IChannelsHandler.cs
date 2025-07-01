using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IChannelsHandler
{
	Channel Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	Channel? Get(Guid id);
	Channel Update(Channel channel);
	void Delete(Guid id);
	bool Exists(Guid id);
}