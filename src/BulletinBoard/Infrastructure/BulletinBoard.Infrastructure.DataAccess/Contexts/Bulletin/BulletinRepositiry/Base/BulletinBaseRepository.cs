using AutoMapper;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base
{
    abstract public class BulletinBaseRepository
    {
        protected BulletinContext _context;
        protected IMapper _mapper;

        public BulletinBaseRepository(BulletinContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
