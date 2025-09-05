using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void SameChanges()
        {
            _context.SaveChangesAsync();
        }
    }
}
