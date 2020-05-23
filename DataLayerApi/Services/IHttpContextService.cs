namespace DataLayerApi.Services
{
    public interface IHttpContextService
    {
        string GetHeaderValue(string headerName);
        bool HasHeader(string headerName);
    }
}