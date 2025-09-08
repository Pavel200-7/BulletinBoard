using AutoMapper;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base
{
    abstract public class BulletinBaseRepository
    {
        protected readonly BulletinContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<BulletinCategory> _dbSet;

        public BulletinBaseRepository(BulletinContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<BulletinCategory>();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
