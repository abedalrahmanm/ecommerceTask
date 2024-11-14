using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceTask.Models
{
    public enum CartStatus
    {
        Active,
        Pending,
        Completed,
        Cancelled,
        Abandoned
    }

    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "CreatedBy cannot exceed 50 characters.")]
        public string CreatedBy { get; set; }

        [Required]
        public CartStatus Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }

        public List<CartItem> CartItems { get; set; }  
    }
}
