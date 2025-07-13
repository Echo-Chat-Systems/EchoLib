using System.Data.Common;
using EchoLib.Database.Models.Config;

namespace EchoLib.Database.Handlers.Bases.Config;

public abstract class BConfigDataHandler : BaseHandler
{
	public abstract Task<MConfigData> Create(string key, object value);
	public abstract Task<MConfigData?> Get(string key);
	public abstract Task<MConfigData> Update(MConfigData row);
	public abstract Task Delete(string key);
	public abstract Task<bool> Exists(string key);
}