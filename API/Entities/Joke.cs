using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JokeAPI.Entities
{
    public class Joke
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        // Foreign key to User
        public int UserId { get; set; }
    }
}
