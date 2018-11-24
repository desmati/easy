namespace System.Data
{
	public interface IEasyStorableObject<TEntityId>
	{
		DateTime CreatedAt { get; set; }
		TEntityId Id { get; set; }
		TEntityId CreatedBy { get; set; }
	}
}
