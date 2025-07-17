namespace Core.Helpers.Snowflake;

public class Snowflake
{
	public ulong Value { get; }
	
	public DateTime Timestamp { get; }
	public byte ApiVersion { get; }
	public ulong Increment { get; }
	
	public Snowflake(ulong snowflake)
	{
		Value = snowflake;
		long timestamp = (long)(snowflake >> 20) + SnowflakeGenerator.Epoch;
		Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
		ApiVersion = (byte)((snowflake >> 16) & 0xF);
		Increment = snowflake & 0xFFFF;
	}
}