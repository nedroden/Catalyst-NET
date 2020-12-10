using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalyst.Api.Main.Models
{
    [Table("users")]
    public class User : IModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Column("nickname")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        public string Password { private get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Column("last_name")]
        public string LastName { get; set; }
    }
}