using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Models;
using System.Net.WebSockets;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : Controller
    {
        public MusicDbContext MusicDbContext;
        public SongsController(MusicDbContext MusicDbContext)
        {
            this.MusicDbContext = MusicDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetSong()
        {
            var song= await MusicDbContext.Songs.ToListAsync();
            return Ok(song);
        }
        [HttpPost]
        public async Task<IActionResult> AddSong(Songs songs)
        {
          //  var song = await MusicDbContext.Songs.FirstAsy();
            //if (song != null)
            //{
                MusicDbContext.Songs.Add(songs);
                MusicDbContext.SaveChanges();
         //   }
            return Ok(songs);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var result = await MusicDbContext.Songs.FindAsync(id);
            if(result==null)
            {
                NotFound();
            }
            var song=MusicDbContext.Songs.Remove(result);
            MusicDbContext.SaveChanges();
            return Ok();


        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateSong(int id,[FromBody] Songs songs)
        {
            if (id != songs.Id)
                return BadRequest();
            var existingSong = await MusicDbContext.Songs.FindAsync(id);
            if(existingSong == null)
            {
                return BadRequest();
            }
            existingSong.Title = songs.Title;
            existingSong.Artist = songs.Artist;
            existingSong.Album = songs.Album;
            existingSong.Url = songs.Url;
           
            MusicDbContext.SaveChanges();
            return NoContent();
        }
    }
}
