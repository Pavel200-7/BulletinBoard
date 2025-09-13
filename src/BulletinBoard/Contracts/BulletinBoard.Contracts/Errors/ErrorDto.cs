namespace BulletinBoard.Contracts.Errors;

/// <summary>
/// ДТО вывода ошибок
/// </summary>
public class ErrorDto
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Id для полной трассировки в распределённой трассировке
    /// </summary>
    public string TraceId { get; set; }
}
