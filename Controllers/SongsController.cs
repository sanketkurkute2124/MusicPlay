using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Models;
using System.Net.WebSockets;
using MusicApi.DTOs;

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
        // POST: api/Songs
        [HttpPost]
        public async Task<IActionResult> AddSong(CreateSongDto createSongDto)
        {
            var song = new Songs
            {
                Title = createSongDto.Title,
                Artist = createSongDto.Artist,
                Album = createSongDto.Album,
                Url = createSongDto.Url
            };

            await MusicDbContext.Songs.AddAsync(song);
            await MusicDbContext.SaveChangesAsync();

            return Ok(song);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, UpdateSongDto updateSongDto)
        {
            var existingSong = await MusicDbContext.Songs.FindAsync(id);

            if (existingSong == null)
                return NotFound();

            existingSong.Title = updateSongDto.Title;
            existingSong.Artist = updateSongDto.Artist;
            existingSong.Album = updateSongDto.Album;
            existingSong.Url = updateSongDto.Url;

            await MusicDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
