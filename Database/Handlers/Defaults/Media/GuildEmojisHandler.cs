using System.Data;
using System.Data.Common;
using Core.Auth.Signing;
using Database.Models.Media;

namespace Database.Handlers.Defaults.Media;

public class GuildEmojisHandler : BaseHandler
{
    public async Task<MGuildEmoji> Create(Guid guildId, UserId createdBy, string name, Guid fileId, MGuildEmoji.MediaType? type = null, string? customisation = null)
    {
        // Create command
        await using DbCommand command = await Command(true);
        command.CommandText =
            "INSERT INTO media.guild_emojis (guild_id, created_by, name, file_id, type, customisation) " +
            "VALUES (@guild_id, @created_by, @name, @file_id, @type, @customisation) RETURNING *";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
            { "@created_by", new Parameter { Type = DbType.String, Value = createdBy.ToString() } },
            { "@name", new Parameter { Type = DbType.String, Value = name } },
            { "@file_id", new Parameter { Type = DbType.Guid, Value = fileId } },
            { "@type", new Parameter { Type = DbType.UInt16, Value = (ushort?)type, Nullable = true } },
            { "@customisation", new Parameter { Type = DbType.String, Value = customisation, Nullable = true } }
        });

        // Execute command
        return await RunModify(command, reader => new MGuildEmoji(reader));
    }

    public async Task<MGuildEmoji?> Get(Guid id)
    {
        // Create command
        await using DbCommand command = await Command(false);
        command.CommandText = "SELECT * FROM media.guild_emojis WHERE id = @id";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@id", new Parameter { Type = DbType.Guid, Value = id } }
        });

        // Execute command
        return await RunGet(command, reader => new MGuildEmoji(reader));
    }

    public async Task<MGuildEmoji> Update(MGuildEmoji emoji)
    {
        // Create command
        await using DbCommand command = await Command(true);
        command.CommandText =
            "UPDATE media.guild_emojis SET guild_id = @guild_id, created_by = @created_by, name = @name, " +
            "file_id = @file_id, type = @type, customisation = @customisation WHERE id = @id RETURNING *";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@id", new Parameter { Type = DbType.Guid, Value = emoji.Id } },
            { "@guild_id", new Parameter { Type = DbType.Guid, Value = emoji.GuildId } },
            { "@created_by", new Parameter { Type = DbType.String, Value = emoji.CreatedBy.ToString() } },
            { "@name", new Parameter { Type = DbType.String, Value = emoji.Name } },
            { "@file_id", new Parameter { Type = DbType.Guid, Value = emoji.FileId } },
            { "@type", new Parameter { Type = DbType.Int32, Value = (int?)emoji.Type, Nullable = true } },
            { "@customisation", new Parameter { Type = DbType.String, Value = emoji.CustomisationRaw, Nullable = true } }
        });

        // Execute command
        return await RunModify(command, reader => new MGuildEmoji(reader));
    }

    public async Task Delete(Guid id)
    {
        // Create command
        await using DbCommand command = await Command(true);
        command.CommandText = "DELETE FROM media.guild_emojis WHERE id = @id";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@id", new Parameter { Type = DbType.Guid, Value = id } }
        });

        // Execute command
        await RunDelete(command);
    }

    public async Task<bool> Exists(Guid id)
    {
        // Create command
        await using DbCommand command = await Command(false);
        command.CommandText = "SELECT id FROM media.guild_emojis WHERE id = @id";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@id", new Parameter { Type = DbType.Guid, Value = id } }
        });

        // Execute command
        return await RunExists(command);
    }
}