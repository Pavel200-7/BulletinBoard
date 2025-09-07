using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository
{
    public interface IBulletinCategoryRepository
    {
        public Task<BulletinCategoryDto> GetByIdAsync(Guid id);

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto);

        public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> IsTheIdExist(Guid id);

        public Task<bool> IsTheCategoryNameExist(string categoryName);

        public Task<bool> IsTheCategoryLeafy(Guid id);

        public void SaveChangesAsync();
    }
}
