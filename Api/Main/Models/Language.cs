using System.ComponentModel.DataAnnotations;

namespace Catalyst.Api.Main.Models
{
    public class Language : IModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Dialect { get; set; }

        [Required]
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}