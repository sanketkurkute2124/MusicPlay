namespace MusicApi.Models
{
    public class Songs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public string Url { get; set; }
    }
}
