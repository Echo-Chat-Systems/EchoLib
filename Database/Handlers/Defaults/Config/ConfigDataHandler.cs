using Database.Handlers.Interface;

namespace Database.Handlers.Defaults.Config;

public class ConfigDataHandler : BaseHandler, IConfigDataHandler
{
	public Task Create(string key, object value)
	{
		throw new NotImplementedException();
	}

	public Task<T?> Get<T>(string key)
	{
		throw new NotImplementedException();
	}

	public Task Update<T>(string key, T value)
	{
		throw new NotImplementedException();
	}

	public Task Delete(string key)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Exists(string key)
	{
		throw new NotImplementedException();
	}
}