using EchoLib.Database.Models.Config;

namespace EchoLib.Database.Handlers.Interfaces.Config;

public interface IConfigDataHandler
{
	DataRow Create(string key, object value);
	DataRow? Get(string key);
	DataRow Update(DataRow row);
	void Delete(string key);
	bool Exists(string key);
}