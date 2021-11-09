using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Subscribe:BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Mail { get; set; }
    }
}