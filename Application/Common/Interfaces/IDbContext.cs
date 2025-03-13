using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        public DatabaseFacade Database { get; }
        public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity: class;
        public EntityEntry Update(object entity);
        public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        public EntityEntry Remove(object entity);
        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
