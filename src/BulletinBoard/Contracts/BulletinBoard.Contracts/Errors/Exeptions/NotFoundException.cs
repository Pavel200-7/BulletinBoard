namespace BulletinBoard.Contracts.Errors.Exeptions;

/// <summary>
/// Ошибка для оповещения о том, что пользователь пытается 
/// взаимодействовать с сущностью, которой нет в системе
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string errorMessage;

    /// <inheritdoc/>
    public NotFoundException(string errMessage)
    {
        errorMessage = errMessage;
    }
}
