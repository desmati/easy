namespace System
{
	public class EasyResult
	{
		public bool Succeed { get; set; }
		public string Message { get; set; }
		public int StatusCode { get; set; }

		public EasyResult Success(string message, int statusCode = 200)
		{
			return new EasyResult()
			{
				Message = message,
				Succeed = true,
				StatusCode = statusCode
			};
		}

		public EasyResult Failure(string message, int statusCode = 400)
		{
			return new EasyResult()
			{
				Message = message,
				Succeed = false,
				StatusCode = statusCode
			};
		}
	}

	public class EasyResult<T>
	{
		public bool Succeed { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		public int StatusCode { get; set; }

		public EasyResult<T> Success(string message, T data = default(T), int statusCode = 200)
		{
			return new EasyResult<T>()
			{
				Data = data,
				Message = message,
				Succeed = true,
				StatusCode = statusCode
			};
		}

		public EasyResult<T> Failure(string message, T data = default(T), int statusCode = 400)
		{
			return new EasyResult<T>()
			{
				Data = data,
				Message = message,
				Succeed = false,
				StatusCode = statusCode
			};
		}
	}
}
