using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.APIGateway.Controllers.Extentions;

/// <summary>
/// Класс методов расширения ответа на запрос клиента httpFactory.
/// </summary>
public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Конвертировать в IActionResult.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static async Task<IActionResult> ToActionResult(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/json";

        return new ContentResult
        {
            Content = content,
            ContentType = contentType,
            StatusCode = (int)response.StatusCode
        };
    }
}