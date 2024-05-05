namespace productivity_hub_api.Service.HttpClient
{
    public interface IHttpClient
    {
        Task<R> GetAsync<R>(string url);
        Task<R> PostAsync<R, C>(string url, C content);
        Task<R> PutAsync<R, C>(string url, C content);
        Task<R> DeleteAsync<R>(string url);
    }
}
