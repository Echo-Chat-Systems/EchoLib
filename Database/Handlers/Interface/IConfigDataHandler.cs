using System.Data;
using System.Data.Common;

namespace Database.Handlers.Interface;

public interface IConfigDataHandler
{
	/// <summary>
	/// Create a new config item.
	/// </summary>
	/// <param name="key">Item key.</param>
	/// <param name="value">Item value.</param>
	public Task Create(string key, object value);

	/// <summary>
	/// Get a config item.
	/// </summary>
	/// <param name="key">Item key.</param>
	/// <typeparam name="T">Expected type of value.</typeparam>
	/// <returns>Value.</returns>
	public Task<T?> Get<T>(string key);
	public Task Update<T>(string key, T value);
	public Task Delete(string key);
	public Task<bool> Exists(string key);
}