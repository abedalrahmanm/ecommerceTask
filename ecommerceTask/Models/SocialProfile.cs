using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceTask.Models
{
    public enum SocialPlatform
    {
        Facebook,
        Twitter,
        LinkedIn,
        Instagram,
        GitHub,
        YouTube,
        Other
    }

    public class SocialProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int UId { get; set; }

        [Required]
        public SocialPlatform Platform { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "Platform username cannot exceed 50 characters.")]
        public string PlatformUser { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
