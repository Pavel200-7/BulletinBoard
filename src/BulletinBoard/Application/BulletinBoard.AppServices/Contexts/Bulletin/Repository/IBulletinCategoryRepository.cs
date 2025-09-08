using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository
{
    public interface IBulletinCategoryRepository
    {
        public Task<BulletinCategoryDto?> GetByIdAsync(Guid id);

        public Task<IReadOnlyCollection<BulletinCategoryDto>> FindAsync(ExtendedSpecification<BulletinCategory> specification);

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto);

        public Task<BulletinCategoryDto?> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto);

        public Task<bool> DeleteAsync(Guid id);

        public Task SaveChangesAsync();
    }
}
