using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceTask.Models
{
    public enum HashMethod
    {
        MD5,
        SHA1,
        SHA256
    }
    [PrimaryKey(nameof(ProviderId), nameof(ProviderKey))]
    public class Credentials
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int ProviderId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "Provider key cannot exceed 100 characters.")]
        public string ProviderKey { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        [Required]
        public HashMethod Hasher { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [StringLength(255, ErrorMessage = "Password hash cannot exceed 255 characters.")]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [StringLength(255, ErrorMessage = "Password salt cannot exceed 255 characters.")]
        public string PasswordSalt { get; set; }

        public User User { get; set; }

    }
}
