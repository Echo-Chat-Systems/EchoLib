namespace Models.User.Reputation;

public class ReputationModel
{
	public int Score
	{
		get
		{
			return Remarks.Sum(r => (int)r.Direction);
		}
	}

	public required IEnumerable<RemarkModel> Remarks { get; set; }
}