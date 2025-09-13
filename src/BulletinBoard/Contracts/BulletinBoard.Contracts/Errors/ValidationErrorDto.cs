namespace BulletinBoard.Contracts.Errors;

/// <summary>
/// Формат представления ошибок валидации
/// </summary>
public class ValidationErrorDto : Exception
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Список ошибок
    /// </summary>
    public IDictionary<string, string[]> Errors { get; set; }

    /// <summary>
    /// Id для полной трассировки в распределённой трассировке
    /// </summary>
    public string TraceId { get; set; }
}
