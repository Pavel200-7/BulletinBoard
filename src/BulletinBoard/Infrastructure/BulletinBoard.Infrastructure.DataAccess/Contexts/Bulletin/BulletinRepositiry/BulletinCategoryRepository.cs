using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry
{
    public class BulletinCategoryRepository : BulletinBaseRepository, IBulletinCategoryRepository
    {
        public BulletinCategoryRepository(BulletinContext context) : base(context)
        {
        }

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto category)
        {
            //throw new NotImplementedException();
            var res = new BulletinCategoryDto();
            res.IsLeafy = true;
            res.CategoryName = "111";
            return Task.FromResult<BulletinCategoryDto>(res);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto category)
        {
            throw new NotImplementedException();
        }
    }
}
