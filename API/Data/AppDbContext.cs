using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{

        // Example DbSet - replace or extend with real entities
        public DbSet<AppUser> Users { get; set; }
	}
}