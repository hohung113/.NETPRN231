namespace eStoreClient.Services
{
    public interface IApiService
    {
        Task<T> GetFromApiAsync<T>(string endpoint);
        Task<T> PostToApiAsync<T>(string endpoint, object data);
    }
}
