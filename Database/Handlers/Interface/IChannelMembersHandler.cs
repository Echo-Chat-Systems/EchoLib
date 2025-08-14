using Database.Models.Chat;
using Models;
using Models.Channel;
using Models.Crypto.Signing;

namespace Database.Handlers.Interface;

public interface IChannelMembersHandler
{
	public Task<MemberModel> Create(UserId userId, Snowflake channelId, long permissions);
	public Task<MemberModel?> Get(Snowflake id);
	public Task<MemberModel?> Get(UserId userId, Snowflake channelId);
	public Task<MemberModel> Update(MemberModel channelMember);
	public Task Delete(Snowflake id);
	public Task Delete(UserId userId, Snowflake channelId);
	public Task<bool> Exists(Snowflake id);
	public Task<bool> Exists(UserId userId, Snowflake channelId);
}