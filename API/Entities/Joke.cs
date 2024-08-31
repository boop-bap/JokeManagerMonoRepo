using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokeAPI.Entities
{
    public class Joke
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }  = string.Empty;
        public string Category { get; set; } = string.Empty;

        // Foreign key to User
        public int UserId { get; set; }
    }
}
