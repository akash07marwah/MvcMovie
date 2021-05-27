using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class GameContext : DbContext
    {   
        public GameContext (DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }
    }
}