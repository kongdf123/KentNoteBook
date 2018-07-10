using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace KentNoteBook.Infrastructure.Utility
{
	public static class DistributedCacheExtension
	{
		static readonly string _keyPrefix = "KentNoteBook";

		public static void SetCache<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options = null) {

			key = $"{_keyPrefix}_{key}";

			var json = JsonConvert.SerializeObject(value);
			options = options ?? new DistributedCacheEntryOptions {
				AbsoluteExpiration = DateTime.Now.AddMinutes(20) // keep the cache data for 20 minutes by default.
			};

			cache.SetString(key, json, options);
		}

		public static T GetCache<T>(this IDistributedCache cache, string key) {

			key = $"{_keyPrefix}_{key}";

			var json = cache.GetString(key);
			if ( string.IsNullOrEmpty(json) ) {
				return default(T);
			}

			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
