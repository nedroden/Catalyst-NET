using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalyst.Api.Main.Models
{
    [Table("languages")]
    public class Language : IModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Column("dialect")]
        public string Dialect { get; set; }

        [Required]
        [Column("project_id")]
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}