using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Database
{
    public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext(options)
    {
    }
}
