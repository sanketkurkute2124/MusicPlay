using Microsoft.EntityFrameworkCore;
using MusicApi.Models;

namespace MusicApi.Data
{
    public class MusicDbContext:DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options):base(options) 
        {
            
        }

       public  DbSet<Songs> Songs { get; set; }
    }
}
