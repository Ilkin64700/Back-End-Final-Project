using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class NoticeBoard : BaseEntity
    {
        [Required]
        public string Date { get; set; }

        [Required]
        [StringLength(maximumLength: 1500)]
        public string Desc { get; set; }


    }
}