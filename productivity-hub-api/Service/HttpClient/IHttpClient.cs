namespace productivity_hub_api.Service.HttpClient
{
    public interface IHttpClient
    {
        Task<TResponse> GetAsync<TResponse>(string url);
        Task<TResponse> PostAsync<TResponse, TContent>(string url, TContent content);
        Task<TResponse> PutAsync<TResponse, TContent>(string url, TContent content);
        Task<TResponse> DeleteAsync<TResponse>(string url);
    }
}
