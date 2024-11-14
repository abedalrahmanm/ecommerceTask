using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceTask.Models
{
    

    public enum Locale
    {
        En,
        Fr
    }

    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int UserId {get; set;}


        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "Email can't exceed 50 characters.")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Column(TypeName = "nvarchar(14)")]
        [StringLength(14, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 14 digits.")]
        public string Phone { get; set; }

		public enum UserRole
		{
			Customer,
			Staff,
			Admin
		}
		[Required]
        public UserRole Role { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [Url(ErrorMessage = "Invalid URL format for Avatar.")]
        public string Avatar { get; set; }

        [Required]
        public Locale Locale { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Last Login")]
        public DateTime LastLogin { get; set; }

        [Column(TypeName = "bit")]
        [Display(Name = "Email Validated")]
        public bool? EmailValidated { get; set; }

        [Column(TypeName = "bit")]
        [Display(Name = "Phone Validated")]
        public bool? PhoneValidated { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [StringLength(500, ErrorMessage = "Bio can't exceed 500 characters.")]
        public string? Bio { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "Company name can't exceed 100 characters.")]
        public string? Company { get; set; }

        public List<SocialProfile> SocialProfiles { get; set; }
        public List<Cart> Carts { get; set; }

        public List<Order> Orders { get; set; }
        public List<Credentials> Credentials { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
