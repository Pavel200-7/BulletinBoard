using BulletinBoard.Contracts.Bulletin.BulletinCategory;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

public interface IBulletinCategoryService
{
    public Task<BulletinCategoryDto> GetByIdAsync(Guid id);

    public Task<IReadOnlyCollection<BulletinCategoryDto>> GetAsync(BulletinCategoryFilterDto categoryDto);
    
    public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto);

    public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto);

    public Task<bool> DeleteAsync(Guid id);

    public Task<BulletinCategoryReadAllDto> GetAllAsync();

    public Task<BulletinCategoryReadSingleDto> GetSingleAsync(Guid id);
}
