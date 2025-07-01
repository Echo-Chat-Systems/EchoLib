using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IMessagesHandler
{
	MessageRow Create(PublicSigningKey sender, Guid channelId, byte[] body, string metadata, out int? code, int? epoch = null, int? senderIndex = null);
	MessageRow? Get(Guid id);
	List<MessageRow> GetMany(Guid channelId, int? epoch = null, int? senderIndex = null, int? limit = null, int? offset = null);
	MessageRow Update(MessageRow message);
	void Delete(Guid id);
	bool Exists(Guid id);
}