using System.Data;
using System.Data.Common;

namespace Database.Handlers.Defaults;

public class BaseHandler
{
	public required DbDataSource DataSource;

	protected BaseHandler()
	{
	}

	/// <summary>
	/// Create a new command.
	/// </summary>
	/// <param name="includeTransaction">If the method should add a transaction to the command.</param>
	/// <returns>New command.</returns>
	protected async Task<DbCommand> Command(bool includeTransaction)
	{
		DbTransaction? transaction = null;
		DbCommand command = DataSource.CreateCommand();
		if (includeTransaction) transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		
		return command;
	}

	public record Parameter
	{
		public required DbType Type { get; init; }
		public required object? Value { get; init; }
		public bool Nullable { get; init; } = false;
	}

	protected void AddParams(DbCommand command, Dictionary<string, Parameter> parameters)
	{
		DbParameter param;  // Reuse the same memory block
		foreach (KeyValuePair<string,Parameter> parameter in parameters)
		{
			param = command.CreateParameter();
			param.ParameterName = parameter.Key;
			param.DbType = parameter.Value.Type;
			param.IsNullable = parameter.Value.Nullable;
			param.Value = parameter.Value.Value;
		}
	}

	protected async Task<T> RunModify<T>(DbCommand command, Func<DbDataReader, T> converter)
	{
		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (reader.RecordsAffected != 1) goto Fail;
		
		if (await reader.ReadAsync())
		{
			await command.Transaction!.CommitAsync();
			return converter(reader);
		}
		
		Fail:
			await command.Transaction!.RollbackAsync();
			throw new InsertFailedException(command);
	}

	protected async Task<T?> RunGet<T>(DbCommand command, Func<DbDataReader, T> converter)
	{
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync()) return converter(reader);

		return default; // Return null if no category found
	}

	protected async Task RunDelete(DbCommand command)
	{
		int rowsAffected = await command.ExecuteNonQueryAsync();
		if (rowsAffected != 1)
		{
			await command.Transaction!.RollbackAsync();
			throw new DeleteFailedException(command);
		}

		await command.Transaction!.CommitAsync();
	}

	protected async Task<bool> RunExists(DbCommand command)
	{
		object? result = await command.ExecuteScalarAsync();

		return result != null;
	}
}