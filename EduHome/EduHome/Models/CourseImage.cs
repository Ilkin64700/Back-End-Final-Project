using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class CourseImage:BaseEntity
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        public Course Course { get; set; }
    }
}
