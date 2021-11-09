using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Tag : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string Name { get; set; }
    }
}
