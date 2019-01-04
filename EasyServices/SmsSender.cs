using System.Threading.Tasks;

namespace Services
{
	public class SmsSender : ISmsSender
	{
		public Task SendSmsAsync(string number, string message)
		{
			return Task.CompletedTask;
		}

		public Task SendSmsAsync(string number, string template, params string[] parameters)
		{
			throw new System.NotImplementedException();
		}
	}
}
