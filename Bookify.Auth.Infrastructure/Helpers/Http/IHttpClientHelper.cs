using System.Threading.Tasks;

namespace Bookify.Auth.Infrastructure.Helpers.Http
{
    public interface IHttpClientHelper
    {
        /// <summary>
        /// Set Access token in Http header
        /// </summary>
        /// <param name="accessToken">access Token</param>
        void SetAccessToken(string accessToken);

        /// <summary>
        /// Send http post request
        /// </summary>
        /// <typeparam name="TRequestModel"></typeparam>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <param name="url">url</param>
        /// <param name="requestModel">model</param>
        /// <returns></returns>
        Task<TResponseModel> PostAsync<TRequestModel, TResponseModel>(string url, TRequestModel requestModel);

        /// <summary>
        /// Send http put request with response
        /// </summary>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<TResponseModel> PutAsync<TResponseModel>(string url);

        /// <summary>
        /// Send http put request
        /// </summary>
        /// <typeparam name="TRequestModel"></typeparam>
        /// <param name="url">url</param>
        /// <param name="requestModel">model</param>
        /// <returns></returns>
        Task PutAsync<TRequestModel>(string url, TRequestModel requestModel);

        /// <summary>
        /// Send http put request with response
        /// </summary>
        /// <typeparam name="TRequestModel"></typeparam>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<TResponseModel> PutAsync<TRequestModel, TResponseModel>(string url, TRequestModel requestModel);

        /// <summary>
        /// Send http Get request
        /// </summary>
        /// <typeparam name="TResponseModel">Resonse model</typeparam>
        /// <param name="url">url</param>
        /// <returns></returns>
        Task<TResponseModel> GetAsync<TResponseModel>(string url);

        /// <summary>
        /// Send http Delete request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task DeleteAsync(string url);

        /// <summary>
        /// Send http put request
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        Task PutAsync(string url);
    }
}
