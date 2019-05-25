using Bookify.Auth.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bookify.Auth.Infrastructure.Helpers.Http
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly HttpClient _client;
        private Object thisLock = new Object();

        public const string AuthorizationHeader = "Authorization";
        public const string MediaType = "application/json";

        public HttpRequestHeaders DefaultRequestHeaders => _client.DefaultRequestHeaders;

        /// <summary>
        /// constructor to set headser snad access token
        /// </summary>
        /// <param name="baseAddress">base Address</param>
        //// <param name="accessToken">accessToken</param>
        /// <param name="addedHeaders">added headers from out of scope</param>
        public HttpClientHelper(string baseAddress, bool addedHeaders = true)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            if (addedHeaders)
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.UserAgent.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

            }
        }

        /// <summary>
        /// Set Access token in Http header
        /// </summary>
        /// <param name="accessToken">access Token</param>
        public void SetAccessToken(string accessToken)
        {
            lock (thisLock)
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(AuthorizationHeader, accessToken);
            }
        }

        /// <summary>
        /// Send http post request
        /// </summary>
        /// <typeparam name="TRequestModel">request Model</typeparam>
        /// <typeparam name="TResponseModel">response tModel</typeparam>
        /// <param name="url">url to send post request</param>
        /// <param name="requestModel">request Model</param>
        /// <returns></returns>
        public async Task<TResponseModel> PostAsync<TRequestModel, TResponseModel>(string url,
            TRequestModel requestModel)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException($"URL is missing.");
            }

            if (requestModel == null)
            {
                throw new ArgumentNullException($"Request login model is missing.");
            }

            var content = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(MediaType);

            var response = await _client.PostAsync(url, byteContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                throw GetExceptionByHttpStatusCode(response, responseContent);
            }

            var result = JsonConvert.DeserializeObject<TResponseModel>(responseContent);

            return result;
        }

        /// <summary>
        /// Send http put request
        /// </summary>
        /// <typeparam name="TRequestModel">request Model</typeparam>
        /// <param name="url">url to send put request</param>
        /// <param name="requestModel">model</param>
        /// <returns></returns>
        public async Task PutAsync<TRequestModel>(string url, TRequestModel requestModel)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException($"URL is missing.");
            }

            if (requestModel == null)
            {
                throw new ArgumentNullException($"Request login model is missing.");
            }

            var content = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(MediaType);

            var response = await _client.PutAsync(url, byteContent);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                throw GetExceptionByHttpStatusCode(response, responseContent);
            }
        }

        /// <summary>
        /// Send http put request with response
        /// </summary>
        /// <typeparam name="TResponseModel">Response Model</typeparam>
        /// <param name="url">url to send put request</param>
        /// <returns>TResponseModel</returns>
        public async Task<TResponseModel> PutAsync<TResponseModel>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException($"URL is missing.");
            }

            var response = await _client.PutAsync(url, null);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                throw GetExceptionByHttpStatusCode(response, responseContent);
            }

            var result = JsonConvert.DeserializeObject<TResponseModel>(responseContent);
            return result;
        }

        /// <summary>
        /// Send http put request
        /// </summary>
        /// <param name="url">url to send put request</param>
        /// <returns></returns>
        public async Task PutAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException($"URL is missing.");
            }

            var response = await _client.PutAsync(url, null);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                throw GetExceptionByHttpStatusCode(response, responseContent);
            }
        }

        /// <summary>
        /// Send http put request with response
        /// </summary>
        /// <typeparam name="TRequestModel"></typeparam>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <param name="url">url to send put request</param>
        /// <param name="requestModel">request Model</param>
        /// <returns></returns>
        public async Task<TResponseModel> PutAsync<TRequestModel, TResponseModel>(string url,
            TRequestModel requestModel)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException($"URL is missing.");
            }

            if (requestModel == null)
            {
                throw new ArgumentNullException($"Request login model is missing.");
            }

            var content = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(MediaType);

            var response = await _client.PutAsync(url, byteContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw GetExceptionByHttpStatusCode(response, responseContent);
            }

            var result = JsonConvert.DeserializeObject<TResponseModel>(responseContent);

            return result;
        }

        /// <summary>
        /// Send http Get request
        /// </summary>
        /// <typeparam name="TResponseModel">Resonse model</typeparam>
        /// <param name="url">url to send get request</param>
        /// <returns></returns>
        public async Task<TResponseModel> GetAsync<TResponseModel>(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL is missing.");
            }

            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                throw GetExceptionByHttpStatusCode(response, content);
            }

            var result = JsonConvert.DeserializeObject<TResponseModel>(content);
            return result;
        }

        /// <summary>
        /// Send http Delete request
        /// </summary>
        /// <param name="url">url to send delete request</param>
        /// <returns></returns>
        public async Task DeleteAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL is missing.");
            }

            var response = await _client.DeleteAsync(url);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw GetExceptionByHttpStatusCode(response, content);
            }
        }

        /// <summary>
        /// Get exception by status code
        /// </summary>
        /// <param name="responseMessage"> response message of http method</param>
        /// <param name="content">content of error reponse</param>
        /// <returns>Http reponse exception</returns>
        private static HttpResponseException GetExceptionByHttpStatusCode(HttpResponseMessage responseMessage,
            string content)
        {
            var contentError = JsonConvert.DeserializeObject<ResponseErrorModel>(content);

            ResponseErrorModel error;

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    {
                        error = string.IsNullOrEmpty(content)
                            ? new ResponseErrorModel("User is not authorized.")
                            : contentError;
                        return new HttpResponseException(responseMessage.StatusCode, error);
                    }
                case HttpStatusCode.NotFound:
                    {
                        error = string.IsNullOrEmpty(content)
                            ? new ResponseErrorModel("Not Found.")
                            : contentError;
                        return new HttpResponseException(responseMessage.StatusCode, error);
                    }
                default:
                    {
                        return new HttpResponseException(responseMessage.StatusCode, contentError);
                    }
            }
        }
    }
}
