namespace BulletinBoard.Contracts.Errors.Exeptions;

public class NotFoundException : Exception
{
    public string errorMessage;

    public NotFoundException(string errMessage)
    {
        errorMessage = errMessage;
    }
}
