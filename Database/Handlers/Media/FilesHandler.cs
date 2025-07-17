using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Public;

public class FilesHandler : BaseHandler
{
	public async Task<MFile> Create(PublicSigningKey owner)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "INSERT INTO public.files VALUES (@owner) RETURNING *";

		// Create parameters
		DbParameter pOwner = command.CreateParameter();
		pOwner.ParameterName = "@owner";
		pOwner.DbType = DbType.Guid;
		pOwner.Value = owner.ToString();

		// Add parameters to command
		command.Parameters.Add(pOwner);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		
		if (reader.RecordsAffected != 1)
		{
			goto Fail;
		}
		
		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MFile(reader);
		}
		
		Fail: 
			await transaction.RollbackAsync();
			throw new InsertFailedException(command);
	}

	public async Task<MFile?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM public.files WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new MFile(reader);
		}
		
		return null;
	}

	public async Task<MFile> Update(MFile file)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "UPDATE public.files SET last_accessed = @last_accessed WHERE id = @id RETURNING *";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = file.Id;

		DbParameter pLastAccessed = command.CreateParameter();
		pLastAccessed.ParameterName = "@last_accessed";
		pLastAccessed.DbType = DbType.DateTime;
		pLastAccessed.Value = file.LastAccessed; 
			
		// Add parameters to command
		command.Parameters.Add(pId);
		command.Parameters.Add(pLastAccessed);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		
		if (reader.RecordsAffected != 1)
		{
			goto Fail;
		}
		
		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MFile(reader);
		}
		
		Fail: 
			await transaction.RollbackAsync();
			throw new UpdateFailedException(command);
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "DELETE FROM public.files WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		int rowsAffected = await command.ExecuteNonQueryAsync();
		if (rowsAffected == 0)
		{
			await transaction.RollbackAsync();
			throw new DataException("Failed to delete file, no rows affected.");
		}

		await transaction.CommitAsync();
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT id FROM public.files WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		object? result = await command.ExecuteScalarAsync();
		
		return result != null;
	}
}