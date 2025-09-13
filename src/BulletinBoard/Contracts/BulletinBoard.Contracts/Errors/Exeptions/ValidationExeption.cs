namespace BulletinBoard.Contracts.Errors.Exeptions;

/// <summary>
/// Ошибка ввода данных для создания или изменения сущности
/// </summary>
public class ValidationExeption : Exception
{
    /// <summary>
    /// Словарь, сопоставляющий поле и список его ошибок
    /// </summary>
    public IDictionary<string, string[]> ValidationErrors { get; }

    /// <inheritdoc/>
    public ValidationExeption(IDictionary<string, string[]> errors)
        : base("Validation errors occurred.")
    {
        ValidationErrors = errors;
    }
}
