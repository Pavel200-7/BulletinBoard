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
        Task<BulletinCategoryDto> GetByIdAsync(Guid id);

        Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto category);

        Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto category);

        Task<bool> DeleteAsync(Guid id);
    }
}
