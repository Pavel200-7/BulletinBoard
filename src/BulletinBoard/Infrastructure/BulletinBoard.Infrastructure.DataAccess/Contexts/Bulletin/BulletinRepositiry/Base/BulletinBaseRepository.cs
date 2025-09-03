using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base
{
    abstract public class BulletinBaseRepository
    {
        protected BulletinContext context;

        public BulletinBaseRepository(BulletinContext context)
        {
            this.context = context;
        }

        public void SameChanges()
        {
            context.SaveChangesAsync();
        }
    }
}
