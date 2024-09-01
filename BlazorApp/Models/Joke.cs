namespace JokeUI.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

        public int UserId { get; set; }
    }
}
