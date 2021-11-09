using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Testimonial:BaseEntity
    {

        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Position { get; set; }

        public int Order { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }


    }
}
