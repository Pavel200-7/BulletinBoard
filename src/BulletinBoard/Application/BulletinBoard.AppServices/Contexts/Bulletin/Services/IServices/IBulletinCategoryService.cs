using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices
{
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
}
