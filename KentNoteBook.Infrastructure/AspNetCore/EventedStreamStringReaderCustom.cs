using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.NodeServices.Util
{
	public class EventedStreamStringReaderCustom : IDisposable
	{
		private EventedStreamReaderCustom _eventedStreamReader;
		private bool _isDisposed;
		private StringBuilder _stringBuilder = new StringBuilder();

		public EventedStreamStringReaderCustom(EventedStreamReaderCustom eventedStreamReader) {
			_eventedStreamReader = eventedStreamReader
				?? throw new ArgumentNullException(nameof(eventedStreamReader));
			_eventedStreamReader.OnReceivedLine += OnReceivedLine;
		}

		public string ReadAsString() => _stringBuilder.ToString();

		private void OnReceivedLine(string line) => _stringBuilder.AppendLine(line);

		public void Dispose() {
			if ( !_isDisposed ) {
				_eventedStreamReader.OnReceivedLine -= OnReceivedLine;
				_isDisposed = true;
			}
		}
	}
}
