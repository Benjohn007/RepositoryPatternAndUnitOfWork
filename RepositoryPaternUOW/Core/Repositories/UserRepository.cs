using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryPaternUOW.Core.IRepository;
using RepositoryPaternUOW.Data;
using RepositoryPaternUOW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPaternUOW.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository( ApplicationDbContext context, ILogger logger) : base(context,logger)
        {

        }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Upsert(User entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                              .FirstOrDefaultAsync();

                if(existingUser == null)
                    return await Add(entity);

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;

                return true;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                       .FirstOrDefaultAsync();

                if(exist != null)
                {
                  dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Delete methode error", typeof(UserRepository));
                return false;
            }
        }
    }
}
