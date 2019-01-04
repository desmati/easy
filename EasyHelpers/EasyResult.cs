namespace System
{
	public class EasyResult
	{
		public bool Succeed { get; set; }
		public string Message { get; set; }
		public int StatusCode { get; set; }

		public static EasyResult Success(string message, int statusCode = 200)
		{
			return new EasyResult()
			{
				Message = message,
				Succeed = true,
				StatusCode = statusCode
			};
		}

		public static EasyResult Failure(string message, int statusCode = 400)
		{
			return new EasyResult()
			{
				Message = message,
				Succeed = false,
				StatusCode = statusCode
			};
		}

		public static EasyResult Failure(Exception exception)
		{
			return new EasyResult()
			{
				Message = exception.Message + " | " + exception.InnerException?.Message,
				Succeed = false,
				StatusCode = 400
			};
		}
	}

	public class EasyResult<T>
	{
		public bool Succeed { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		public int StatusCode { get; set; }

		public static EasyResult<T> Success(string message, T data = default(T), int statusCode = 200)
		{
			return new EasyResult<T>()
			{
				Data = data,
				Message = message,
				Succeed = true,
				StatusCode = statusCode
			};
		}

		public static EasyResult<T> Failure(string message, T data = default(T), int statusCode = 400)
		{
			return new EasyResult<T>()
			{
				Data = data,
				Message = message,
				Succeed = false,
				StatusCode = statusCode
			};
		}

		public static EasyResult<T> Failure(Exception exception)
		{
			return new EasyResult<T>()
			{
				Data = default(T),
				Message = exception.Message + " | " + exception.InnerException?.Message,
				Succeed = false,
				StatusCode = 400
			};
		}
	}
}
