using System.Data;
using System.Data.Common;
using Database.Models.Media;

namespace Database.Handlers.Defaults.Media;

public class FilesHandler : BaseHandler
{
	public async Task<MFile> Create(string owner)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO media.files VALUES (@owner) RETURNING *";

		// Create parameters
		DbParameter pOwner = command.CreateParameter();
		pOwner.ParameterName = "@owner";
		pOwner.DbType = DbType.String;
		pOwner.Value = owner;

		// Add parameters
		command.Parameters.Add(pOwner);

		// Execute command
		return await RunModify(command, reader => new MFile(reader));
	}

	public async Task<MFile?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM media.files WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		return await RunGet(command, reader => new MFile(reader));
	}

	public async Task<MFile> Update(MFile file)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE media.files SET last_accessed = @last_accessed WHERE id = @id RETURNING *";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = file.Id;

		DbParameter pLastAccessed = command.CreateParameter();
		pLastAccessed.ParameterName = "@last_accessed";
		pLastAccessed.DbType = DbType.DateTime;
		pLastAccessed.Value = file.LastAccessed; 
			
		// Add parameters
		command.Parameters.Add(pId);
		command.Parameters.Add(pLastAccessed);

		// Execute command
		return await RunModify(command, reader => new MFile(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM media.files WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM media.files WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		return await RunExists(command);
	}
}