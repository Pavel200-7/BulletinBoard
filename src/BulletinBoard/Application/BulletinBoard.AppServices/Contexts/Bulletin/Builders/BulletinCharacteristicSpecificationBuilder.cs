using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.SpecificationBuilderBase;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinCharacteristicSpecificationBuilder : SpecificationBuilderBase<BulletinCharacteristic>, IBulletinCharacteristicSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder WhereName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _specification.Add(c => c.Name == name);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder WhereNameContains(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _specification.Add(c => c.Name != null && c.Name.ToLower().Contains(name.ToLower()));
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder WhereCategoryId(Guid categoryId)
    {
        if (categoryId != Guid.Empty)
        {
            _specification.Add(c => c.CategoryId == categoryId);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder OrderByName(bool ascending = true)
    {
        _orderByExpression = u => u.Name;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder Paginate(int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        _specification.Skip = (pageNumber - 1) * pageSize;
        _specification.Take = pageSize;
        return this;
    }
}
