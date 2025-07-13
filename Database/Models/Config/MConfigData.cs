using System.Data;
using System.Data.SqlTypes;

namespace EchoLib.Database.Models.Config;

public class MConfigData(IDataRecord record)
{
	public string Key { get; } = record.GetString(record.GetOrdinal("key"));
	public byte[] Value { get; set; } = (byte[])record.GetValue(record.GetOrdinal("value"));

	/// <summary>
	/// Tries to get the value as a specific type.
	/// </summary>
	/// <typeparam name="T">Target type.</typeparam>
	/// <returns></returns>
	public T GetValue<T>()
	{
		if (Value.Length == 0) throw new SqlNullValueException(nameof(Value));

		// Attempt to cast the byte array to the target type
		return (T)Convert.ChangeType(Value, typeof(T));
	}
}