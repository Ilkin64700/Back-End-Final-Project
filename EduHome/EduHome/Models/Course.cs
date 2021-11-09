using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Course: BaseEntity
    {
        public int CategoryId { get; set; }
        public int TeacherId { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string AboutDesc { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string ApplyDesc { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string CertificationDesc { get; set; }
        public Category Category { get; set; }
        public Teacher Teacher { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        public List<CourseImage> CourseImages { get; set; }
    }
}