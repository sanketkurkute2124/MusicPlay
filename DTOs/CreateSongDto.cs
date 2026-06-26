namespace MusicApi.DTOs
{
    public class CreateSongDto
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public string Url { get; set; }
    }
}
