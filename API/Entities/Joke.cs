namespace JokeAPI.Entities
{
    public class Joke
    {
        public int? Id { get; set; }
        public string Content { get; set; }  = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
