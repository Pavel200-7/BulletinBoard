using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.SpecificationBuilderBase;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinUserSpecificationBuilder : SpecificationBuilderBase<BulletinUser>, IBulletinUserSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereFullName(string name)
    {
        if (!String.IsNullOrEmpty(name))
        {
            _specification.Add(u => u.FullName == name);
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereFullNameContains(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _specification.Add(u => u.FullName.ToLower().Contains(name.ToLower()));
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereIsBlocked(bool isBlocked)
    {
        _specification.Add(u => u.Blocked == isBlocked);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereFormattedAddress(string formattedAddress)
    {
        if (!String.IsNullOrEmpty(formattedAddress))
        {
            _specification.Add(u => u.FormattedAddress == formattedAddress);
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereFormattedAddressContains(string formattedAddress)
    {
        if (!string.IsNullOrEmpty(formattedAddress))
        {
            _specification.Add(u => u.FormattedAddress.ToLower().Contains(formattedAddress.ToLower()));
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereCoordinates(double latitude, double longitude)
    {
        _specification.Add(u => u.Latitude == latitude && u.Longitude == longitude);


        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereCoordinatesCloser(double latitude, double longitude, double distance)
    {
        double degreesPerKm = 0.009;

        _specification.Add(u =>
            Math.Abs(u.Latitude - latitude) <= distance * degreesPerKm &&
            Math.Abs(u.Longitude - longitude) <= distance * degreesPerKm / Math.Cos(ToRadians(latitude)));

        return this;

    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WhereCoordinatesFarther(double latitude, double longitude, double distance)
    {
        double degreesPerKm = 0.009;

        _specification.Add(u =>
            Math.Abs(u.Latitude - latitude) >= distance * degreesPerKm &&
            Math.Abs(u.Longitude - longitude) >= distance * degreesPerKm / Math.Cos(ToRadians(latitude)));

        return this;
    }

    private double ToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WherePhone(string phone)
    {
        if (!string.IsNullOrEmpty(phone))
        {
            _specification.Add(u => u.Phone == phone);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder WherePhoneContains(string phone)
    {
        if (!string.IsNullOrEmpty(phone))
        {
            _specification.Add(u => u.Phone != null &&
                                  u.Phone.ToLower().Contains(phone.ToLower()));
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder OrderByFullName(bool ascending = true)
    {
        _orderByExpression = u => u.FullName;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinUserSpecificationBuilder Paginate(int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        _specification.Skip = (pageNumber - 1) * pageSize;
        _specification.Take = pageSize;
        return this;
    }
}
