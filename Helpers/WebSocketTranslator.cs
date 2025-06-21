using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using EchoLib.Comms;

namespace EchoLib.Helpers;

public static class WebSocketTranslator
{
	/// <summary>
	/// QOL Helper method for sending actions easily.
	/// </summary>
	/// <param name="socket">Target socket connection.</param>
	/// <param name="parameters">Parameters of the action</param>
	/// <typeparam name="TAction">Action Type</typeparam>
	/// <typeparam name="TParams">Action Parameters Type</typeparam>
	public static async Task SendAction<TAction, TParams>(ClientWebSocket socket, TParams parameters)
	where TAction : IAction<TParams>, new()
	{
		// Create a message envelope for the action
		MessageEnvelope<TParams> message = new()
		{
			Target = TAction.Target,
			Data = new ActionWrapper<TParams>
			{
				Action = TAction.Action,
				Params = parameters
			}
		};

		// TODO: Check if this fails when emoji or scripts from other languages are used in fields contained within parameters
		byte[] json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

		await socket.SendAsync(json, WebSocketMessageType.Text, true, CancellationToken.None);
	}
}