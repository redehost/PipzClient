using Newtonsoft.Json;
using Pipz.Models;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pipz
{
    public class PipzClient
    {
        private HttpClient _httpClient;
        private RetryPolicy _waitAndRetryPolicy;
        private RetryPolicy _waitAndRetryAsyncPolicy;
        private User _user;

        public PipzClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["pipz:default-address"])
            };

            _waitAndRetryPolicy = Policy
                 .Handle<Exception>()
                 .WaitAndRetry(new[] {
                     TimeSpan.FromSeconds(1),
                     TimeSpan.FromSeconds(2),
                     TimeSpan.FromSeconds(3),
                 }, (exception, timeSpan, context) =>
                 {

                 });

            _waitAndRetryAsyncPolicy = Policy
                 .Handle<Exception>()
                 .WaitAndRetryAsync(new[] {
                     TimeSpan.FromSeconds(1),
                     TimeSpan.FromSeconds(2),
                     TimeSpan.FromSeconds(3),
                 }, (exception, timeSpan, context) =>
                 {

                 });
        }

        /// <summary>
        /// Método responsável por identificar o usuário que está efetuando o evento. 
        /// Caso ele não exista, será criado um novo
        /// </summary>
        /// <param name="user"></param>
        /// <returns>O próprio objeto</returns>
        public PipzClient Identify(User user)
        {            
            return _waitAndRetryPolicy.Execute(() =>
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

                var response = _httpClient.PostAsync(_httpClient.BaseAddress, httpContent).Result;

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.StatusCode.ToString());

                _user = user;

                return this;
            });
        }

        /// <summary>
        /// Chamar um evento na API do pipz
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public async Task Track(string eventName, Dictionary<string, object> properties)
        {
            CancellationToken ct;

            await _waitAndRetryAsyncPolicy.ExecuteAsync(async token =>
            {
                if (eventName == null)
                    throw new NullReferenceException("eventName");

                if (properties == null)
                    throw new NullReferenceException("properties");

                if (_user == null)
                    throw new NullReferenceException("You should call Identify method first");

                if (_user.Email == null)
                    throw new NullReferenceException("You should fill a valid e-mail");

                var body = new
                {
                    properties,
                    @event = eventName,
                    type = "track",
                    writeKey = ConfigurationManager.AppSettings["pipz:tracker-api-key"],
                    userId = _user.Email
                };

                var content = JsonConvert.SerializeObject(body);

                var httpContent = new StringContent(content);

                var response = await _httpClient.PostAsync(_httpClient.BaseAddress, httpContent);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.StatusCode.ToString());
            }, ct);

        }
    }
}