using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceTask.Models
{
    public enum DiscountType
    {
        None,
        Percentage,
        FixedAmount
    }

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Category")]
        [Column(TypeName = "int")]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [StringLength(255, ErrorMessage = "Picture URL cannot exceed 255 characters.")]
        public string Picture { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [StringLength(500, ErrorMessage = "Summary cannot exceed 500 characters.")]
        public string Summary { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public DiscountType DiscountType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountValue { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "Tags cannot exceed 100 characters.")]
        public string Tags { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }


        public List<CartItem> CartItems { get; set; }
        public List<OrderLine> orderLines { get; set; }
        public List<Review> Reviews { get; set; }
        public Category Category { get; set; }

    }
}
