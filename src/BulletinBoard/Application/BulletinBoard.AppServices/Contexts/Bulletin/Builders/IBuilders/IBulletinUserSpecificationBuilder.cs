using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinUser в методах соответствующего репозитория.
/// Спецификации содержат фильтрацию, сортировку и пагинацию.
/// </summary>
public interface IBulletinUserSpecificationBuilder
{
    /// <summary>
    /// Добавить отбор по полному совпадению имени.
    /// </summary>
    /// <param name="name">Полное имя пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereFullName(string name);

    /// <summary>
    /// Добавить отбор по частичному совпадению имени.
    /// </summary>
    /// <param name="name">Фрагмент имени пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereFullNameContains(string name);

    /// <summary>
    /// Добавить отбор по статусу блокировки.
    /// </summary>
    /// <param name="isBlocked">Признак блокировки пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereIsBlocked(bool isBlocked);

    /// <summary>
    /// Добавить отбор по полному совпадению адреса.
    /// </summary>
    /// <param name="formattedAddress">Форматированный адрес пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereFormattedAddress(string formattedAddress);

    /// <summary>
    /// Добавить отбор по частичному совпадению адреса.
    /// </summary>
    /// <param name="formattedAddress">Фрагмент адреса пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereFormattedAddressContains(string formattedAddress);

    /// <summary>
    /// Добавить отбор по точным координатам.
    /// </summary>
    /// <param name="latitude">Широта местоположения пользователя.</param>
    /// <param name="longitude">Долгота местоположения пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereCoordinates(double latitude, double longitude);

    /// <summary>
    /// Добавить отбор пользователей в радиусе от указанных координат.
    /// </summary>
    /// <param name="latitude">Широта центра поиска.</param>
    /// <param name="longitude">Долгота центра поиска.</param>
    /// <param name="distance">Максимальное расстояние в километрах.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereCoordinatesCloser(double latitude, double longitude, double distance);

    /// <summary>
    /// Добавить отбор пользователей за пределами указанного радиуса.
    /// </summary>
    /// <param name="latitude">Широта центра поиска.</param>
    /// <param name="longitude">Долгота центра поиска.</param>
    /// <param name="distance">Минимальное расстояние в километрах.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WhereCoordinatesFarther(double latitude, double longitude, double distance);

    /// <summary>
    /// Добавить отбор по полному совпадению номера телефона.
    /// </summary>
    /// <param name="phone">Номер телефона пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WherePhone(string phone);

    /// <summary>
    /// Добавить отбор по частичному совпадению номера телефона.
    /// </summary>
    /// <param name="phone">Фрагмент номера телефона пользователя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder WherePhoneContains(string phone);

    /// <summary>
    /// Добавить сортировку по имени.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder OrderByFullName(bool ascending = true);

  
    /// <summary>
    /// Добавить пагинацию для результатов запроса.
    /// </summary>
    /// <param name="pageNumber">Номер страницы (начинается с 1).</param>
    /// <param name="pageSize">Количество элементов на странице.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinUserSpecificationBuilder Paginate(int pageNumber, int pageSize);

    /// <summary>
    /// Создать расширенную спецификацию на основе добавленных условий.
    /// </summary>
    /// <returns>Готовая спецификация для использования в репозитории.</returns>
    public ExtendedSpecification<BulletinUser> Build();
}