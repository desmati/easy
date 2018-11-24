using System.Threading.Tasks;

namespace Services
{
	public interface ISmsSender
	{
		Task SendSmsAsync(string number, string message);
	}
}
