using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalyst.Api.Main.Models
{
    public class Project : IModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public List<Language> Languages { get; set; }
    }
}