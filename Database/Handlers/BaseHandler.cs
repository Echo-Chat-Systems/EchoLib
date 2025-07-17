using System.Data.Common;
using EchoLib.Database.Handlers.HandlerGroups;

namespace EchoLib.Database.Handlers.Defaults;

public class BaseHandler
{
	private HandlersGroup? _handlers;
	public required DbDataSource DataSource;

	protected BaseHandler()
	{
	}

	public void Populate(HandlersGroup? handlers)
	{
		_handlers = handlers;
	}
}