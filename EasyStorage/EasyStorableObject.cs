namespace System.Data
{
	public abstract class EasyStorableObject<TEntityId>
	{
		public DateTime CreatedAt { get; set; }
		public TEntityId Id { get; set; }
		public TEntityId CreatedBy { get; set; }
	}
}
