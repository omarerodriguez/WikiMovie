using System.ComponentModel.DataAnnotations;

namespace WikiMovies.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string NameCategory { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
