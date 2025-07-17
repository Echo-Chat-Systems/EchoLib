using System.Data.Common;
using EchoLib.Database.Handlers;
using EchoLib.Database.Handlers.Chat;
using EchoLib.Database.Handlers.Config;
using EchoLib.Database.Handlers.Public;
using EchoLib.Database.Handlers.Secure;

namespace EchoLib.Database;

public class Db
{
	public HandlersGroup Handlers { get; private init; }

	public Db(DbDataSource dataSource)
	{
		// Initialize the handlers group with the provided data source
		Handlers = new HandlersGroup
		{
			Config = new ConfigHandlers
			{
				Data = new ConfigDataHandler
				{
					DataSource = dataSource
				},
				Owners = new OwnersHandler
				{
					DataSource = dataSource
				}
			},
			Public = new PublicHandlers
			{
				Invites = null,
				Roles = null,
				UserRoles = null,
				Users = null
			},
			Secure = new SecureHandlers
			{
				Certificates = new CertificatesHandler
				{
					DataSource = dataSource
				},
				ChannelCommits = new ChannelCommitsHandler
				{
					DataSource = dataSource
				},
				MlsStates = new MlsStatesHandler
				{
					DataSource = dataSource
				}
			},
			Chat = new  ChatHandlers
			{
				ChannelCategories = null,
				ChannelMembers = null,
				Channels = null,
				GuildMembers = null,
				Guilds = null
				
			},
			Media = null
		};

		// Populate the handlers with itself
		Handlers.Populate(Handlers);
	}
}