using Models;
using Models.Channel;
using Models.Crypto.Signing;
using Models.DatabaseModels.Channel;
using Models.Generic;

namespace Database.Handlers.Interface;

public interface IChannelMembersHandler
{
	public Task<ChannelMemberDbm> Create(UserId userId, Snowflake channelId, long permissions);
	public Task<ChannelMemberDbm?> Get(Snowflake id);
	public Task<ChannelMemberDbm?> Get(UserId userId, Snowflake channelId);
	public Task<ChannelMemberDbm> Update(ChannelMemberDbm channelMember);
	public Task Delete(Snowflake id);
	public Task Delete(UserId userId, Snowflake channelId);
	public Task<bool> Exists(Snowflake id);
	public Task<bool> Exists(UserId userId, Snowflake channelId);
}