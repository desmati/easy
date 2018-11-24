using System.ComponentModel;

namespace System
{
	public class EasyPair
	{
		public EasyPair(string key, string value)
		{
			Key = key;
			Value = value;
		}

		public string Key { get; }
		public string Value { get; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out string key, out string value)
		{
			key = null;
			value = null;
		}
		public override string ToString()
		{
			return Key + ":" + Value;
		}
	}
}
