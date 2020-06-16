using System;
using Microsoft.AspNetCore.Http;

namespace ECommerceCase.WebClient.Extensions
{
	public static class RequestExtensions
	{
		public static Uri GetReferer(this HttpRequest request)
		{
			if (request == null)
			{
				return null;
			}
			
			if (request.Headers.ContainsKey("Referer"))
			{
				var refererHeader = request.Headers["Referer"];
				Uri refererUrl = new Uri(refererHeader.ToString());
				if (refererUrl.Host == request.Host.Host)
				{
					return refererUrl;
				}
			}

			return null;
		}

		public static bool IsValidUrl(this Uri url)
		{
			return Uri.TryCreate(url.ToString(), UriKind.Absolute, out Uri uriResult) && 
				(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
		}
	}
}