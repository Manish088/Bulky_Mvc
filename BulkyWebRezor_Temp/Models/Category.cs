﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRezor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Orders")]
        [Range(1, 100, ErrorMessage = "Display Order Must be between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
