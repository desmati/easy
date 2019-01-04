using System.Collections.Generic;

namespace System.Data
{
	public class EasyPaging<TItems>
	{
		public EasyPaging(int pageNumber, int pageSize)
		{
			Rows = new List<TItems>();
			PageNumber = pageNumber;
			PageSize = pageSize;
		}

		public List<TItems> Rows { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int Count { get; set; }
		public int PagesCount { get { return (int)Math.Ceiling(Count / PageSize * 1D); } }
	}
}
