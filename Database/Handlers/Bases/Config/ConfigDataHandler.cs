using System.Data.Common;
using EchoLib.Database.Models.Config;

namespace EchoLib.Database.Handlers.Bases.Config;

public class ConfigDataHandler : BaseHandler
{
	public async Task<MConfigData> Create(string key, object value)
	{
		
	}

	public async Task<MConfigData?> Get(string key)
	{

	}

	public async Task<MConfigData> Update(MConfigData row)
	{

	}

	public async Task Delete(string key)
	{

	}

	public async Task<bool> Exists(string key)
	{

	}
}