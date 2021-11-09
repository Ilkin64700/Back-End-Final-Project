using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Blog:BaseEntity
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }

        [Required]
        [StringLength(maximumLength: 1500)]
        public string Desc { get; set; }

        public Category Category { get; set; }
    }
}
