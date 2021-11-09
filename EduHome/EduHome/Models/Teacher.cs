using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Teacher:BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Position { get; set; }

        [Required]
        [StringLength(maximumLength: 1500)]
        public string About { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Degree { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Experience { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Hobbies { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Faculty { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Mail { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Number { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Skype { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public List<Course> Courses{ get; set; }
    }
}