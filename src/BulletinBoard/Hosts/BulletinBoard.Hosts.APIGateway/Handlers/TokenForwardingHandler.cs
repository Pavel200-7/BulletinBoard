namespace BulletinBoard.Hosts.APIGateway.Handlers;

/// <summary>
/// Обработчик для автоматической передачи jwt при обращении из APIGateway.
/// </summary>
public class TokenForwardingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TokenForwardingHandler> _logger;

    public TokenForwardingHandler
        (
        IHttpContextAccessor httpContextAccessor,
        ILogger<TokenForwardingHandler> logger
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
        .FirstOrDefault();

        _logger.LogInformation($"Загаловок аунтификации: {authHeader}");

        if (!string.IsNullOrEmpty(authHeader))
        {
            request.Headers.Add("Authorization", authHeader);
            _logger.LogInformation($"Загаловок аунтификации передан");

        }

        return await base.SendAsync(request, cancellationToken);
    }
}
