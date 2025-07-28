namespace Core.Helpers.Snowflake;

public class SnowflakeGenerator
{
	public static readonly long Epoch = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds();
	private static readonly object Lock = new object();
	
	private static ushort _increment = 0;
	private long _lastMs = 0;
	
	public byte ApiVersion { get; set; } = 1;
	
	public Snowflake New()
	{
		lock (Lock)
		{
			long ms = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Epoch;
			if (ms < _lastMs)
			{
				throw new InvalidOperationException("Clock moved backwards. Unable to generate snowflake.");
			}

			if (ms == _lastMs)
			{
				_increment++;
				if (_increment > 4095) // 12 bits for increment
				{
					throw new InvalidOperationException("Increment overflow. Unable to generate snowflake.");
				}
			}
			else
			{
				_increment = 0;
			}

			_lastMs = ms;

			return new Snowflake((ulong)(ms << 44) | ((ulong)ApiVersion << 16) | _increment);
		}
	}
}