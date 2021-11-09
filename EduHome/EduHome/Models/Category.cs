using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
        public List<Event> Events { get; set; }
        public List<Course> Courses { get; set; }
    }
}