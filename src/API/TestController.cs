using System.Threading.Tasks;
using API.ManyToMany;
using API.OneToMany;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet("1")]
        public async Task<ManyToMany.User> LoadUsers([FromServices] ManyToManyDbContext dbContext)
        {
            var user = new ManyToMany.User();
            var role1 = new ManyToMany.Role();
            var role2 = new ManyToMany.Role();
            var ur1 = new ManyToMany.UserRole
            {
                User = user,
                Role = role1
            };
            var ur2 = new ManyToMany.UserRole
            {
                User = user,
                Role = role2
            };
            await dbContext.Set<ManyToMany.UserRole>().AddRangeAsync(ur1, ur2);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<ManyToMany.User>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstAsync();
        }

        [HttpGet("2")]
        public async Task<OneToMany.User> LoadUsers([FromServices] OneToManyDbContext dbContext)
        {
            var user1 = new OneToMany.User();
            var user2 = new OneToMany.User();
            var role1 = new OneToMany.Role();
            role1.Users.Add(user1);
            role1.Users.Add(user2);
            await dbContext.Set<OneToMany.Role>().AddAsync(role1);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<OneToMany.User>()
                .Include(u => u.Role)
                .ThenInclude(r => r.Users)
                .FirstAsync();
        }
    }
}