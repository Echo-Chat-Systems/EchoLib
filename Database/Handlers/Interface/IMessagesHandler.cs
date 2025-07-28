using Core.Helpers.Snowflake;
using Database.Models.NoSql;

namespace Database.Handlers.Interface;

public record PagingToken(ulong LastSeen, Guid Channel);

public interface IMessagesHandler
{
	public Task<Snowflake> Add(MMessage message);
	public Task Update(MMessage message);
	public Task Delete(MMessage message);

	public Task<IEnumerable<MMessage>> GetMany(Guid channelId, PagingToken? pagingToken = null, Func<IEnumerable<MMessage>, IEnumerable<MMessage>>? selector = null);
}