using System.ComponentModel.DataAnnotations;

namespace DigitalAwareness.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public int StateId { get; set; }

        public virtual State State { get; set; } = null!;
    }
}