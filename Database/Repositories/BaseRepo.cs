using System.Data;
using System.Data.Common;
using Dapper;

namespace Database.Repositories;

public class BaseRepo
{
	public required DbDataSource DataSource;

	protected BaseRepo()
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

	protected async Task<T> RunModify<T>(DbCommand command)
	{
		return command.Connection!.ExecuteScalar<T>(command.CommandText, command.Transaction) ?? throw new UpdateFailedException(command);
	}

	protected async Task<T?> RunGet<T>(DbCommand command)
	{
		return command.Connection!.Query<T>(command.CommandText).FirstOrDefault();
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