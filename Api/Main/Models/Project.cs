using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalyst.Api.Main.Models
{
    [Table("projects")]
    public class Project : IModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [Column("description")]
        public string Description { get; set; }

        public List<Language> Languages { get; set; }
    }
}