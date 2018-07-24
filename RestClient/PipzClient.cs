using Newtonsoft.Json;
using Pipz.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace Pipz
{
	public class PipzClient
	{
		private HttpClient _Pipz;

		public PipzClient()
		{
			_Pipz = new HttpClient();
			_Pipz.BaseAddress = new Uri(ConfigurationManager.AppSettings["pipz:default-address"]);
		}

		public void Identify(User user)
		{
			if (user == null)
				throw new NullReferenceException("user");

			var body = new
			{
				traits = new
				{
					name = user.Name,
					email = user.Email,
					job_title = user.JobTitle,
					phone = user.Phone,
					company = user.Company,
				},
				type = "identify",
				writeKey = ConfigurationManager.AppSettings["pipz:tracker-api-key"],
				userId = user.UserId
			};

			var content = JsonConvert.SerializeObject(body);

			var httpContent = new StringContent(content);

			var request = _Pipz.PostAsync(_Pipz.BaseAddress, httpContent);

			if (!request.Result.IsSuccessStatusCode)
				throw new HttpRequestException(request.Result.StatusCode.ToString());
		}

		public void Track(string eventName, Dictionary<string, string> properties, User user)
		{
			if(eventName == null)
				throw new NullReferenceException("eventName");

			if (properties == null)
				throw new NullReferenceException("properties");

			if (user == null)
				throw new NullReferenceException("user");

			if(user.Email == null)
				throw new NullReferenceException("user.Email");

			var body = new
			{
				properties,
				@event = eventName,
				type = "track",
				writeKey = ConfigurationManager.AppSettings["pipz:tracker-api-key"],
				userId = user.Email
			};

			var content = JsonConvert.SerializeObject(body);

			var httpContent = new StringContent(content);

			var request = _Pipz.PostAsync(_Pipz.BaseAddress, httpContent);

			if (!request.Result.IsSuccessStatusCode)
				throw new HttpRequestException(request.Result.StatusCode.ToString());
		}
	}
}