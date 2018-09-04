using System.Net;
using System.Net.Sockets;

namespace Microsoft.AspNetCore.SpaServices.Util
{
	public static class TcpPortFinderCustom
	{
		public static int FindAvailablePort() {
			var listener = new TcpListener(IPAddress.Loopback, 0);
			listener.Start();
			try {
				return ((IPEndPoint)listener.LocalEndpoint).Port;
			}
			finally {
				listener.Stop();
			}
		}
	}
}
