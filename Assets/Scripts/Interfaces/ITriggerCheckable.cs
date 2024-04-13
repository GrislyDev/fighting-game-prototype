public interface ITriggerCheckable
{
	bool IsChased { get;set; }
	bool IsWithinStrikingDistance { get;set; }
	void SetChaseStatus(bool isChased);
	void SetStrikingDistanceBool(bool isWithinStrikingDistance);
}