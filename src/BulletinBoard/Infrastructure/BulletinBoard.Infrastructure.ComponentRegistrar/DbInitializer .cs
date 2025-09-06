using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.ComponentRegistrar
{
    public class DbInitializer : IAsyncInitializer
    {
        private BulletinContext _bulletinContext;


        public DbInitializer
            (
            BulletinContext bulletinContext
            )
        {
            _bulletinContext = bulletinContext;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            await _bulletinContext.Database.MigrateAsync(cancellationToken);
        }
    }
}
