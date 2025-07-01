using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IChannelMembersHandler
{
	ChannelMemberRow Create(PublicSigningKey userId, Guid channelId, long permissions);
	ChannelMemberRow? Get(Guid id);
	ChannelMemberRow? Get(PublicSigningKey userId, Guid channelId);
	ChannelMemberRow Update(ChannelMemberRow channelMember);
	void Delete(Guid id);
	void Delete(PublicSigningKey userId, Guid channelId);
	bool Exists(Guid id);
	bool Exists(PublicSigningKey userId, Guid channelId);
}