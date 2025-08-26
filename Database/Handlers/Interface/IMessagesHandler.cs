using Database.Models;
using Models;
using Models.Generic;

namespace Database.Handlers.Interface;

// ReSharper disable once ClassNeverInstantiated.Global
public record PagingToken(ulong LastSeen, Guid Channel);

public interface IMessagesHandler
{
	public Task<Snowflake> Add(DMessage message);
	public Task Update(DMessage message);
	public Task Delete(DMessage message);

	public Task<IEnumerable<DMessage>> GetMany(Guid channelId, PagingToken? pagingToken = null, Func<IEnumerable<DMessage>>? selector = null);
}