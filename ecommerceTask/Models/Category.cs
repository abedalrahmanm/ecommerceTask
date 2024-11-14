using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace ecommerceTask.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

		// Self-referencing foreign key
		[ForeignKey("ParentCategory")]
		[Column(TypeName = "int")]
		public int? ParentCategoryId { get; set; }  // Nullable for top-level categories

		// Navigation property for self-join
		public virtual Category ParentCategory { get; set; }
		public virtual List<Category> SubCategories { get; set; }  // Navigation property for child categories



		[Required]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string Slung { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [StringLength(255)]
        public string Tags { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }



		public List<Product> products { get; set; }
    }
}
