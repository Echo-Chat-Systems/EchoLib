using System.Data.Common;
using EchoLib.Database.Models;
using Org.BouncyCastle.Utilities.IO;

namespace EchoLib.Database.Handlers;

public class BaseHandler
{
	private HandlersGroup? _handlers;
	public required DbDataSource DataSource;

	protected BaseHandler()
	{
	}

	public void Populate(HandlersGroup? handlers)
	{
		_handlers = handlers;
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