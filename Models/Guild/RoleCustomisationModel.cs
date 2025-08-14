using System.Text.Json.Serialization;
using Models.Media;

namespace Models.Guild;

public class RoleCustomisationModel : VisualCustomisationModel
{
	[JsonPropertyName("text-colour")] public string? TextColour { get; set; }
	[JsonPropertyName("background-colour")] public string? BackgroundColour { get; set; }
}