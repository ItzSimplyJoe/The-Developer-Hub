namespace DeveloperHub
{
    public class HttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;
    }

}
